```csharp id="yq482"
using System;

namespace InventoryRestockFeeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                double itemPrice = 200.0;
                int conditionScore = 4;
                int daysSincePurchase = 15;
                bool isLoyaltyMember = true;

                double fee1 = CalculateRestockFee(
                    itemPrice,
                    conditionScore,
                    daysSincePurchase,
                    isLoyaltyMember);

                Console.WriteLine($"Fee 1: {fee1:C}");

                double fee2 = CalculateRestockFee(
                    150.0,
                    5,
                    30,
                    false);

                Console.WriteLine($"Fee 2: {fee2:C}");

                double fee3 = CalculateRestockFee(
                    300.0,
                    3,
                    45,
                    true);

                Console.WriteLine($"Fee 3: {fee3:C}");

                double fee4 = CalculateRestockFee(
                    100.0,
                    7,
                    -5,
                    false);

                Console.WriteLine($"Fee 4: {fee4:C}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                double fee5 = CalculateRestockFee(
                    250.0,
                    6,
                    -10,
                    true,
                    true);

                Console.WriteLine($"Fee 5: {fee5:C}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static double CalculateRestockFee(
            double itemPrice,
            int conditionScore,
            int daysSincePurchase,
            bool isLoyaltyMember)
        {
            ValidateData(itemPrice, conditionScore, daysSincePurchase);

            return ProcessFee(
                itemPrice,
                conditionScore,
                daysSincePurchase,
                isLoyaltyMember);
        }

        public static double CalculateRestockFee(
            double itemPrice,
            int conditionScore,
            int daysSincePurchase,
            bool isLoyaltyMember,
            bool forceCalculation)
        {
            ValidateBasicData(itemPrice, conditionScore);

            if (!forceCalculation && daysSincePurchase < 0)
            {
                throw new Exception("Days cannot be negative.");
            }

            return ProcessFee(
                itemPrice,
                conditionScore,
                daysSincePurchase,
                isLoyaltyMember);
        }

        static void ValidateData(
            double itemPrice,
            int conditionScore,
            int daysSincePurchase)
        {
            ValidateBasicData(itemPrice, conditionScore);

            if (daysSincePurchase < 0)
            {
                throw new Exception("Days cannot be negative.");
            }
        }

        static void ValidateBasicData(
            double itemPrice,
            int conditionScore)
        {
            if (itemPrice <= 0)
            {
                throw new Exception("Price must be positive.");
            }

            if (conditionScore < 1 || conditionScore > 10)
            {
                throw new Exception("Condition score must be between 1 and 10.");
            }
        }

        static double ProcessFee(
            double itemPrice,
            int conditionScore,
            int daysSincePurchase,
            bool isLoyaltyMember)
        {
            double feePercentage = daysSincePurchase > 30
                ? 0.0
                : conditionScore < 5
                ? 0.50
                : 0.20;

            double feeAmount = itemPrice * feePercentage;

            feeAmount = isLoyaltyMember
                ? feeAmount - (feeAmount * 0.10)
                : feeAmount;

            return feeAmount;
        }
    }
}
```

