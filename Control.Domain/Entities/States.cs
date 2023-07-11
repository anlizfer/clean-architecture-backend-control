using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Domain.Entities
{
    public class States
    {
        public int IdState { get; set; }
        public string NameState { get; set; }
        
        public bool Status { get; set; }
        public int IdCountry { get; set; }
        public virtual Countries Countries { get; set; }
    }
}
