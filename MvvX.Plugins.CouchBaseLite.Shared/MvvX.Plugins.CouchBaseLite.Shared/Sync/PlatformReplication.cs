using MvvX.Plugins.CouchBaseLite.Sync;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvvX.Plugins.CouchBaseLite.Shared.Sync
{
    public class PlatformReplication : IReplication
    {
        #region Fields

        private readonly Couchbase.Lite.Replication replication;

        #endregion

        #region Constructor

        public PlatformReplication(Couchbase.Lite.Replication replication)
        {
            if (replication == null)
                throw new ArgumentNullException("replication");

            this.replication = replication;
            this.replication.Changed += Replication_Changed;
        }

        public bool Continuous
        {
            get
            {
                return this.replication.Continuous;
            }
            set
            {
                this.replication.Continuous = value;
            }
        }

        /// <summary>
        /// Change from the database
        /// </summary>
        public event EventHandler<IReplicationChangeEventArgs> Changed;

        private void Replication_Changed(object sender, Couchbase.Lite.ReplicationChangeEventArgs e)
        {
            if (Changed != null)
            {
                this.Changed(sender, new PlatformReplicationChangeEventArgs(e, this));
            }
        }

        public void Start()
        {
            this.replication.Start();
        }

        #endregion

        #region Implementation

        #endregion
    }
}
