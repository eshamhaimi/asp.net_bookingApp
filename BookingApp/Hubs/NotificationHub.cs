using BookingApp.Models;
using BookingApp.Models.AppModel;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace BookingApp.Hubs
{
    [HubName("notifications")]
    public class NotificationHub : Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();

        private static Dictionary<string, string> Introduced = new Dictionary<string, string>();
        private List<Accommodation> ApprovedList = new List<Accommodation>();

        private BAContext db = new BAContext();

        private Thread t;

        public NotificationHub()
        {
            ApprovedList = this.db.Accommodations.Where(x => x.Approved).ToList();
        }

        public static void NotifyManager(string accommodationName, string username)
        {
            if (Introduced.ContainsValue(username))
            {
                hubContext.Clients.Client(Introduced.FirstOrDefault(x => x.Value == username).Key).NotifyManager(accommodationName);
            }
        }
        public void Introduce(string username, string role)
        {
            if (Introduced.Count == 0)
            {
                Introduced.Add(Context.ConnectionId, username);
                Groups.Add(Context.ConnectionId, role);
                this.t = new Thread(AdminLoop);
                this.t.Start();
            }
            else
            {
                Introduced.Add(Context.ConnectionId, username);
                Groups.Add(Context.ConnectionId, role);
            }
        }

        private void AdminLoop()
        {
            while (true)
            {
               Thread.Sleep(2000);
               this.CheckAcc();
            }
        }

        public void CheckAcc()
        {
            var forApprove = this.db.Accommodations.Where(x => x.Approved == false).ToList();
            if (forApprove.Count != 0)
            {
                Clients.Group("Admin").ApprovedAccomodations(forApprove.Count);
            }
      
        }

        public static void Notify(int clickCount)
        {
            hubContext.Clients.Group("Admins").clickNotification($"Clicks: {clickCount}");
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            Introduced.Remove(Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }
    }
}