using System.ComponentModel.DataAnnotations;

namespace fastkart101.ViewModel.Login
{
    public class LoginVm
    {
        [Required, MaxLength(256), MinLength(3)]
        public string EmailAddress { get; set; }
        [Required, MaxLength(256), MinLength(3), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
