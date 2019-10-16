using System;
using System.Linq;
using System.Data.Linq;

namespace LinqToSql
{
    public class UsefullMethods
    {
        static string strConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyDb;Integrated Security=True";

        static DataContext db = new DataContext(strConnection);
        Table<Product> products = db.GetTable<Product>();
        Table<Customer> customers = db.GetTable<Customer>();
        public void JoinCustomerAndProduct()
        {
            var result = from pr in products
                         join c in customers on pr.Id equals c.ProductsId
                         select new { NameCustomer = c.Name, NameProduct = pr.Name, Price = pr.Price, Manufacturer = pr.Manufacturer };

            foreach (var group in result)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", group.NameCustomer, group.NameProduct, group.Price, group.Manufacturer);
            }
            Console.ReadKey();

        }
        public void GroupByCustomers()
        {
            var result = customers.GroupJoin(products, c => c.ProductsId, p => p.Id, (cust, prod) => new
            {
                NameCustomers = cust.Name,
                NameProducts = prod.Select(p => p.Name)

            });

            foreach (var cust in result)
            {
                Console.WriteLine(cust.NameCustomers);
                foreach (string product in cust.NameProducts)
                {
                    Console.WriteLine(product);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        public bool SomethingMoreExpensive(int maxPrice)
        {
            bool result = products.Any(p => p.Price > maxPrice);
            return result;
        }

        public void ShowProducts()
        {
            foreach (var product in products)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", product.Id, product.Name, product.Price, product.Manufacturer);
            }
            Console.ReadKey();
        }

        public void ShowCustomers()
        {
            foreach (var customer in customers)
            {
                Console.WriteLine("{0}\t{1}\t{2}", customer.Id, customer.Name, customer.ProductsId);
            }
            Console.ReadKey();
        }

        public void SortByPrice()
        {
            var prods = products.OrderBy(p => p.Price);
            foreach (var product in prods)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}", product.Id, product.Name, product.Price, product.Manufacturer);
            }
            Console.ReadKey();
        }

        public void ChangePrice(int id, int newPrice)
        {

            Product product = (Product)products.Where(p => p.Id == id).SingleOrDefault();
            product.Price = newPrice;
            db.SubmitChanges();
        }

        public void AddProduct(string nameProduct, int price, string manufacturer)
        {

            Product product = new Product() { Name = nameProduct, Price = price, Manufacturer = manufacturer };
            products.InsertOnSubmit(product);
            db.SubmitChanges();
        }

        public void AddCustomer(string nameCustomer, int productId)
        {

            Customer customer = new Customer() { Name = nameCustomer, ProductsId = productId };
            customers.InsertOnSubmit(customer);
            db.SubmitChanges();
        }

        public void DeleteProduct(int id)
        {
            Product product = products.Where(p => p.Id == id).FirstOrDefault();
            products.DeleteOnSubmit(product);
            db.SubmitChanges();
        }

        public void DeleteCustomer(int id)
        {
            Customer customer = customers.Where(p => p.Id == id).FirstOrDefault();
            customers.DeleteOnSubmit(customer);
            db.SubmitChanges();
        }

    }
}
