using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayered.Core.DTOs
{
    public class ProductDto : BaseDto
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        // category ve productfeature navigaiton propertiyleir almadık, çünkü bunları zaten cliente geri döndirmeyeceğiz, eğer alırsak boş yere client'e null veri gidecek.
        //ayrı bir Dto olacak zaten ProductWithCategory isimli.
    }
}
