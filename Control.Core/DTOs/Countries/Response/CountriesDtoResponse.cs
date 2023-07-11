using Control.Domain.Entities;
using System.Collections.Generic;

namespace Control.Core.DTOs.Countries.Response
{
    public class CountriesDtoResponse
    {
        public int IdCountry { get; set; }
        public string NameCountry { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<StatesDtoResponse> States { get; set; }
    }

}
