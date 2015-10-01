namespace ProjetoIntranet.Models
{
    public class DepartamentNews
    {
        public virtual News News { get; set; }
        public virtual Departament Departament { get; set; }
    }
}