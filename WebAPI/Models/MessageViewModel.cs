using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class MessageViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Active { get; set; }

        public DateTime DateRegister { get; set; }

        public DateTime DateUpdate { get; set; }

        public string UserId { get; set; }
    }
}
