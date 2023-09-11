using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayered.Core
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; } // navigation property, çünkü producttan catgorye gitmemizi sağlayacak

        public ProductFeature ProductFeature { get; set; } //navigation property, çünkü producttan eature'e gştmemizi sağlayacak
    }
}
