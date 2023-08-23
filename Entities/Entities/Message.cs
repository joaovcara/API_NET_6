using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("TB_MESSAGE")]
    public class Message : Notify
    {
        [Column("MSG_ID")]
        public int Id { get; set; }

        [Column("MSG_TITLE")]
        public string Title { get; set; }

        [Column("MSG_ACTIVE")]
        public bool Active { get; set; }

        [Column("MSG_DATEREGISTER")]
        public DateTime DateRegister { get; set; }

        [Column("MSG_DATEUPDATE")]
        public DateTime DateUpdate { get; set; }

        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
