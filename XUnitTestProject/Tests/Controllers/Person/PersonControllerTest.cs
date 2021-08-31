using System.Collections.Generic;
using System.Linq;
using System.Net;
using assecor_assessment_backend.Controllers;
using assecor_assessment_backend.Services.Color;
using assecor_assessment_backend.Services.Logger;
using assecor_assessment_backend.Services.Person;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace assecor_assessment_backend.Tests.Controllers.Person
{
    public class PersonControllerTest
    {
        readonly Mock<IPersonService> _personServiceMock;
        readonly Mock<IColorService> _colorServiceMock;
        readonly Mock<ILoggerManager> _loggerMock;
        readonly PersonsController _personController;

        private IEnumerable<Models.Person> _persons;
        public PersonControllerTest()
        {
            _colorServiceMock = new Mock<IColorService>();
            _personServiceMock = new Mock<IPersonService>();
            _loggerMock = new Mock<ILoggerManager>();

            _personController = new PersonsController(_personServiceMock.Object, _loggerMock.Object);
            InitializeMock();
        }

        private void InitializeMock()
        {
            _persons = GetPersons();
            _personServiceMock.Setup(m => m.GetAllPersons()).Returns(_persons);
            _personServiceMock.Setup(m => m.GetAllWithSameColor("blau")).Returns(_persons.Where(person => person.Color.Equals("blau", System.StringComparison.OrdinalIgnoreCase)));
            _personServiceMock.Setup(m => m.GetAllWithSameColor("schwarz")).Returns(_persons.Where(person => person.Color.Equals("schwarz", System.StringComparison.OrdinalIgnoreCase)));
            _personServiceMock.Setup(m => m.GetPersonById(1)).Returns(_persons.FirstOrDefault(person => person.Id.Equals(1)));
            _personServiceMock.Setup(m => m.GetPersonById(11)).Returns(_persons.FirstOrDefault(person => person.Id.Equals(11)));
            _colorServiceMock.Setup(m => m.GetColorIdByName("blau")).Returns(1);
        }

        [Fact]
        public void PersonControllerTest_Test_Get_Request()
        {
            var okResult = _personController.Get().Result as OkObjectResult;
            var persons = Assert.IsType<List<Models.Person>>(okResult.Value);
            Assert.Equal(10, persons.Count);
            Assert.Equal(this._persons, persons);
        }

        [Fact]
        public void PersonControllerTest_Test_Get_By_Id_Request()
        {
            var a = _personServiceMock.Object.GetAllPersons().ToList();
            var b = _personServiceMock.Object.GetPersonById(1);
            var c = _personServiceMock.Object.GetAllWithSameColor("blau").ToList();

            //Good request
            var okResult = _personController.GetById("1").Result as OkObjectResult;
            var person = Assert.IsType<Models.Person>(okResult.Value);
            Assert.Equal(_persons.FirstOrDefault(person => person.Id.Equals(1)), person);

            //Bad request
            var badResult = _personController.GetById("1a").Result as StatusCodeResult;
            Assert.Equal((int)HttpStatusCode.BadRequest, badResult.StatusCode);

            //Out of range
            var noContentResult = _personController.GetById("11").Result as StatusCodeResult;
            Assert.Equal((int)HttpStatusCode.NoContent, noContentResult.StatusCode);
        }

        [Fact]
        public void PersonControllerTest_Test_Get_By_Color_Request()
        {
            //Get all persons with color blau
            var okResult = _personController.GetAllWithSameColor("blau").Result as OkObjectResult;
            var persons = (IEnumerable<Models.Person>)okResult.Value;
            Assert.Equal(2, persons.Count());
            Assert.Equal(_persons.Where(person => person.Color.Equals("blau")), persons);

            //Get all persons with other color
            var noContentResult = _personController.GetAllWithSameColor("schwarz").Result as StatusCodeResult;
            Assert.Equal((int)HttpStatusCode.NoContent, noContentResult.StatusCode);
        }

        [Fact]
        public void PersonControllerTest_Test_Add_Person_Request()
        {
            // Add correct person
            var person = new Models.Person
            {
                Id = 11,
                Name = "Dobou",
                LastName = "Daniel",
                ZipCode = "12345",
                City = "Berlin",
                Color = "blau"
            };
            _personServiceMock.Setup(m => m.AddPerson(person)).Returns(true);

            var result = _personController.AddPerson(person) as StatusCodeResult;
            Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);

            // Add person with empty Name
            var person1 = new Models.Person
            {
                Name = "",
                LastName = "Daniel",
                ZipCode = "12345",
                City = "Berlin",
                Color = "blau"
            };
            _personServiceMock.Setup(m => m.AddPerson(person1)).Returns(false);
            var result1 = _personController.AddPerson(person1) as StatusCodeResult;
            Assert.Equal((int)HttpStatusCode.PartialContent, result1.StatusCode);


            // Add person with a color that doesn't exist
            var person2 = new Models.Person
            {
                Name = "Dobou",
                LastName = "Daniel",
                ZipCode = "12345",
                City = "Berlin",
                Color = "schwarz"
            };
            _personServiceMock.Setup(m => m.AddPerson(person2)).Returns(false);
            var result2 = _personController.AddPerson(person2) as StatusCodeResult;
            Assert.Equal((int)HttpStatusCode.InternalServerError, result2.StatusCode);
        }

        private IEnumerable<Models.Person> GetPersons()
        {
            return new List<Models.Person>
            {
                new Models.Person
                {
                    Id = 1,
                    Name = "Müller",
                    LastName = "Hans",
                    ZipCode = "67742",
                    City = "Lauterecken",
                    Color = "blau"
                },
                new Models.Person
                {
                    Id = 2,
                    Name = "Petersen",
                    LastName = "Peter",
                    ZipCode = "18439",
                    City = "Stralsund",
                    Color = "grün"
                },
                new Models.Person
                {
                    Id = 3,
                    Name = "Johnson",
                    LastName = "Johnny",
                    ZipCode = "88888",
                    City = "made up",
                    Color = "violett"
                },
                new Models.Person
                {
                    Id = 4,
                    Name = "Millenium",
                    LastName = "Milly",
                    ZipCode = "77777",
                    City = "made up too",
                    Color = "rot"
                },
                new Models.Person
                {
                    Id = 5,
                    Name = "Müller",
                    LastName = "Jonas",
                    ZipCode = "32323",
                    City = "Hansstadt",
                    Color = "gelb"
                },
                new Models.Person
                {
                    Id = 6,
                    Name = "Fujitsu",
                    LastName = "Tastatur",
                    ZipCode = "42342",
                    City = "Japan",
                    Color = "türkis"
                },
                new Models.Person
                {
                    Id = 7,
                    Name = "Andersson",
                    LastName = "Anders",
                    ZipCode = "32132",
                    City = "Schweden - ☀",
                    Color = "grün"
                },
                new Models.Person
                {
                    Id = 8,
                    Name = "Bart",
                    LastName = "Bartman",
                    ZipCode = "12313",
                    City = "Wasweißich",
                    Color = "blau"
                },
                new Models.Person
                {
                    Id = 9,
                    Name = "Gerber",
                    LastName = "Gerda",
                    ZipCode = "76535",
                    City = "Woanders",
                    Color = "violett"
                },
                new Models.Person
                {
                    Id = 10,
                    Name = "Klaussen",
                    LastName = "Klaus",
                    ZipCode = "43246",
                    City = "Hierach",
                    Color = "grün"
                },
            };
        }
    }
}
