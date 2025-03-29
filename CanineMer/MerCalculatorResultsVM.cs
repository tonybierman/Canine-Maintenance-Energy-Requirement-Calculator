namespace CanineMer
{
    /// <summary>
    /// Represents the results of a Maintenance Energy Requirement (MER) calculation for a canine,
    /// including life stage, weight, and energy bounds.
    /// </summary>
    public class MerCalculatorResultsVM
    {
        /// <summary>
        /// Gets or sets the life stage of the dog as a human-readable string (e.g., "Neutered Adult").
        /// </summary>
        public string? LifeStage { get; set; }

        /// <summary>
        /// Gets or sets the dog's weight in pounds.
        /// </summary>
        public double WeightInPounds { get; set; }

        /// <summary>
        /// Gets or sets the lower bound of the dog's Maintenance Energy Requirement (MER) in kilocalories per day.
        /// </summary>
        public double LowerBounds { get; set; }

        /// <summary>
        /// Gets or sets the upper bound of the dog's Maintenance Energy Requirement (MER) in kilocalories per day.
        /// </summary>
        public double UpperBounds { get; set; }

        /// <summary>
        /// Gets or sets the mean Maintenance Energy Requirement (MER) in kilocalories per day,
        /// calculated as the average of the lower and upper bounds.
        /// </summary>
        public double Mean { get; set; }

        /// <summary>
        /// Gets the Resting Energy Requirement (RER) in kilocalories per day.
        /// This property is set internally by the calculator.
        /// </summary>
        public double Rer { get; internal set; }

        public double? CupsLow { get; set; }    // Nullable to indicate optional
        public double? CupsHigh { get; set; }   // Nullable to indicate optional
        public double? CupsMean { get; set; }   // Nullable to indicate optional

        /// <summary>
        /// Calculates the Maintenance Energy Requirement (MER) results for a dog based on its life stage and weight.
        /// </summary>
        /// <param name="lifeStage">The life stage of the dog, determining the MER range factors.</param>
        /// <param name="weightInKgs">The dog's weight in kilograms. Must be positive.</param>
        /// <returns>A <see cref="MerCalculatorResultsVM"/> object containing the calculated MER results.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="weightInKgs"/> is less than or equal to zero, or if the underlying calculator detects an invalid weight.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="lifeStage"/> is not a valid value of <see cref="LifeStageFactorsEnum"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when an unexpected error occurs during the MER calculation.
        /// </exception>
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
                    Mean = mean,
                    CupsLow = null,  // Initialized as null; set in component if kcalPerCup provided
                    CupsHigh = null,
                    CupsMean = null
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