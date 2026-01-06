using System.ComponentModel.DataAnnotations;

namespace fastkart101.ViewModel.Register
{
    public class RegisterVm
    {
        [Required, MaxLength(256), MinLength(3)]
        public string Username { get; set; }
        [Required, MaxLength(256), MinLength(3)]
        public string FullName { get; set; }
        [Required, MaxLength(256), MinLength(3), EmailAddress]
        public string EmailAddress { get; set; }
        [Required, MaxLength(256), MinLength(3), DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
