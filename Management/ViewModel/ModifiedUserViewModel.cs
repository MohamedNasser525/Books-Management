using System.ComponentModel.DataAnnotations;

namespace Management.ViewModel
{
    public class ModifiedUserViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Fname"), MaxLength(100), MinLength(3)]
        [RegularExpression(@"^[a-zA-Z0-9@,._\s]+$", ErrorMessage = "in Name Please enter characters like (a~z, A~Z, 0~9, @, _, ., ,, space) not more")]

        public string? Fname { get; set; }


        [Required]
        [RegularExpression(@"^[a-zA-Z0-9@,._\s]+$", ErrorMessage = "in Name Please enter characters like (a~z, A~Z, 0~9, @, _, ., ,, space) not more")]

        [Display(Name = "Lname"), MaxLength(100), MinLength(3)]
        public string? Lname { get; set; }


        [Required]
        [MaxLength(150), EmailAddress, RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9@-._]*$", ErrorMessage = "Please enter characters like (a~z , A~Z , 0~9 , @ , _ , -) not more")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [RegularExpression(@"^[a-zA-Z0-9@-._]*$", ErrorMessage = "Please enter characters like (a~z , A~Z , 0~9 , @ , _ , -) not more")]
        [MinLength(8, ErrorMessage = "Confirm Password must be at least 8 characters long")]
        public string? ConfirmPassword { get; set; }




    }
}
