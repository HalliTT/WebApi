
## Author [@Haraldur](https://github.com/HalliTT) 

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)

![Microsoft](https://img.shields.io/badge/Microsoft-0078D4?style=for-the-badge&logo=microsoft&logoColor=white)

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

![MySQL](https://img.shields.io/badge/mysql-4479A1.svg?style=for-the-badge&logo=mysql&logoColor=white)






# Wep Api
I landsbyen Gudumholm i Himmerland er der en driftig idrætsforening.
Det eneste den driftige idrætsforening mangler er et WebApi til håndtering af foreningens mange
medlemmer.
## Task
1 - SQL/MySQL database
Indeholder tabeller, som
kan håndtere administrationen af foreningen på tilfredsstillende vis.

2 - WebApi
Foreningens kasserer let kan bruge til at holde styr på alle medlemmer ved hjælp af
CRUD operationer på alle aspekter af data i SQL/MySQL databasen.


## Run Locally

Clone the project

```bash
  git clone https://github.com/HalliTT/WebApi
```

Go to the project directory

```bash
  cd WebApi
```

Install dependencies

```bash
  dotnet restore
```

#### Configure the database

1. Update the connection string in [appsettings.json](https://github.com/HalliTT/WebApi/blob/main/WebApi/appsettings.json)

```bash
"ConnectionStrings": {
    "DefaultConnection": "Server={SERVER};Integrated Security=true;Database={DATABASE};TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
```
2. Migrate to the database

```bash
  dotnet ef database update
```

Note: Ensure you have the EF Core tools installed. If not, you can install them by running:

```bash
  dotnet tool install --global dotnet-ef

```

Run the project

```bash
  dotnet run
```

OR

```bash
  CTRL+F5
```

## Features

- [Mapper](https://github.com/HalliTT/WebApi/tree/main/WebApi/Services/Mappers)
- [Middleware Exception](https://github.com/HalliTT/WebApi/blob/main/WebApi/Middleware/ExceptionMiddleware.cs)
- [Validation helper](https://github.com/HalliTT/WebApi/blob/main/WebApi/Helpers/ValidationHelper.cs)
