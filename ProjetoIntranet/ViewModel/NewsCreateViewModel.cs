using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoIntranet.ViewModel
{
    public class NewsCreateViewModel
    {
        public int Amount { get; set; }
        public IEnumerable<DepartamentSelectViewModel> Departaments { get; set; }
    }
}