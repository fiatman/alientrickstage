namespace Stage.AlienTrick
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Message
    {
        public int MessageID { get; set; }

        public string MessageText { get; set; }

        public DateTime? TimeSent { get; set; }

        public int? Receiver_ID { get; set; }

        public int? Sender_ID { get; set; }

        public string SenderName { get; set; }

        public string ReceiverName { get; set; }

        public virtual Student Student { get; set; }

        public virtual Student Student1 { get; set; }
    }
}
