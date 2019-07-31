# PinDrop

PinDrop is a coding exercise challenging me to create a full REST-style API to manage scoring for a bowling alley. It is written in C# using .NET Core and Web API Core. It uses Entity Framework Core as its ORM.

My design philosophy in creating PinDrop was to have the POST commands be as simple and small as possible, as they would likely be coming from a machine as limited as an Arduino or Raspberry Pi, and to have the results of the GET request focused towards what a front-end client might want to display to a user.

It uses Swagger and Swashbuckle to generate API Documentation, as well as allowing you to test the API in real time.

### Features
- Multiple Games tracked at same time.
- Real-Time Scoring.
- Front-End friendly view models.
- Unit Testing!

### Limitations

- Currently uses an In-Memory database, rather than an actual instance.
- Does not have any form of authentication.
- Limited sanity checking.