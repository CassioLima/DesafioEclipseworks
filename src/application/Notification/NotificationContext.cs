using Flunt.Notifications;

namespace Application
{
    public class NotificationContext : INotificationContext
    {
        private readonly List<Notification> _notifications;
        public IReadOnlyCollection<Notification> Notifications => _notifications;
        public bool HasNotifications => _notifications.Any();

        public NotificationContext()
        {
            _notifications = new List<Notification>();
        }

        public void AddNotification(string key, string message)
        {
            _notifications.Add(new Notification(key, message));
        }

        public void AddNotification(IReadOnlyCollection<Notification> errors)
        {
            foreach (var error in errors)
            {
                AddNotification(error.Key, error.Message);
            }
        }
    }
}
