using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Shared.EventHandlers
{
    public static class EventNotification
    {
        public static Action<string> EventNotifier { get; set; }
    }
}
