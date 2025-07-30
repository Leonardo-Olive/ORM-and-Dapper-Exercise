using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);

            var repo = new DapperDepartmentRepository(conn);
            var departments = repo.GetAllDepartments();

            Console.WriteLine("Would you like to enter:");
            Console.WriteLine("1 - Departments");
            Console.WriteLine("2 - Products");

            var input = Convert.ToInt32(Console.ReadLine());

            if (input == 1)
            {


                foreach (var dept in departments)
                {
                    Console.Write($"{dept.DepartmentID} {dept.Name}");
                   
                }

                
            }

           var repos = new DapperProductRepository(conn);
          
            var prodList = repos.GetAllProducts();

            if (input == 2)
            {
                foreach (var prod in prodList)
                {
                    Console.WriteLine($"{prod.ProductID} - {prod.Name}");
                    
                }

                Console.WriteLine("Would you like to:");
                Console.WriteLine("1 - Update Product?");
                Console.WriteLine("2- Delete Product?");
                var answer = Convert.ToInt32(Console.ReadLine());
                if (answer == 1)
                {
                    Console.WriteLine("What is the Product ID you would like to update?");
                    var prodID = int.Parse(Console.ReadLine());
                    Console.WriteLine("What is the new Product name?");
                    var newName = Console.ReadLine();
                    repos.UpdateProduct(prodID, newName);
                }
                if (answer == 2)
                {
                    Console.WriteLine("What is the Product ID you would like to delete?");
                    var prodID = int.Parse(Console.ReadLine());
                    repos.DeleteProduct(prodID);
                }
            }
          
  
  
        }


    }
}
