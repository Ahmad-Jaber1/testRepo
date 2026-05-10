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

    // Employee Class
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public int YearsOfService { get; set; }
        public int PerformanceRating { get; set; }
        public double Salary { get; set; }
        public double CalculatedBonus { get; set; }
        public bool IsExecutive { get; set; }
    }

    class Program
    {
        // Primary CalculateBonus Method
        public static double CalculateBonus(int yearsOfService, int performanceRating, double salary)
        {
            // Validation
            if (salary <= 0 || yearsOfService < 0 || performanceRating < 1 || performanceRating > 5)
            {
                throw new InvalidEmployeeDataException(
                    "Invalid employee data: Salary must be positive, YearsOfService cannot be negative, and PerformanceRating must be between 1 and 5."
                );
            }

            double bonusPercentage = 0.0;

            // Bonus Percentage Logic
            if (performanceRating < 3)
            {
                bonusPercentage = 0.0;
            }
            else if (performanceRating == 3)
            {
                bonusPercentage = 0.05;
            }
            else if (performanceRating == 4)
            {
                bonusPercentage = 0.10;
            }
            else if (performanceRating == 5)
            {
                bonusPercentage = 0.15;
            }

            // Base Bonus
            double bonus = salary * bonusPercentage;

            // Tenure Multiplier
            if (yearsOfService > 10)
            {
                bonus *= 1.5;
            }
            else if (yearsOfService > 5)
            {
                bonus *= 1.2;
            }

            return bonus;
        }

        // Overloaded CalculateBonus Method
        public static double CalculateBonus(int yearsOfService, int performanceRating, double salary, bool isExecutive)
        {
            double bonus = CalculateBonus(yearsOfService, performanceRating, salary);

            if (isExecutive)
            {
                bonus += 1000;
            }

            return bonus;
        }

        static void Main(string[] args)
        {
            // Employee Collection
            List<Employee> employees = new List<Employee>()
            {
                new Employee
                {
                    EmployeeId = 1,
                    Name = "Alice",
                    YearsOfService = 3,
                    PerformanceRating = 3,
                    Salary = 50000,
                    IsExecutive = false
                },

                new Employee
                {
                    EmployeeId = 2,
                    Name = "Bob",
                    YearsOfService = 7,
                    PerformanceRating = 4,
                    Salary = 70000,
                    IsExecutive = true
                },

                new Employee
                {
                    EmployeeId = 3,
                    Name = "Charlie",
                    YearsOfService = 12,
                    PerformanceRating = 5,
                    Salary = 90000,
                    IsExecutive = false
                },

                new Employee
                {
                    EmployeeId = 4,
                    Name = "David",
                    YearsOfService = 2,
                    PerformanceRating = 6, // Invalid Rating
                    Salary = 45000,
                    IsExecutive = false
                },

                new Employee
                {
                    EmployeeId = 5,
                    Name = "Eva",
                    YearsOfService = 4,
                    PerformanceRating = 4,
                    Salary = -30000, // Invalid Salary
                    IsExecutive = true
                }
            };

            // Process Employees
            foreach (Employee employee in employees)
            {
                try
                {
                    employee.CalculatedBonus = CalculateBonus(
                        employee.YearsOfService,
                        employee.PerformanceRating,
                        employee.Salary,
                        employee.IsExecutive
                    );

                    Console.WriteLine(
                        $"Employee: {employee.Name}, Bonus: ${employee.CalculatedBonus:F2}"
                    );
                }
                catch (InvalidEmployeeDataException ex)
                {
                    Console.WriteLine(
                        $"Error processing employee {employee.Name}: {ex.Message}"
                    );
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"Unexpected error for employee {employee.Name}: {ex.Message}"
                    );
                }
            }

            Console.ReadLine();
        }
    }
}
```

