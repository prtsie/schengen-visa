using System.ComponentModel.DataAnnotations;

namespace BlazorWebAssemblyVisaApiClient.Validation.VisaApplications.Models
{
    public enum VisaCategoryModel
    {
        Transit = 0,
        [Display(Name = "Short dated")]
        ShortDated = 1
    }
}
