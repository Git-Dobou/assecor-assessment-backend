using System.ComponentModel.DataAnnotations.Schema;

namespace web_api.Models
{
    [Table("Zoo")]
    public class Zoo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }

        public bool IsComplet()
        {
            return
            !string.IsNullOrEmpty(Name?.Trim()) &&
            !string.IsNullOrEmpty(ZipCode?.Trim()) &&
            !string.IsNullOrEmpty(Adress?.Trim()) &&
            !string.IsNullOrEmpty(City?.Trim());
        }
    }
}
