# Back-end Challenge - Space Flight News

This is a challenge by [Coodesh](https://coodesh.com/). 
[Presentation Link](https://www.loom.com/embed/0e7409a78fd045849601117556cacc77)

This is a simple ASP .NET Web API challenge that uses some of the data from the [Space Flight News](https://api.spaceflightnewsapi.net/v3/documentation) project.

#### Languages and frameworks used in this project
C# ASP.NET 6  
[MySQL Database](https://www.mysql.com/) (it was used the JawsDB service on Heroku for data storage)  
[FluentScheduler](https://github.com/fluentscheduler/FluentScheduler) (this framework was used to perform a Cron Job for data sync everyday at 9AM, also logging everything in case something goes wrong)  
[Newtonsoft Json.Net](https://github.com/JamesNK/Newtonsoft.Json) (this framework was needed for json serialization/deserialization)  
[xUnit.net](https://xunit.net/) (for unit testing)