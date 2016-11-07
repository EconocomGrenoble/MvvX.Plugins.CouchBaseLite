﻿using Couchbase.Lite;
using MvvX.Plugins.CouchBaseLite.Queries;
using System.Collections.Generic;
using System.Collections;
using MvvX.Plugins.CouchBaseLite.Database;
using System;

namespace MvvX.Plugins.CouchBaseLite.Platform.Queries
{
    public class PlatformQueryEnumerator : IQueryEnumerator
    {
        #region Fields

        private readonly QueryEnumerator queryEnumerator;
        private readonly IDatabase database;

        #endregion

        #region Constructor

        public PlatformQueryEnumerator(QueryEnumerator queryEnumerator, IDatabase database)
        {
            if(queryEnumerator == null)
            {
                throw new ArgumentNullException("queryEnumerator should not be null");
            }

            this.queryEnumerator = queryEnumerator;
            this.database = database;
        }

        #endregion

        #region Implements

        public int Count
        {
            get
            {
                return queryEnumerator.Count;
            }
        }

        public IQueryRow Current
        {
            get
            {
                if (queryEnumerator.Current == null)
                    return null;
                else
                    return new PlatformQueryRow(queryEnumerator.Current, database);
            }
        }

        public long SequenceNumber
        {
            get
            {
                return queryEnumerator.SequenceNumber;
            }
        }

        [Obsolete("This property is heavy and will be replaced by IsStale()")]
        public bool Stale
        {
            get
            {
                return queryEnumerator.Stale;
            }
        }

        public bool IsStale()
        {
            return queryEnumerator.IsStale();
        }

        object IEnumerator.Current
        {
            get
            {
                // TODO : Check this value.
                return queryEnumerator.Current;
            }
        }

        public void Dispose()
        {
            this.queryEnumerator.Dispose();
        }

        [Obsolete("Use LINQ ElementAt")]
        public IQueryRow GetRow(int index)
        {
            var row = this.queryEnumerator.GetRow(index);

            if (row != null)
                return new PlatformQueryRow(row, this.database);
            else
                return null;
        }

        public IEnumerator<IQueryRow> GetEnumerator()
        {
            return CastEnumerator(this.queryEnumerator.GetEnumerator());
        }

        private IEnumerator<IQueryRow> CastEnumerator(IEnumerator<QueryRow> iterator)
        {
            while (iterator.MoveNext())
            {
                IQueryRow row;

                if (iterator.Current != null)
                    row = new PlatformQueryRow(iterator.Current, database);
                else
                    row = null;

                yield return row;
            }
        }

        public bool MoveNext()
        {
            return this.queryEnumerator.MoveNext();
        }

        public void Reset()
        {
            this.queryEnumerator.Reset();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.queryEnumerator.GetEnumerator();
        }

        #endregion

        #region overrides

        public override bool Equals(object obj)
        {
            return this.queryEnumerator.Equals(obj);
        }
        
        public override int GetHashCode()
        {
            return this.queryEnumerator.GetHashCode();
        }

        #endregion

    }
}
