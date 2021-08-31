using System.Collections.Generic;

namespace assecor_assessment_backend.Services.Person
{
    public interface IPersonService
    {
        bool AddPerson(in Models.Person person);
        Models.Person GetPersonById(int id);
        IEnumerable<Models.Person> GetAllPersons();
        IEnumerable<Models.Person> GetAllWithSameColor(string color);
    }
}