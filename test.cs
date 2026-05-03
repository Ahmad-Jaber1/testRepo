using System;

class Program
{
    // Primary method (intentional mistake: return type is double instead of decimal)
    static double CalculateRestockFee(decimal itemPrice, int conditionScore, int daysSincePurchase, bool isLoyaltyMember)
    {
        // Validation
        if (conditionScore < 0 || conditionScore > 100)
        {
            throw new ArgumentException("Condition score must be between 0 and 100.");
        }

        if (daysSincePurchase < 0)
        {
            throw new ArgumentException("Days since purchase cannot be negative.");
        }

        decimal feePercentage;

        // Base fee logic
        if (daysSincePurchase <= 7)
        {
            feePercentage = 0m;
        }
        else if (daysSincePurchase <= 30)
        {
            feePercentage = 0.15m;
        }
        else
        {
            feePercentage = 0.25m;
        }

        // Condition penalty
        if (conditionScore < 50)
        {
            feePercentage += 0.10m;
        }

        // Calculate fee
        decimal finalFee = itemPrice * feePercentage;

        // Loyalty discount
        if (isLoyaltyMember)
        {
            finalFee -= finalFee * 0.05m;
        }

        return (double)finalFee;
    }

    // Overloaded method
    static double CalculateRestockFee(decimal itemPrice, int conditionScore, int daysSincePurchase)
    {
        return CalculateRestockFee(itemPrice, conditionScore, daysSincePurchase, false);
    }

    static void Main(string[] args)
    {
        // Valid test case with loyalty member
        try
        {
            double fee1 = CalculateRestockFee(200m, 80, 20, true);
            Console.WriteLine($"Fee 1: {fee1}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        // Valid test case using overloaded method
        try
        {
            double fee2 = CalculateRestockFee(150m, 40, 40);
            Console.WriteLine($"Fee 2: {fee2}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        // Invalid condition score
        try
        {
            double fee3 = CalculateRestockFee(100m, 120, 10, false);
            Console.WriteLine($"Fee 3: {fee3}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        // Invalid days since purchase
        try
        {
            double fee4 = CalculateRestockFee(100m, 70, -5, true);
            Console.WriteLine($"Fee 4: {fee4}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
