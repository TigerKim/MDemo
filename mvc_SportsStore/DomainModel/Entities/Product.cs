using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace DomainModel.Entities
{
    // System.Data.Linq
    [Table(Name="Products")]
    public class Product //: IEnumerable
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int  ProductID { get; set; }
        [Column] 
        public string Name { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public decimal Price { get; set; }
        [Column]
        public string Catagory { get; set; }

        //public IEnumerator GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
