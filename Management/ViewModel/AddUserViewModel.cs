using System.ComponentModel.DataAnnotations;

namespace Management.ViewModel
{
    public class AddUserViewModel
    {
        [RegularExpression(@"^[a-zA-Z0-9@,._\s]+$", ErrorMessage = "in Name Please enter characters like (a~z, A~Z, 0~9, @, _, ., ,, space) not more")]

        [Display(Name = "Fname"), MaxLength(100), MinLength(3)]
        public string Fname { get; set; }


        [RegularExpression(@"^[a-zA-Z0-9@,._\s]+$", ErrorMessage = "in Name Please enter characters like (a~z, A~Z, 0~9, @, _, ., ,, space) not more")]

        [Display(Name = "Lname"), MaxLength(100),MinLength(3)]
        public string Lname { get; set; }


        [Display(Name = "Email")]
        [MaxLength(150), EmailAddress, RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]

        public string Email { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[-+_!@#$%^&*.,?]).+$", ErrorMessage = "Invalid input! The data must contain at least one uppercase letter, one lowercase letter, one special character, and can include letters, numbers, and symbols")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
       // [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

       
        //[DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[-+_!@#$%^&*.,?]).+$", ErrorMessage = "Invalid input! The data must contain at least one uppercase letter, one lowercase letter, one special character, and can include letters, numbers, and symbols")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string ConfirmPassword { get; set; }
    }

}
