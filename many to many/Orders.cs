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
        Table<Order> orders = db.GetTable<Order>();
        public void JoinSome ()
        {
            var result = from o in orders
                         join p in products on o.ProductId equals p.Id
                         join c in customers on o.CustomerId equals c.Id
                         select new { NameCustomer = c.Name, NameProduct = p.Name, Count = o.Count ,TimeOrder = o.TimeOrder };

            foreach (var order in result)
            {
                Console.WriteLine("NameCustomer: {0} , NameProduct: {1} ,Count = {2} ,TimeOrder = {3}",
                                 order.NameCustomer, order.NameProduct, order.Count, order.TimeOrder);
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
                Console.WriteLine("{0}\t{1}", customer.Id, customer.Name);
            }
            Console.ReadKey();
        }
        public void ShowOrders ()
        {
            foreach(var order in orders)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}",order.Id , order.CustomerId ,order.ProductId,order.Count);
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

        public void AddCustomer(string nameCustomer)
        {

            Customer customer = new Customer() { Name = nameCustomer};
            customers.InsertOnSubmit(customer);
            db.SubmitChanges();
        }
        public void AddOrder (int IdCust , int IdProd , int count, string timeOrder)
        {
            Order order = new Order() { CustomerId = IdCust, ProductId = IdProd, Count = count ,TimeOrder = timeOrder };
            orders.InsertOnSubmit(order);
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
        public void DeleteOrder(int id)
        {
            Order order = orders.Where(p => p.Id == id).FirstOrDefault();
            orders.DeleteOnSubmit(order);
            db.SubmitChanges();
        }

    }
}
