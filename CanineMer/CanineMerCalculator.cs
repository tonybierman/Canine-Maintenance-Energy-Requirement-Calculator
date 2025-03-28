namespace CanineMer
{
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

    public class CanineMerCalculator
    {
        private const double PoundsPerKilogram = 2.20462;

        public static double KgToLbs(double kilograms)
        {
            if (kilograms < 0)
                throw new ArgumentOutOfRangeException(nameof(kilograms), "Weight cannot be negative.");
            return kilograms * PoundsPerKilogram;
        }

        public static double LbsToKg(double pounds)
        {
            if (pounds < 0)
                throw new ArgumentOutOfRangeException(nameof(pounds), "Weight cannot be negative.");
            return pounds / PoundsPerKilogram;
        }

        public static double CalculateRer(double bodyWeightKg)
        {
            if (bodyWeightKg <= 0)
                throw new ArgumentOutOfRangeException(nameof(bodyWeightKg), "Body weight must be positive.");
            return 70 * Math.Pow(bodyWeightKg, 0.75); // Simplified from nested Sqrt
        }

        public static (double rer, double lowerMer, double upperMer) CalculateMer(LifeStageFactorsEnum lifeStage, double bodyWeightKg)
        {
            double rer = CalculateRer(bodyWeightKg);
            (double lowerFactor, double upperFactor) = GetMerRange(lifeStage);
            return (rer, rer * lowerFactor, rer * upperFactor);
        }

        public static (double lowerFactor, double upperFactor) GetMerRange(LifeStageFactorsEnum lifeStage)
        {
            switch (lifeStage)
            {
                case LifeStageFactorsEnum.NeuteredAdult: // 1.4–1.6: Typical for neutered adults
                case LifeStageFactorsEnum.IntactAdult:   // 1.4–1.6: Slightly low for intact, but combined for simplicity
                    return (1.4, 1.6);
                case LifeStageFactorsEnum.InactiveObeseProne: // 1.2–1.4: Reduced for obesity management
                    return (1.2, 1.4);
                case LifeStageFactorsEnum.WeightLoss: // 1.0: Restricted for weight loss
                    return (1.0, 1.0);
                case LifeStageFactorsEnum.WeightGain: // 1.2–1.8: Increased for growth
                    return (1.2, 1.8);
                case LifeStageFactorsEnum.ActiveWorkingDog: // 2.0–5.0: Wide range for high activity
                    return (2.0, 5.0);
                case LifeStageFactorsEnum.Puppy0To4Months: // 3.0: High for early growth
                    return (3.0, 3.0);
                case LifeStageFactorsEnum.Puppy4MonthsToAdult: // 1.5–2.0: Transition to adult needs
                    return (1.5, 2.0);
                case LifeStageFactorsEnum.Gestation: // 1.6–2.0: Increased for pregnancy
                    return (1.6, 2.0);
                case LifeStageFactorsEnum.Lactation: // 2.0–5.0: High for milk production
                    return (2.0, 5.0);
                default:
                    throw new ArgumentException($"Invalid life stage: {lifeStage}", nameof(lifeStage));
            }
        }

        public static double CalculateMeanMer(LifeStageFactorsEnum lifeStage, double bodyWeightKg)
        {
            (double rer, double lowerMer, double upperMer) = CalculateMer(lifeStage, bodyWeightKg);
            return (lowerMer + upperMer) / 2;
        }
    }
}