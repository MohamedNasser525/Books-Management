using System.ComponentModel.DataAnnotations;

namespace Management.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [MaxLength(50), MinLength(3)]
        [RegularExpression(@"^[a-zA-Z0-9@,._\s]+$", ErrorMessage = "in Name Please enter characters like (a~z, A~Z, 0~9, @, _, ., ,, space) not more")]

        public string FName { get; set; }
        [MaxLength(50), MinLength(3)]
        [RegularExpression(@"^[a-zA-Z0-9@,._\s]+$", ErrorMessage = "in Name Please enter characters like (a~z, A~Z, 0~9, @, _, ., ,, space) not more")]

        public string LName { get; set; }
        [MaxLength(150), EmailAddress, RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]

        public string Email { get; set; }

    }
}
