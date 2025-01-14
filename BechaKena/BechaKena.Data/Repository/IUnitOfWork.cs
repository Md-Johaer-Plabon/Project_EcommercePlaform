using BechaKena.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BechaKena.Data.Repository
{
    public interface IUnitOfWork : ICategoryRepository
    {
        ICategoryRepository Category { get; }
    }
}
