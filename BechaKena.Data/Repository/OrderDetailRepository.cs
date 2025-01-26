using BechaKena.Data.Data;
using BechaKena.Data.Repository.Interface;
using BechaKena.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BechaKena.Data.Repository
{
	public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
	{
		private ApplicationDbContext _db;

		public OrderDetailRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}


		public void Update(OrderDetail obj)
		{
			_db.OrderDetail.Update(obj);
		}
	}
}
