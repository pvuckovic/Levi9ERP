using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levi9.ERP.Domain.Models.DTO.DocumentDto
{
    public class DocumentSyncDTO
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; }
        public Guid ClientId { get; set; }
        public string DocumentType { get; set; }
        public string LastUpdate { get; set; }
        public List<DocumentItemSyncDTO> Items { get; set; }
    }
}
