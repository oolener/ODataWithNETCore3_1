using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.oData.Models
{
    public class Storage
    {
        [Key]
        public Guid Uuid { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

    }
}
