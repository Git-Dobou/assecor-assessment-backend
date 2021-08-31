using System.Collections.Generic;
using System.Linq;

namespace assecor_assessment_backend.Services.Color
{
    public class ColorService : IColorService
    {
        private IReadOnlyList<Models.Color> _colors;
        public ColorService()
        {
            _colors = new List<Models.Color>
            {
                new Models.Color
                {
                    Id = 1,
                    Name = "blau"
                },
                new Models.Color
                {
                    Id = 2,
                    Name = "grün"
                },
                new Models.Color
                {
                    Id = 3,
                    Name = "violett"
                },
                new Models.Color
                {
                    Id = 4,
                    Name = "rot"
                },
                new Models.Color
                {
                    Id = 5,
                    Name = "gelb"
                },
                new Models.Color
                {
                    Id = 6,
                    Name = "türkis"
                },
                new Models.Color
                {
                    Id = 7,
                    Name = "weiß"
                }
            };
        }

        public int? GetColorIdByName(string name)
        {
            var color = _colors.FirstOrDefault(color => color.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
            return color?.Id;
        }

        public string GetColorNameById(int id)
        {
            var color = _colors.FirstOrDefault(color => color.Id == id);
            return color?.Name;
        }
    }
}