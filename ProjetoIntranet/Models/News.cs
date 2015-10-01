using System;
using System.Collections.Generic;

namespace ProjetoIntranet.Models
{
    public class News
    {
        public News()
        {
            this.Departaments = new HashSet<Departament>();
        }

        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Departament> Departaments { get; set; }

    }
}