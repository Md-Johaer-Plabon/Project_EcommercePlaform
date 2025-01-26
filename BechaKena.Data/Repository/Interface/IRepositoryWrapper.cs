using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BechaKena.Data.Repository.Interface
{
    public interface IRepositoryWrapper
    {
        ICategoryRepository Category { get; }
        ICoverTypeRepository CoverType { get; }
        IProductRepository Product { get; }
		ICompanyRepository Company { get; }
		IShoppingCartRepository ShoppingCart { get; }
		IApplicationUserRepository ApplicationUser { get; }
		IOrderDetailRepository OrderDetail { get; }
		IOrderHeaderRepository OrderHeader { get; }
		void Save();
    }
}
