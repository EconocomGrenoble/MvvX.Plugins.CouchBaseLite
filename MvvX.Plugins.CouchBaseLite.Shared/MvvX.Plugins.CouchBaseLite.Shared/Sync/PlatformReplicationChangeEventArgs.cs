using Couchbase.Lite;
using MvvX.Plugins.CouchBaseLite.Sync;

namespace MvvX.Plugins.CouchBaseLite.Shared.Sync
{
    public class PlatformReplicationChangeEventArgs : IReplicationChangeEventArgs
    {
        #region Fields

        private readonly ReplicationChangeEventArgs replicationChangeEventArgs;
        private readonly IReplication replication;

        #endregion

        #region Constructor

        public PlatformReplicationChangeEventArgs(ReplicationChangeEventArgs replicationChangeEventArgs, IReplication replication)
        {
            this.replicationChangeEventArgs = replicationChangeEventArgs;
            this.replication = replication;
        }

        #endregion

        #region Implements

        #endregion
    }
}
