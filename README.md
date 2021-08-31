# assecor-assessment-backend
Web API with ASP.NET Core + XUnit Test

the assessor-assessment-backend project requires at least .Net Core 3.1 to run.
the XUnitTestProject project requires at least .Net 5 to run.

Functionality:

    * GET - http://localhost:5000/persons : get all the persons.     
    * GET - http://localhost:5000/persons/{id} : get a person with a specific {id}. 
    * GET - http://localhost:5000/persons/color/{color} : get all persons with a specific {color}. 
    * POST - http://localhost:5000/person/ : add a new person. 
        - Content : JSON 
                - Format:   {
                                "name" : "{name}}",
                                "lastname": "{lasName}",
                                "zipcode" : "{zipCode}",
                                "city" : "{city}",
                                "color" : "{color}"
                            }