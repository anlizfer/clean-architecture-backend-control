using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Core.DTOs.DocumentType.Response
{
    public class DocumentTypeDtoResponse
    {
        public long IdDocumentType { get; set; }
        public string NameDocumentType { get; set; }
        public bool Status { get; set; }
    }
}
