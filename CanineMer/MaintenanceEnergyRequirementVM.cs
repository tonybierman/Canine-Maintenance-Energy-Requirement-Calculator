using System.ComponentModel.DataAnnotations;

namespace CanineMer
{
    public class MaintenanceEnergyRequirementVM
    {
        [Display(Name = "Dog's Weight In Pounds")] public double WeightInPounds { get; set; }

        [Display(Name = "Dog's Status")] public LifeStageFactorsEnum LifeStage { get; set; }
    }
}
