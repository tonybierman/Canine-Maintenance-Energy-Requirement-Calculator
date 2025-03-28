namespace CanineMer
{
    public class MerCalculatorResultsVM
    {
        public long? SubjectId { get; set; }
        public string? LifeStage { get; set; }
        public double WeightInPounds { get; set; }
        public double LowerBounds { get; set; }
        public double UpperBounds { get; set; }
        public double Mean { get; set; }
        public double Rer { get; internal set; }

        public static MerCalculatorResultsVM Calculate(LifeStageFactorsEnum lifeStage, double weightInKgs)
        {
            (double lowerMer, double upperMer) result = 
                CanineMerCalculator.CalculateMer(lifeStage, weightInKgs);

            double rer = CanineMerCalculator.CalculateRer(weightInKgs);

            var retval = new MerCalculatorResultsVM();
            retval.LifeStage = lifeStage.ToString().AddSpacesToPascalCase();
            retval.WeightInPounds = CanineMerCalculator.KgToLbs(weightInKgs);
            retval.LowerBounds = result.lowerMer;
            retval.UpperBounds = result.upperMer;
            retval.Rer = rer;
            retval.Mean = CanineMerCalculator.CalculateMeanMer(lifeStage,
                weightInKgs);

            return retval;
        }
    }
}
