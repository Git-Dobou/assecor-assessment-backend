# web_api
Web API with ASP.NET Core

Functionality:

    * GET - http://localhost:{port}/zoo/all : get all the zoos.     
    * GET - http://localhost:{port}/zoo/get?name={name} : get zoos with a specific {name}. 
    * DELETE - http://localhost:{port}/zoo/delete?id={id} : delete a zoo with a specific {id}.
    * POST - http://localhost:{port}/zoo/add : add a new zoo. 
        - Content : JSON 
                - Format:   {
                                "name" : "{name}}",
                                "zipcode" : "{zipCode}",
                                "city" : "{city}",
                                "Adress" : "{adress}"
                            }