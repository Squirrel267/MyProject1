
namespace LinqToSql { 

    public class SomeClass
    {
         static void Main(string[] args)
        {
            UsefullMethods ob = new UsefullMethods();
            ob.AddCustomer("Nina", 1);
            ob.AddProduct("Orange", 20, "Voronezh");

            ob.JoinCustomerAndProduct();
            ob.GroupByCustomers();
            
            ob.ShowCustomers();
            ob.ShowProducts();
                
        }
    }
}
