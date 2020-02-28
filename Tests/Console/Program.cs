using System;
using System.Linq;
using HumanResources.Common.Repositories;

namespace HumanResources.ConsoleTests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var employeePromotions = new EmployeeRepository();
            employeePromotions.Resolve();
            try
            {
                var employeeEntities = employeePromotions.Result.Select().ToList();
                employeeEntities.ForEach(e => Console.WriteLine(e.DateBirthday));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}