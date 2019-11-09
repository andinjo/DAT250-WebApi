
# Forum

## Building
---
### Prerequisites

1. .NET Core 3.0
2. Microsoft SQL Server
3. Dotnet ef CLI tool
4. Auth0 account

### Instructions

1. Create a database called `Forum` on the local SQL server
2. Clone this repository and navigate to its root directory.
3. Execute `dotnet user-secrets set "ConnectionStrings:Forum" "Data Source=.;Initial Catalog=Forum;Integrated Security=True;"`
3. Execute `dotnet ef database update -p .\Repositories -s .\Web`
4. Run the project as a .NET project

## Authorization
---

1. Open Auth0 and create an SPA application
2. Download the SPA example project and follow the instructions
3. Execute `dotnet user-secrets set "Auth0:Audience" "[AUTH0 CLIENT ID]"`
4. Execute `dotnet user-secrets set "Auth0:Authority" "[AUTH0 URL ENDPOINT]"`
4. Using the SPA example application, register an account, login and use the access token from the authentication response
