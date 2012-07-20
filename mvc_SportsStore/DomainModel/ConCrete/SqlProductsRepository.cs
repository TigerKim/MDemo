using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using DomainModel.Abstract;
using DomainModel.Entities;

namespace DomainModel.ConCrete
{
    public class SqlProductsRepository : IProductsRepository
    {
        private Table<Product> productsTable;
        public SqlProductsRepository(string connectionString)
        {
            productsTable = (new DataContext(connectionString)).GetTable<Product>();
        }
        public IQueryable<Product> Products
        {
            get { return productsTable; }
        }
    }
}
