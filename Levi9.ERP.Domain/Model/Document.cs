using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.Domain.Model
{
    public class Document
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        [StringLength(18), MinLength(18)]
        public string LastUpdate { get; set; }
        [StringLength(20)]
        public string DocumentType { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public List<ProductDocument> ProductDocuments { get; set; }
    }
}
