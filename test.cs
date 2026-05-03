using System;

class Program
{
    // Intentional mistake for Objective 10:
    // Return type should be double, but it is int.
    static int CalculateBonus(double salary, int rating, int yearsOfService)
    {
        double bonus = 0;

        // Bonus logic
        if (rating < 3)
        {
            bonus = 0;
        }
        else if (rating >= 3)
        {
            bonus = salary * 0.05;

            if (yearsOfService > 5)
            {
                bonus += salary * 0.02;
            }

            if (rating == 5 && yearsOfService > 10)
            {
                bonus += 1000;
            }
        }

        return (int)bonus;
    }

    // Overloaded method
    static int CalculateBonus(double salary, int rating, int yearsOfService, bool isExecutive)
    {
        double bonus = CalculateBonus(salary, rating, yearsOfService);

        if (isExecutive)
        {
            bonus *= 1.5;
        }

        return (int)bonus;
    }

    static void Main(string[] args)
    {
        double[] salaries = { 50000, 70000, -1000, 90000, 120000 };
        int[] ratings = { 2, 5, 4, 6, 5 };
        int[] years = { 3, 12, 7, 8, -2 };
        bool[] executives = { false, true, false, true, false };

        for (int i = 0; i < salaries.Length; i++)
        {
            // Validation
            if (ratings[i] < 1 || ratings[i] > 5)
            {
                Console.WriteLine($"Employee {i + 1}: Invalid rating.");
                continue;
            }

            if (salaries[i] < 0)
            {
                Console.WriteLine($"Employee {i + 1}: Invalid salary.");
                continue;
            }

            if (years[i] < 0)
            {
                Console.WriteLine($"Employee {i + 1}: Invalid years of service.");
                continue;
            }

            int bonus;

            if (executives[i])
            {
                bonus = CalculateBonus(salaries[i], ratings[i], years[i], true);
            }
            else
            {
                bonus = CalculateBonus(salaries[i], ratings[i], years[i]);
            }

            Console.WriteLine($"Employee {i + 1} Bonus: ${bonus}");
        }
    }
}
