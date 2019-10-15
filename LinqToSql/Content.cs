using System;
using System.Linq;
using System.Data.Linq;

namespace LinqToSql
{
    class UsefullMethods
    {
        static string strConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyDb;Integrated Security=True";

        static DataContext db = new DataContext(strConnection);
        Table<Product> products = db.GetTable<Product>();

        public void Read()
        {
            foreach (var product in products)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}",product.Id, product.Name , product.Price ,  product.Manufacturer);
            }
            Console.ReadKey();
        }

        public void SortByPrice ()
        {
            var prods = products.OrderBy(p => p.Price);
            foreach (var product in prods)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", product.Id , product.Name , product.Price , product.Manufacturer);
            }
            Console.ReadKey();
        }

        public void GroupByManufacturer()
        {
            var gr = products.GroupBy(p => p.Manufacturer);
            foreach (var group in gr)
            {
                Console.WriteLine("Manufacturer: {0}", group.Key );
                {
                    foreach(var product in group)
                    {
                        Console.WriteLine("Name: {0}, Price: {1}", product.Name ,product.Price);
                    }
                }
            }
            Console.ReadKey();
        }

        public void ShowSomeItems (int number)
        { 
            var s = products.Take(number);
            foreach(var product in s)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t", product.Name, product.Price, product.Manufacturer);
            }
            Console.ReadKey();
        }

        public void ChangePrice( int id, int newPrice)
        {
            
            Product product =(Product) products.Where(p=>p.Id==id).SingleOrDefault();
            product.Price = newPrice;
            db.SubmitChanges();
        }

        public void AddProduct (string nameProduct , int price , string manufacturer)
        {
           
            Product product = new Product() { Name = nameProduct, Price = price, Manufacturer = manufacturer };
            products.InsertOnSubmit(product);
            db.SubmitChanges();
        }

        public void DeleteProduct (int id)
        {   
            Product product = products.Where(p => p.Id == id).FirstOrDefault();
            products.DeleteOnSubmit(product);
            db.SubmitChanges();
        }
    }

    
    public class SomeClass
    {
         static void Main(string[] args)
        {
        }
    }
}
