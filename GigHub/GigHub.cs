using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Configuration;
using System.Data.SqlClient;
using TableDependency.Enums;
using TableDependency.EventArgs;
using TableDependency.SqlClient;

namespace GigHub
{
    [HubName("gigHub")]
    public class GHub : Hub
    {

        private readonly PushNotification _push;


        public GHub()
            : this(PushNotification.Instance)
        {

        }


        public GHub(PushNotification push)
        {
            _push = push;
        }



        public void Start()
        {

            Groups.Add(Context.ConnectionId, Context.User.Identity.GetUserId());
            _push.ExecuteQuery();
        }

    }



    public class PushNotification : IDisposable
    {
        private static readonly Lazy<PushNotification> LazyInstance = new Lazy<PushNotification>(
            () => new PushNotification(clients: GlobalHost.ConnectionManager.GetHubContext<GHub>().Clients));




        private IHubConnectionContext<dynamic> _clients;
        private static SqlTableDependency<UserNotificationMap> _tableDependency;


        public PushNotification(IHubConnectionContext<dynamic> clients)
        {
            _clients = clients;

            _tableDependency = new SqlTableDependency<UserNotificationMap>(
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                "UserNotifications");

            _tableDependency.OnChanged += SqlTableDependency_Changed;
            _tableDependency.OnError += SqlTableDependency_OnError;
            _tableDependency.Start();
            _clients.All.started();

        }


        public static PushNotification Instance => LazyInstance.Value;


        void SqlTableDependency_OnError(object sender, ErrorEventArgs e)
        {
            _clients.All.error();
            throw e.Error;
        }

        void SqlTableDependency_Changed(object sender, RecordChangedEventArgs<UserNotificationMap> e)
        {
            if (e.ChangeType != ChangeType.None)
            {

                BroadcastStockPrice(e.Entity);
            }
        }

        private void BroadcastStockPrice(UserNotificationMap notification)
        {
            _clients.Group(notification.UserId).updateNotification(notification);
        }



        public void ExecuteQuery()
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (var cmd = new SqlCommand("SELECT UserId, NotificationId FROM UserNotifications", con))
                {
                    cmd.ExecuteReader();
                }
            }
        }



        #region IDisposable Support
        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _clients.All.ended();
                    _tableDependency.Stop();
                }

                _disposedValue = true;
            }
        }

        ~PushNotification()
        {
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }


    public class UserNotificationMap
    {
        public string UserId { get; set; }

        public int NotificationId { get; set; }

        public bool IsRead { get; set; }
    }
}