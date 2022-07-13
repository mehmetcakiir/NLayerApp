using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }

        //Base class ta Id olarak tanım olduğu için CategoryId yi ForenKey olarak algılanır. Özellikle belirtmeye gerek kalmaz
        public int CategoryId { get; set; }

        //[ForeignKey("CategoryId")] yazmaya gerek yok
        public Category Category { get; set; }


        public ProductFeature ProductFeature { get; set; }
    }
}
