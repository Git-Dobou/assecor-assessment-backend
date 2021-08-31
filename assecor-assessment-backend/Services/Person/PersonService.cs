using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using assecor_assessment_backend.Services.Color;
using assecor_assessment_backend.Services.Csv;
using assecor_assessment_backend.Services.Db;
using assecor_assessment_backend.Services.Logger;
using assecor_assessment_backend.Services.Path;

namespace assecor_assessment_backend.Services.Person
{
    public class PersonService : IPersonService
    {
        private readonly List<Models.Person> _persons;
        private readonly IColorService _colorService;
        private readonly ILoggerManager _logger;
        private readonly DatabaseInteractor _databaseInteractor;
        private const string SOURCE_FILE_NAME = "sample-input-person.csv";
        private readonly string _sourceFilePath;

        public PersonService(IColorService colorService,
            IPathService pathservice,
            ILoggerManager loggerManager,
            DatabaseInteractor databaseInteractor)
        {
            _colorService = colorService;
            _logger = loggerManager;
            this._databaseInteractor = databaseInteractor;
            _sourceFilePath = pathservice.Combine(pathservice.GetSolutionDir(), $@"Ressources/{SOURCE_FILE_NAME}");
            _persons = LoadFromCsv(_sourceFilePath)?.ToList();
        }

        private List<Models.Person> LoadPersonFromDataBase()
        {
            return _databaseInteractor.Person.ToList();
        }

        private IEnumerable<Models.Person> LoadFromCsv(string path)
        {
            try
            {
                using var csv = new CsvReader(path, ", ", false);
                var persons = new List<Models.Person>();
                var counter = 1;
                while (csv.Read())
                {
                    var person = new Models.Person
                    {
                        Id = counter,
                        Name = csv.GetIndex(0),
                        LastName = csv.GetIndex(1)
                    };

                    var adresse = csv.GetIndex(2);
                    if (!string.IsNullOrEmpty(adresse))
                    {
                        var indexOfFirstSpace = adresse.IndexOf(" ", 0);
                        person.ZipCode = adresse.Substring(0, indexOfFirstSpace);
                        person.City = adresse.Substring(indexOfFirstSpace + 1);
                    }

                    int.TryParse(csv.GetIndex(3), out int colorId);

                    var colorName = _colorService.GetColorNameById(colorId);
                    person.Color = colorName;

                    persons.Add(person);

                    counter++;
                }

                return persons;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                throw ex;
            }
        }

        public IEnumerable<Models.Person> GetAllPersons()
        {
            return _persons;
        }

        public Models.Person GetPersonById(int id)
        {
            try
            {
                if (id > _persons.Count())
                    return null;

                return _persons[id - 1];
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex);
                throw ex;
            }
        }

        public IEnumerable<Models.Person> GetAllWithSameColor(string color)
        {
            return _persons.Where(person => person.Color.Equals(color, StringComparison.OrdinalIgnoreCase));
        }

        public bool AddPerson(in Models.Person person)
        {
            try
            {
                using var csvWritter = new CsvWritter(_sourceFilePath);
                StringBuilder builder = new StringBuilder();

                var colorId = _colorService.GetColorIdByName(person.Color);
                if (colorId == null)
                    return false;
                var newLine = $"{person.Name}, {person.LastName}, {person.ZipCode} {person.City}, {colorId.Value}";
                builder.AppendLine();
                builder.Append(newLine);
                csvWritter.Writte(builder.ToString());
                person.Id = _persons.Count() + 1;

                _persons.Add(person);
                _databaseInteractor.Person.Add(person);
                _databaseInteractor.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                throw ex;
            }

            return true;
        }
    }
}