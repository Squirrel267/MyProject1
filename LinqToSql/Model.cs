using System;
using System.Data.Linq.Mapping;

namespace LinqToSql
{
    [Table(Name ="Products")]
    public class Product
    {
        [Column(IsPrimaryKey = true,IsDbGenerated  = true)]
        public int Id { get; set; }
        [Column(Name = "NameProduct")]
        public string Name { get; set; }
        [Column(Name ="Price")]
        public int Price { get; set; }
        [Column]
        public string Manufacturer { get; set; }
    }
}
