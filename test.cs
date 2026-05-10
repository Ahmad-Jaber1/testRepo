using System;
using System.Collections.Generic;

namespace EmployeeBonusCalculator
{
    // Custom Exception Class
    public class InvalidEmployeeDataException : Exception
    {
        public InvalidEmployeeDataException(string message)
            : base(message)
        {
        }
    }

    // Employee Class
    public class Employee
    {
        public string Name { get; set; }
        public double BaseSalary { get; set; }
        public int PerformanceRating { get; set; }
        public int YearsOfService { get; set; }
        public bool IsManager { get; set; }

        public Employee(string name, double baseSalary, int performanceRating, int yearsOfService, bool isManager)
        {
            Name = name;
            BaseSalary = baseSalary;
            PerformanceRating = performanceRating;
            YearsOfService = yearsOfService;
            IsManager = isManager;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // List of mock employees
            List<Employee> employees = new List<Employee>()
            {
                new Employee("Alice", 50000, 5, 10, true),
                new Employee("Bob", 40000, 4, 5, false),
                new Employee("Charlie", 35000, 2, 3, false),
                new Employee("David", -45000, 5, 8, true),   // Invalid salary
                new Employee("Emma", 60000, 7, 6, false),   // Invalid rating
                new Employee("Frank", 70000, 3, 15, true)
            };

            // Process each employee
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

                    Console.WriteLine("=================================");
                    Console.WriteLine($"Employee: {employee.Name}");
                    Console.WriteLine($"Salary: {employee.BaseSalary:C}");
                    Console.WriteLine($"Rating: {employee.PerformanceRating}");
                    Console.WriteLine($"Years of Service: {employee.YearsOfService}");
                    Console.WriteLine($"Manager: {employee.IsManager}");
                    Console.WriteLine($"Final Bonus: {bonus:C}");
                }
                catch (InvalidEmployeeDataException ex)
                {
                    Console.WriteLine("=================================");
                    Console.WriteLine($"Error processing employee {employee.Name}: {ex.Message}");
                }
            }

            Console.WriteLine("\nProgram completed.");
        }

        // First overloaded method
        public static double CalculateBonus(double baseSalary, int performanceRating, int yearsOfService)
        {
            // Calls overloaded version with default manager status = false
            return CalculateBonus(baseSalary, performanceRating, yearsOfService, false);
        }

        // Second overloaded method
        public static double CalculateBonus(double baseSalary, int performanceRating, int yearsOfService, bool isManager)
        {
            // Validation
            if (baseSalary < 0)
            {
                throw new InvalidEmployeeDataException("Negative Salary is not allowed.");
            }

            if (performanceRating < 1 || performanceRating > 5)
            {
                throw new InvalidEmployeeDataException("Invalid Rating. Rating must be between 1 and 5.");
            }

            double bonus = 0;

            // Bonus calculation logic
            if (performanceRating < 3)
            {
                bonus = 0;
            }
            else if (performanceRating == 3 || performanceRating == 4)
            {
                // 5% base + 1% per year of service
                bonus = baseSalary * (0.05 + (0.01 * yearsOfService));
            }
            else if (performanceRating == 5)
            {
                // 10% base + 2% per year of service
                bonus = baseSalary * (0.10 + (0.02 * yearsOfService));
            }

            // Manager uplift
            if (isManager)
            {
                bonus *= 1.5;
            }

            // Bonus cap = 50% of salary
            double maxBonus = baseSalary * 0.50;

            if (bonus > maxBonus)
            {
                bonus = maxBonus;
            }

            return bonus;
        }
    }
}
