using MYSHOP.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYSHOP.CORE.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<ProductCategory> productCategories { get; set; }
    }
}
