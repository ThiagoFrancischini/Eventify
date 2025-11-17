using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Core.Entities
{
    namespace Eventify.Services.Notifications
    {
        public class NotificationMessage
        {
            public string Message { get; set; }
            public string CssClass { get; set; }
            public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(3);

            public NotificationMessage(string message, string cssClass)
            {
                Message = message;
                CssClass = cssClass;
            }
        }
    }
}
