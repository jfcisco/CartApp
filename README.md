# Cart Application

The application is a REST API that allows you to add, update, and remove a cart of items.

At any point, the application can generate a detailed bill of the active cart.

## Current Status

The application starts a Kestrel server hosting a REST API.

Navigating to /swagger will show a UI to test out the Web API.

All requirements have been implemented except for the following:
- Conversion of Bill to Non-USD Currency

Possible enhancements include the following:
- Cover controller layer and the rest of service layer functionality with tests
- Add configuration for products list instead of hard coding them
- Finish OpenAPI documentation by writing and enabling XML comments support

## Background

This project is an exercise to let my backend web development lessons sink in. This is more or less what I know about C# and ASP.NET Core a this time.

The requirements of the app in this exercise can be seen in a `requirements.md` in the repository. It's a take-home challenge for an backend developer interview.