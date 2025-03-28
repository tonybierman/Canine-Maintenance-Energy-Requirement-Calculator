namespace CanineMer
{
    public class MerCalculatorResultsVM
    {
        public string? LifeStage { get; set; }
        public double WeightInPounds { get; set; }
        public double LowerBounds { get; set; }
        public double UpperBounds { get; set; }
        public double Mean { get; set; }
        public double Rer { get; internal set; }

        public static MerCalculatorResultsVM Calculate(LifeStageFactorsEnum lifeStage, double weightInKgs)
        {
            if (weightInKgs <= 0)
                throw new ArgumentOutOfRangeException(nameof(weightInKgs), "Weight must be positive.");

            try
            {
                var (rer, lowerMer, upperMer) = CanineMerCalculator.CalculateMer(lifeStage, weightInKgs);
                double mean = (lowerMer + upperMer) / 2;

                return new MerCalculatorResultsVM
                {
                    LifeStage = lifeStage.ToString().AddSpacesToPascalCase(),
                    WeightInPounds = CanineMerCalculator.KgToLbs(weightInKgs),
                    LowerBounds = lowerMer,
                    UpperBounds = upperMer,
                    Rer = rer,
                    Mean = mean
                };
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException(nameof(weightInKgs), ex, "Invalid weight provided for calculation.");
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Invalid life stage: {lifeStage}", nameof(lifeStage), ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to calculate MER results.", ex);
            }
        }
    }
}