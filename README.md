# auto.bus – Ticket Office web application  

## Features

- Search for routes by departure/arrival city and date
- Filter routes by number, duration and date
- Print ticket in pdf format
- Buy tickets
- Manage routes (create, read, update and delete) on admin account

## Project preview

<div style="display: flex;">
  <img src="http://drive.google.com/uc?export=view&id=1ltCPim1sfg9Rq_4MPqs97QOc_nU_Cfqj" alt="Search route page" width="480" height="233"  border="10" />
<!--   <img src="http://drive.google.com/uc?export=view&id=1wJKDHYI9KuWQjcS70o13UmDqliayA31X" alt="My tickets page" width="480" height="233"  border="10" /> -->
</div>                                                                                                                                          
                                                                                                                                                    
## How to install?

### Linux

1. [Download](https://dotnet.microsoft.com/download) and install .NET with ASP.NET Core
2. Clone this project
3. Install Entity Framework Core .NET Command-line Tools `[~]$ dotnet tool install --global dotnet-ef`
4. Run run.sh file in terminal `[~]$ sh run.sh`

NOTE: use `[~]$ sh run.sh --help` to see available options

## How to tweak this project for your own uses?

- You can tweak `~/TicketOffice/Models/SeedData.cs` to fill up empty sqlite3 database with your data
- You can comment lines 28, 29 and 30 in `~/TicketOffice/Program.cs` to disable data seeding

## How to contribute?

If you want to add a feature, you should follow these steps:

1. Fork the project
2. Make some changes and commit them with [conventional commit message](https://www.freecodecamp.org/news/how-to-write-better-git-commit-messages/)
3. Submit a PR with a new feature/code

## Find a bug?

If you found an issue or would like to submit an improvement to this project, please submit an issue using the issues tab above. If you would like to submit a PR with a fix, reference the issue you created!

## Known issues (Work in progress)

- The route management page will not load if database contains a route with an empty departure/arrival city date
