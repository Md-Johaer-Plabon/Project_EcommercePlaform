using BechaKena.Data.Data;
using BechaKena.Data.Repository.Interface;
using BechaKena.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BechaKena.Data.Repository
{
	public class CompanyRepository:Repository<Company>, ICompanyRepository
	{
		private ApplicationDbContext _db;

		public CompanyRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}


		public void Update(Company obj)
		{
			_db.Companies.Update(obj);
		}
	}
}
