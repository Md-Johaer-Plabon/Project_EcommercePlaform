using BechaKena.Data.Data;
using BechaKena.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BechaKena.Data.Repository.Interface
{
	public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
	{
		private ApplicationDbContext _db;

		public ShoppingCartRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public int DecrementContents(ShoppingCart shoppingCart, int count)
		{
			shoppingCart.Count -= count;
			return shoppingCart.Count;
		}

		public int IncrementContents(ShoppingCart shoppingCart, int count)
		{
			shoppingCart.Count += count;
			return shoppingCart.Count;
		}
	}
}
