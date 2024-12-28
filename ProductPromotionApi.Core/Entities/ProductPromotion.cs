using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductPromotionApi.Core.Entities
{
    public class ProductPromotion
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int PromotionId { get; set; }
        public Promotion Promotion { get; set; }
    }

}
