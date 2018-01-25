using System;
using System.Windows;
using MvvX.Plugins.CouchBaseLite;
using MvvX.Plugins.CouchBaseLite.Database;
using MvvX.Plugins.CouchBaseLite.Platform;

namespace Client.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICouchBaseLite couchBaseLite;

        private string username;

        private string password;

        private Uri url;

        private IDatabase database;

        public MainWindow()
        {
            InitializeComponent();

            InitializeCouchbase();
        }

        /// <summary>
        /// Initialize the couchbase database
        /// </summary>
        private void InitializeCouchbase()
        {
            couchBaseLite = new CouchBaseLite();
            
            couchBaseLite.Initialize("C:/temp/cbl");

            var databaseOptions = couchBaseLite.CreateDatabaseOptions();

            databaseOptions.Create = true;
            databaseOptions.StorageType = MvvX.Plugins.CouchBaseLite.Storages.StorageTypes.Sqlite;

            database = couchBaseLite.CreateConnection("beer", databaseOptions);
        }

        /// <summary>
        /// Initialize connection to db
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectToCouchbaseSync(object sender, RoutedEventArgs e)
        {
            InitializeCouchbase();
        }

        private void PushReplication(object sender, RoutedEventArgs e)
        {
            url = new Uri(UrlTextBox.Text);
            username = LoginTextBox.Text;
            password = PasswordTextBox.Text;

            var push = database.CreatePushReplication(url);
            push.SetBasicAuthenticator(username, password);
            push.Continuous = true;
            push.Changed += Push_Changed;

            push.Start();
        }

        private void PullReplication(object sender, RoutedEventArgs e)
        {
            url = new Uri(UrlTextBox.Text);
            username = LoginTextBox.Text;
            password = PasswordTextBox.Text;

            var pull = database.CreatePullReplication(url);
            pull.SetBasicAuthenticator(username, password);
            pull.Continuous = true;
            pull.Changed += Pull_Changed;

            pull.Start();
        }

        /// <summary>
        /// Triggered when a push replication causes changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Push_Changed(object sender, MvvX.Plugins.CouchBaseLite.Sync.IReplicationChangeEventArgs e)
        {
            ContentTextBlock.Text += string.Concat("PUSH : ", e.ToString(), "\n");
        }

        /// <summary>
        /// Triggered when a pull replication causes changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pull_Changed(object sender, MvvX.Plugins.CouchBaseLite.Sync.IReplicationChangeEventArgs e)
        {
            ContentTextBlock.Text += string.Concat("PULL : ",e.ToString(), "\n");
        }
    }
}
