using System;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    class Testing
    {
        static void Main(string[] args)
        {
            Operations obj = new Operations();
            obj.CreateTable();
            obj.AddUser("Vika", 15, "wno3daa");
            obj.AddUser("Tom", 52, "youDream");
            obj.ShowTable();
            obj.DeleteTable();
            obj.RenameColumn("Age", "AgeUser");
        }
    }
    class Operations
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyDb;Integrated Security=True";

        public void CreateTable()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                string sqlExpression = "create table Users " +
                                 "( Id INT IDENTITY PRIMARY KEY," +
                                 " FirstName nchar(20)," +
                                 " Age int," +
                                 " Password nchar(20) )";

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();

            }

        }
        public void AddUser(string name, int age, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlExpression = "insert into Users (FirstName, Age, Password)" +
                                       "values (@name, @age, @password)";

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter nameParam = new SqlParameter("@name", name);
                command.Parameters.Add(nameParam);
                SqlParameter ageParam = new SqlParameter("@age", age);
                command.Parameters.Add(ageParam);
                SqlParameter passwordParam = new SqlParameter("@password", password);
                command.Parameters.Add(passwordParam);

                command.ExecuteNonQuery();
            }
        }
        public void ShowTable()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlExpression = "select * from Users";

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                for (int i = 0; i < reader.FieldCount; i++) //выводит названия столбиков
                {
                    Console.Write($"{reader.GetName(i)}" + "\t");
                }
                Console.WriteLine();

                while (reader.Read())
                {
                    //not exactly
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write($"{reader.GetValue(i)}" + "\t");

                    }
                    Console.WriteLine();
                    Console.ReadKey();
                }
            }
        }
        public void RenameColumn(string oldName, string newName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string str = "Users." + oldName;
                string sqlExpression = "EXEC sp_RENAME @oldName , @newName , 'column'";
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter oldNameParam = new SqlParameter("@oldName", str);
                command.Parameters.Add(oldNameParam);
                SqlParameter newNameParam = new SqlParameter("@newName", newName);
                command.Parameters.Add(newNameParam);

                command.ExecuteNonQuery();
            }
        }
        public void DeleteTable()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlExpression = "drop table Users";

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();

            }
        }

    }
}
