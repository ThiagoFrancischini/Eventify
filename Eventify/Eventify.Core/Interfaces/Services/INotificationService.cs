using Eventify.Core.Entities.Eventify.Services.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Core.Interfaces.Services
{
    public interface INotificationService
    {
        event Action<NotificationMessage> OnShow;

        void ShowError(string message);
        void ShowSuccess(string message);
    }
}
