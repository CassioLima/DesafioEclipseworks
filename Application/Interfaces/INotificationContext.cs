using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface INotificationContext
    {
        bool HasNotifications { get; }
        IReadOnlyCollection<Notification> Notifications { get; }

        void AddNotification(IReadOnlyCollection<Notification> errors);
        void AddNotification(string key, string message);
    }
}
