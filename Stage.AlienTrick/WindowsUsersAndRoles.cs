namespace Stage.AlienTrick
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WindowsUsersAndRoles
    {
        public int ID { get; set; }

        public string WindowsUserAccount { get; set; }

        public byte IsStudent { get; set; }
        public byte IsAdmin { get; set; }
        public byte IsSupervisor { get; set; }

    


    }
}