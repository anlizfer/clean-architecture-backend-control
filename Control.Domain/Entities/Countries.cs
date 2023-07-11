using System.Collections.Generic;

namespace Control.Domain.Entities
{
    public class Countries
    {
        public Countries()
        {
            States = new HashSet<States>();
        }
        public int IdCountry { get; set; }
        public string NameCountry { get; set; }
        public bool Status { get; set; }



        public virtual ICollection<States> States { get; set; }

    }
}
