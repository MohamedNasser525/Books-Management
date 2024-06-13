using System.ComponentModel.DataAnnotations.Schema;

namespace Management.Models
{
    [Table("Books",Schema = "security")]
    public class book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string title { get; set; }
        public string categories { get; set; }
        public string authors { get; set; }
        public string description { get; set; }
        public string published_year { get; set; }
        public string average_rating { get; set; }
        public string ratings_count { get; set; }
        public string num_pages { get; set; }
        public string url_image { get; set; }

    }
}
