using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Entities;

namespace DomainModel.Abstract
{
    public interface IProductsRespository 
    {
        IQueryable<Product> Products { get; }
    }
}
