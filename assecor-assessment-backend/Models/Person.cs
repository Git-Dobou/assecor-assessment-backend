using System.ComponentModel.DataAnnotations.Schema;

namespace assecor_assessment_backend.Models
{
    [Table("Person")]
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Color { get; set; }

        public bool IsComplet()
        {
            return !string.IsNullOrEmpty(Name) &&
            !string.IsNullOrEmpty(LastName) &&
            !string.IsNullOrEmpty(ZipCode) &&
            !string.IsNullOrEmpty(Color) &&
            !string.IsNullOrEmpty(City);
        }
    }
}
