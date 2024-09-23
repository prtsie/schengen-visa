using System.ComponentModel.DataAnnotations;

namespace BlazorWebAssemblyVisaApiClient.Validation.Applicants.Models
{
    public enum MaritalStatusModel
    {
        Other = 0,

        Married = 1,

        Unmarried = 2,

        Separated = 3,

        [Display(Name = "Widow or widower")]
        WidowOrWidower = 4
    }
}
