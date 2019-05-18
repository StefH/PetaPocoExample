using PetaPocoExample.Net472.Entities;
using System.Linq;

namespace PetaPocoExample.Net472
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Northwind();

            var orders = db.Query<Order>("").ToArray();

            var sql = PetaPoco.Sql.Builder
                .Append("SELECT o.*, e.*")
                .Append("FROM Orders as o")
                .Append("LEFT JOIN Employees as e ON e.EmployeeID = o.EmployeeID");

            var ordersJoinedWithEmployees = db.Query<Order, Employee, (Order, Employee)>((order, employee) => (order, employee), sql).ToList();


            var sql2 = PetaPoco.Sql.Builder
                .Append("SELECT o.*, e.*")
                .From("Orders as o")
                .LeftJoin("Employees as e").On("e.EmployeeID = o.EmployeeID");

            var ordersJoinedWithEmployees2 = db.Query<Order, Employee, (Order, Employee)>((order, employee) => (order, employee), sql2).ToList();

            int x = 0;
        }
    }
}