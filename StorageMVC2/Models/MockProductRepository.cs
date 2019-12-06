using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageMVC2.Models
{
    public class MockProductRepository:IProductRepository
    {

        public IEnumerable<Product> AllProduct =>
            new List<Product>
            {
                new Product {Id = 1, Name="sara", Price=15, Shelf ="7", Count=2,  Description="Lorem Ipsum"},
                new Product {Id = 2, Name="sahar", Price=15, Shelf ="8", Count=2,  Description="Lorem Ipsum"},
                new Product {Id = 3, Name="sona", Price=15, Shelf ="9", Count=2,  Description="Lorem Ipsum"},

            };

        public int TotalValue()
        {
            return AllProduct.Sum(item => item.Price);
        }
    }
}
