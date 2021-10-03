using System;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using Secao17.Entities;

namespace Secao17
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Enter file full path: ");
                string path = Console.ReadLine();

                Console.Write("Enter salary: ");
                double salary = double.Parse(Console.ReadLine());

                Console.WriteLine();

                List<Employee> employees = new List<Employee>();

                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        while (!sr.EndOfStream)
                        {
                            string[] line = sr.ReadLine().Split(',');

                            employees.Add(new Employee(line[0], line[1], double.Parse(line[2], CultureInfo.InvariantCulture)));
                        }
                    }
                }

                /*var email =
                    from emp in employees
                    where emp.Salary > salary
                    orderby emp.Email
                    select emp.Email;*/
                var email = employees.Where(emp => emp.Salary > salary).Select(emp => emp.Email);
                Console.WriteLine($"Email of people whose salary is more than $ {salary.ToString("F2", CultureInfo.InvariantCulture)}:");
                
                foreach (string emp in email)
                {
                    Console.WriteLine(emp);
                }

                Console.WriteLine();

                var sum =
                    (from emp in employees
                     where emp.Name[0] == 'M'
                     select emp.Salary)
                    .Aggregate(0.0, (x, y) => x + y);
                
                Console.WriteLine($"Sum of salary of people whose name starts with 'M': {sum.ToString("F2", CultureInfo.InvariantCulture)}");
            } 
            catch (IOException e)
            {
                Console.WriteLine("An error ocurred: " + e.Message);
            } 
            catch (Exception e)
            {
                Console.WriteLine("Unexpected error: " + e.Message);
            }
        }
    }
}
