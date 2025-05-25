using System.ComponentModel.DataAnnotations;

namespace Assessment_try.Models
{
    public class User
    {
        public int Id { get; set; } 
        public string user_name { get; set; }

        public int? user_no { get; set; }

        public DateTime create_date { get; set; }
    }

}
