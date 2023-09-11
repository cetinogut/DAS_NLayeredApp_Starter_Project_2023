using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayered.Core
{
    public class ProductFeature // bu producta bağlı olacağından buna bir daha ilave bir BaseEntity vermeye gerek yok. sadece Id tanımlamak yeterli Created ve Updated date zaten Product ile aynı oalcaktır.
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
