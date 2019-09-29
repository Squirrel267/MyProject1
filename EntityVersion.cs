using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EntityVersion
{ 
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class PersonContex : DbContext
    {
        public PersonContex() : base("StringConnection") { }

        public DbSet<Person> People { get; set; }
    }

    class Operations
    {
        public void AddPerson(string name, int age)
        {
            using (PersonContex pc = new PersonContex())
            {
                Person person = new Person { Name = name, Age = age };
                pc.People.Add(person);
                pc.SaveChanges();
            }
        }

        public void ShowPeople()
        {
            using (PersonContex pc = new PersonContex())
            {
                var people = pc.People.ToList();
                Console.WriteLine("Id" + "\t" + "Name" + "\t" + "Age");
                foreach (var p in people)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t", p.Id, p.Name, p.Age);
                }
                Console.ReadKey();
            }
        }

        public void UpdateObject (int idObject, string nameColumn, string newValue)
        {
            using (PersonContex pc = new PersonContex())
            {
                Person person = new Person() { Id = idObject };
                pc.People.Attach(person);

                if (nameColumn == "Name")
                {
                    person.Name = newValue;
                }

                if (nameColumn == "Age")
                {
                    int newAge = Int32.Parse(newValue);
                    person.Age = newAge;
                }
                pc.SaveChanges();


            }
        }

        public void DeletePerson(int idObject)
        {
            using (PersonContex pc = new PersonContex())
            {
                Person person = new Person() { Id = idObject };
                pc.People.Attach(person);
                pc.People.Remove(person);
                pc.SaveChanges();
            }
        }
    }
        class Testing
        {
            static void Main(string[] args)
            {
            }
        }
    
   
    
}
