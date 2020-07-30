using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG.Exam.OOP
{
    public interface IStringable
    {
        string ToString2();
    }

    public abstract class EntityBase
    {
        protected string GetTypeName()
        {
            return $"{this.GetType().Name}";
        }
    }

    public class Employee : EntityBase, IStringable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }

        public virtual string ToString2()
        {
            return $"{GetTypeName()}: {FirstName} {LastName} salary is {Salary}";
        }

    }

    public class SalesManager : Employee
    {
        public int BonusPerSale { get; set; }
        public int SalesThisMonth { get; set; }

        public override string ToString2()
        {
            return $"{GetTypeName()}: {FirstName} {LastName} salary is {Salary} and bonus is {SalesThisMonth * BonusPerSale}";
        }
    }

    public class CustomerServiceAgent : Employee
    {
        public int Customers { get; set; }

        public override string ToString2()
        {
            return $"{GetTypeName()}: {FirstName} {LastName} salary is {Salary}. Number of assigned customers: {Customers}";
        }
    }

    public class Dog : EntityBase, IStringable
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public string ToString2()
        {
            return $"{GetTypeName()}: {Name} is {Age} years old";
        }
    }
}
