using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvX.Plugins.CouchBaseLite.Sync
{
    public interface IReplication
    {
        /// <summary>
        /// Change from the database
        /// </summary>
        event EventHandler<IReplicationChangeEventArgs> Changed;

        bool Continuous { get; set; }

        void Start();
    }
}
