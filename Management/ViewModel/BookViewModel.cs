using System.ComponentModel.DataAnnotations;

namespace Management.ViewModel
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [MaxLength(150),MinLength(2)]
        [Display(Name = "title")]
        [RegularExpression(@"^[a-zA-Z0-9@,._\s]+$", ErrorMessage = "in title Please enter characters like (a~z, A~Z, 0~9, @, _, ., ,, space) not more")]
        public string title { get; set; }



        [MaxLength(150), MinLength(2)]
        [Display(Name = "categories")]
        [RegularExpression(@"^[a-zA-Z0-9@,._\s]+$", ErrorMessage = "in categories Please enter characters like (a~z, A~Z, 0~9, @, _, ., ,, space) not more")]
        public string categories { get; set; }



        [MaxLength(150), MinLength(2)]
        [Display(Name = "authors")]
        [RegularExpression(@"^[a-zA-Z0-9@,._\s]+$", ErrorMessage = "in authors Please enter characters like (a~z, A~Z, 0~9, @, _, ., ,, space) not more")]
        public string authors { get; set; }



        [MaxLength(2000), MinLength(2)]
        [Display(Name = "description")]
        [RegularExpression(@"^[a-zA-Z0-9@,._\s]+$", ErrorMessage = "in description Please enter characters like (a~z, A~Z, 0~9, @, _, ., ,, space) not more")]
        public string description { get; set; }




        [StringLength(4)]
        [Display(Name = "published_year")]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "published_year must be  number from 4 digits")]
        public string published_year { get; set; }






        [Display(Name = "average_rating")]
        [RegularExpression(@"^(10(\.00?)?|[0-9](\.[0-9]{1,2})?)$", ErrorMessage = "in average_rating Please enter a number between 0.00 and 10.00")]
        public string average_rating { get; set; }



        [Display(Name = "ratings_count")]
        [RegularExpression(@"^(10{7}|\d{1,7}(\.\d{1,2})?)$", ErrorMessage = "in ratings_count Please enter a number between 0 and 10,000,000")]
        public string ratings_count { get; set; }



        [Display(Name = "num_pages")]
        [RegularExpression(@"^(10{7}|\d{1,7}(\.\d{1,2})?)$", ErrorMessage = "in num_pages Please enter a number between 0 and 10,000,000")]
        public string num_pages { get; set; }



        [Display(Name = "url_image")]
        [RegularExpression(@"^(https?|ftp)://[^\s/$.?#].[^\s]*$", ErrorMessage = "in url_image Please enter a valid URL")]
        public string url_image { get; set; }
    }
}
