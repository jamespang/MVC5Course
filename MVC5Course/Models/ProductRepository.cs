using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {
        internal Product Find(int? id)
        {
            //throw new NotImplementedException();
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }
    }

    public  interface IProductRepository : IRepository<Product>
	{

	}
}