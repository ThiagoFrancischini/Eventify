using Eventify.Core.Entities.Eventify.Services.Notifications;
using Eventify.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Core.Services
{
    public class NotificationService : INotificationService
    {
        public event Action<NotificationMessage> OnShow;

        public void ShowSuccess(string message)
        {
            Show(message, "success");
        }

        public void ShowError(string message)
        {
            Show(message, "error");
        }

        private void Show(string message, string type)
        {
            var notification = new NotificationMessage(message, type);
            OnShow?.Invoke(notification);
        }
    }
}
