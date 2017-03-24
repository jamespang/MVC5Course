using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {
        public override IQueryable<Product> All()
        {
            return base.All().Where(p => p.Stock > 500 && false == p.IsDeleted);
        }

        public IQueryable<Product> All(bool showAll)
        {
            //return base.All();
            if (showAll)
            {
                return base.All();
            }
            return this.All();
        }

        internal Product Find(int? id)
        {
            //throw new NotImplementedException();
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public override void Delete(Product entity)
        {
            base.Delete(entity);
            //this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            //entity.IsDeleted = true;
        }
    }

    public  interface IProductRepository : IRepository<Product>
	{

	}
}