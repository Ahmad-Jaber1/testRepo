using System;

namespace InventoryRestockFeeCalculator
{
    public class InvalidRestockDataException : Exception
    {
        public InvalidRestockDataException(string message) : base(message)
        {
        }
    }

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

                double fee = CalculateRestockFee(
                    itemPrice,
                    conditionScore,
                    daysSincePurchase,
                    isLoyaltyMember);

                Console.WriteLine($"Fee 1: {fee:C}");
            }
            catch (InvalidRestockDataException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                double itemPrice = 150.0;
                int conditionScore = 5;
                int daysSincePurchase = 30;
                bool isLoyaltyMember = false;

                double fee = CalculateRestockFee(
                    itemPrice,
                    conditionScore,
                    daysSincePurchase,
                    isLoyaltyMember);

                Console.WriteLine($"Fee 2: {fee:C}");
            }
            catch (InvalidRestockDataException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                double itemPrice = 300.0;
                int conditionScore = 3;
                int daysSincePurchase = 45;
                bool isLoyaltyMember = true;

                double fee = CalculateRestockFee(
                    itemPrice,
                    conditionScore,
                    daysSincePurchase,
                    isLoyaltyMember);

                Console.WriteLine($"Fee 3: {fee:C}");
            }
            catch (InvalidRestockDataException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                double itemPrice = 100.0;
                int conditionScore = 7;
                int daysSincePurchase = -5;
                bool isLoyaltyMember = false;

                double fee = CalculateRestockFee(
                    itemPrice,
                    conditionScore,
                    daysSincePurchase,
                    isLoyaltyMember);

                Console.WriteLine($"Fee 4: {fee:C}");
            }
            catch (InvalidRestockDataException ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                double itemPrice = 250.0;
                int conditionScore = 6;
                int daysSincePurchase = -10;
                bool isLoyaltyMember = true;
                bool forceCalculation = true;

                double fee = CalculateRestockFee(
                    itemPrice,
                    conditionScore,
                    daysSincePurchase,
                    isLoyaltyMember,
                    forceCalculation);

                Console.WriteLine($"Fee 5: {fee:C}");
            }
            catch (InvalidRestockDataException ex)
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
            if (itemPrice <= 0)
            {
                throw new InvalidRestockDataException(
                    "Item price must be greater than zero.");
            }

            if (conditionScore < 1 || conditionScore > 10)
            {
                throw new InvalidRestockDataException(
                    "Condition score must be between 1 and 10.");
            }

            if (!forceCalculation && daysSincePurchase < 0)
            {
                throw new InvalidRestockDataException(
                    "Days since purchase cannot be negative.");
            }

            return ProcessFee(
                itemPrice,
                conditionScore,
                daysSincePurchase,
                isLoyaltyMember);
        }

        private static void ValidateData(
            double itemPrice,
            int conditionScore,
            int daysSincePurchase)
        {
            if (itemPrice <= 0)
            {
                throw new InvalidRestockDataException(
                    "Item price must be greater than zero.");
            }

            if (conditionScore < 1 || conditionScore > 10)
            {
                throw new InvalidRestockDataException(
                    "Condition score must be between 1 and 10.");
            }

            if (daysSincePurchase < 0)
            {
                throw new InvalidRestockDataException(
                    "Days since purchase cannot be negative.");
            }
        }

        private static double ProcessFee(
            double itemPrice,
            int conditionScore,
            int daysSincePurchase,
            bool isLoyaltyMember)
        {
            double feePercentage;

            if (daysSincePurchase > 30)
            {
                feePercentage = 0.0;
            }
            else if (conditionScore < 5)
            {
                feePercentage = 0.50;
            }
            else
            {
                feePercentage = 0.20;
            }

            double feeAmount = itemPrice * feePercentage;

            if (isLoyaltyMember)
            {
                feeAmount -= feeAmount * 0.10;
            }

            return feeAmount;
        }
    }
}
