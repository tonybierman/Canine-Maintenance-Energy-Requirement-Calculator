namespace CanineMer
{
    public enum MealPlanTierEnum
    {
        Essential = 1,
        Balanced = 2,
        Performance = 3
    }

    public enum LifeStageFactorsEnum
    {
        None = 0,
        NeuteredAdult = 1,
        IntactAdult = 2,
        InactiveObeseProne = 3,
        WeightLoss = 4,
        WeightGain = 5,
        ActiveWorkingDog = 6,
        Puppy0To4Months = 7,
        Puppy4MonthsToAdult = 8,
        Gestation = 9,
        Lactation = 10
    }

    // https://todaysveterinarynurse.com/nutrition/veterinary-nutrition-math/
    public class CanineMerCalculator
    {
        private const double PoundsPerKilogram = 2.20462;

        // Convert kilograms to pounds
        public static double KgToLbs(double kilograms)
        {
            return kilograms * PoundsPerKilogram;
        }

        // Convert pounds to kilograms
        public static double LbsToKg(double pounds)
        {
            return pounds / PoundsPerKilogram;
        }

        // Calculate RER based on the dog's weight
        public static double CalculateRer(double bodyWeightKg)
        {
            return 70 * Math.Sqrt(Math.Sqrt(bodyWeightKg * bodyWeightKg * bodyWeightKg));
        }

        // Calculate MER based on life stage
        public static (double lowerMer, double upperMer) CalculateMer(LifeStageFactorsEnum lifeStage, double bodyWeightKg)
        {
            double rer = CalculateRer(bodyWeightKg);

            // Get MER range factors based on life stage
            (double lowerFactor, double upperFactor) = GetMerRange(lifeStage);

            // Calculate and return MER range
            return (rer * lowerFactor, rer * upperFactor);
        }

        // Adjusting MER based on life stage using LifeStageFactorsEnum
        public static (double lowerFactor, double upperFactor) GetMerRange(LifeStageFactorsEnum lifeStage)
        {
            switch (lifeStage)
            {
                case LifeStageFactorsEnum.NeuteredAdult:
                case LifeStageFactorsEnum.IntactAdult:
                    return (1.4, 1.6);
                case LifeStageFactorsEnum.InactiveObeseProne:
                    return (1.2, 1.4);
                case LifeStageFactorsEnum.WeightLoss:
                    return (1.0, 1.0);
                case LifeStageFactorsEnum.WeightGain:
                    return (1.2, 1.8);
                case LifeStageFactorsEnum.ActiveWorkingDog:
                    return (2.0, 5.0);
                case LifeStageFactorsEnum.Puppy0To4Months:
                    return (3.0, 3.0);
                case LifeStageFactorsEnum.Puppy4MonthsToAdult:
                    return (1.5, 2.0);
                case LifeStageFactorsEnum.Gestation:
                    return (1.6, 2.0);
                case LifeStageFactorsEnum.Lactation:
                    return (2.0, 5.0);
                default:
                    throw new ArgumentException("Invalid life stage provided.");
            }
        }

        // New function to calculate the mean of the MER range
        public static double CalculateMeanMer(LifeStageFactorsEnum lifeStage, double bodyWeightKg)
        {
            (double lowerMer, double upperMer) = CalculateMer(lifeStage, bodyWeightKg);
            return (lowerMer + upperMer) / 2;
        }
    }
}
