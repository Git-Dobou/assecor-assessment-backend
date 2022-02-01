using System.Collections.Generic;

namespace web_api.Services.Zoo
{
    public interface IZooService
    {
        bool Add(in Models.Zoo zoo);
        bool Delete(string name);
        IEnumerable<Models.Zoo> GetByName(string name);
        IEnumerable<Models.Zoo> GetAll();
    }
}