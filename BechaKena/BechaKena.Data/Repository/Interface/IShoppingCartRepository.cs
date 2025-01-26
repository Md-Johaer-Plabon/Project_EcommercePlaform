using BechaKena.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BechaKena.Data.Repository.Interface
{
	public interface IShoppingCartRepository : IRepository<ShoppingCart>
	{
		int IncrementContents(ShoppingCart shoppingCart, int count);
		int DecrementContents(ShoppingCart shoppingCart, int count);
	}
}
