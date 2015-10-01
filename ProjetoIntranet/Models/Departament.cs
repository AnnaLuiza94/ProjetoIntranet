using System.Collections.Generic;

namespace ProjetoIntranet.Models
{
    public class Departament
    {
        public Departament()
        {
            this.News = new HashSet<News>();
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<News>  News   { get; set; }
    }
}