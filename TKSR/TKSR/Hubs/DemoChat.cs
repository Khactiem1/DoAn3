using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
namespace Demochat.Hubs
{
    [HubName("chat")]
    public class DemoChat : Hub
    {
        public void Message()
        {
            Clients.All.message();
        }
    }
}