
# Forum

## Building
---
### Prerequisites

1. .NET Core 3.0
2. Microsoft SQL Server
3. Dotnet ef CLI tool
4. Auth0 account

### Instructions

1. Create a database called Forum on the local SQL server
2. Clone this repository and navigate to the `repositories` project.
3. Execute `dotnet ef database update -s ..\Web`
4. Run the project as a .NET project

## Authorization
---

1. Open Auth0 and create an SPA application
2. Download the SPA example project and follow the instructions
3. In `appsettings.json`, fill out `authority` and `audience` with the authorization endpoint and the client ID respectively
4. Using the SPA example application, register, login and use the access token from the token response
