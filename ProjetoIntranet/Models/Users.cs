using System.Collections.Generic;
using System.Data.Entity;


namespace ProjetoIntranet.Models
{
    public class Users
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}