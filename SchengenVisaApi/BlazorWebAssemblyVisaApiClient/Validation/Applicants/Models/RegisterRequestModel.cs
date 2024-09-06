using System.ComponentModel.DataAnnotations;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Validation.Applicants.Models
{
    /// Model of request with attributes required for validation to work
    public class RegisterRequestModel
    {
        [Required]
        [ValidateComplexType]
        public AuthData AuthData { get; set; } = new AuthData();
    }
}
