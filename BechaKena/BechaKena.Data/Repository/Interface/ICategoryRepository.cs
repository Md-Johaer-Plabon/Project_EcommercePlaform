﻿using BechaKena.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BechaKena.Data.Repository.Interface
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
    }
}