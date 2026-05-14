```csharp
using System;
using System.Collections.Generic;

namespace EmployeeBonusCalculator
{
    // Custom Exception
    public class InvalidEmployeeDataException : Exception
    {
        public InvalidEmployeeDataException(string message) : base(message)
        {
        }
    }

    // Employee Model
    public class Employee
    {
        public string Name { get; set; }
        public double BaseSalary { get; set; }
        public int PerformanceRating { get; set; }
        public int YearsOfService { get; set; }
        public bool IsManager { get; set; }
    }

    class Program
    {
        // Primary Method
        public static double CalculateBonus(double baseSalary, int performanceRating, int yearsOfService)
        {
            // Validation
            if (baseSalary < 0)
            {
                throw new InvalidEmployeeDataException("Salary cannot be negative.");
            }

            if (performanceRating < 1 || performanceRating > 5)
            {
                throw new InvalidEmployeeDataException("Performance rating must be between 1 and 5.");
            }

            double bonus = 0;

            // Tiered Logic
            if (performanceRating < 3)
            {
                bonus = 0;
            }
            else if (performanceRating == 3 || performanceRating == 4)
            {
                double percentage = 0.05 + (0.01 * yearsOfService);
                bonus = baseSalary * percentage;
            }
            else if (performanceRating == 5)
            {
                double percentage = 0.10 + (0.02 * yearsOfService);
                bonus = baseSalary * percentage;
            }

            // Apply 50% cap
            double maxBonus = baseSalary * 0.50;

            if (bonus > maxBonus)
            {
                bonus = maxBonus;
            }

            return bonus;
        }

        // Overloaded Method
        public static double CalculateBonus(double baseSalary, int performanceRating, int yearsOfService, bool isManager)
        {
            double bonus = CalculateBonus(baseSalary, performanceRating, yearsOfService);

            if (isManager)
            {
                bonus *= 1.5;

                // Re-apply cap after manager bump
                double maxBonus = baseSalary * 0.50;

                if (bonus > maxBonus)
                {
                    bonus = maxBonus;
                }
            }

            return bonus;
        }

        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee
                {
                    Name = "Alice",
                    BaseSalary = 50000,
                    PerformanceRating = 5,
                    YearsOfService = 5,
                    IsManager = true
                },
                new Employee
                {
                    Name = "Bob",
                    BaseSalary = 40000,
                    PerformanceRating = 4,
                    YearsOfService = 3,
                    IsManager = false
                },
                new Employee
                {
                    Name = "Charlie",
                    BaseSalary = 35000,
                    PerformanceRating = 2,
                    YearsOfService = 1,
                    IsManager = false
                },
                new Employee
                {
                    Name = "David",
                    BaseSalary = -45000,
                    PerformanceRating = 5,
                    YearsOfService = 4,
                    IsManager = true
                },
                new Employee
                {
                    Name = "Eva",
                    BaseSalary = 60000,
                    PerformanceRating = 6,
                    YearsOfService = 10,
                    IsManager = false
                }
            };

            foreach (Employee employee in employees)
            {
                try
                {
                    double bonus = CalculateBonus(
                        employee.BaseSalary,
                        employee.PerformanceRating,
                        employee.YearsOfService,
                        employee.IsManager
                    );

                    Console.WriteLine("Employee: " + employee.Name);
                    Console.WriteLine("Salary: $" + employee.BaseSalary);
                    Console.WriteLine("Rating: " + employee.PerformanceRating);
                    Console.WriteLine("Years of Service: " + employee.YearsOfService);
                    Console.WriteLine("Manager: " + employee.IsManager);
                    Console.WriteLine("Calculated Bonus: $" + bonus);
                    Console.WriteLine(new string('-', 40));
                }
                catch (InvalidEmployeeDataException ex)
                {
                    Console.WriteLine($"Error processing employee {employee.Name}: {ex.Message}");
                    Console.WriteLine(new string('-', 40));
                }
            }

            Console.ReadLine();
        }
    }
}
```

