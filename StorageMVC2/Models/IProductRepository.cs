using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageMVC2.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProduct { get; }

        int TotalValue();
    }
}
