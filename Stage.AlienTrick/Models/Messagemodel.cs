using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stage.AlienTrick.Models
{
    public class Messagemodel
    {
        public IEnumerable<PortalEntities.Message> Message { get; set; }
        public int MessageID { get; set; }
        public string MessageText { get; set; }
        public DateTime TimeSent { get; set; }
        public string ReceiverName { get; set; }
        public string SenderName { get; set; }


    }
}
