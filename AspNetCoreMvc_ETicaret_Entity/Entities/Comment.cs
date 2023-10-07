using AspNetCoreMvc_ETicaret_Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wissen.Bright.BlogProject.App.Entity.Entities
{
    public class Comment : BaseEntity
    {
        public string Description { get; set; }
        public int UserId { get; set; }
        public int ProductsId { get; set; }
        [ForeignKey("ProductsId")]
        [InverseProperty("Comments")]
        public Products Products { get; set; }
    }
}
