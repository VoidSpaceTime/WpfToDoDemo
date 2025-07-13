using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.Common.Events
{
    public class MessgaeModel
    {
        public string Message { get; set; }
        public string Filter { get; set; }
    }
    public class MessageEvent : PubSubEvent<MessgaeModel>
    {

    }
}
