using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoIntranet.ViewModel
{
    public class DepartamentSelectViewModel
    {
        public int DepartamentId { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}