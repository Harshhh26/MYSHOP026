using MYSHOP.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MYSHOP.CORE.ViewModels;

namespace MYSHOP.CORE.ViewModels
{
    public  class ProductManagerViewModel
    {
        public Product Product { get; set; }

        public IEnumerable<ProductCategory> productCategories { get; set; }
    }
}
