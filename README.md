# LoadBalancer-ASP.NET_
This project is a ASP.NET MVC application designed to solve difficult tasks in the background using Hangfire. The architecture involves two primary execution servers and one backup server. The load balancing is realized with YARP (Yet Another Reverse Proxy), the main server responsible for distributing tasks among the execution servers (it could be changed to nginx or similar). Check the architecture illustrations in file "Schema.png". There is a connected authorization that works on the basis of a cookie with password hashing by sha256 (it is better to redo it into jwt tokens and add salting). Authorized users can track the progress of tasks executing, view the tasks history with results or delete it. There is also the admin Hangfire Dashbord availible to check servers` health, loading, request results etc. 

## Technology stack
- ### Server
  - ASP.NET MVC Framework
  - Entity Framework
  - YARP Load Balancing
  - Hangfire
  - Microsoft SQL Server
- ### Client
  - Razor
  - Bootstrap
  - JS (JQuery)

 ## How to run it
 Clone the project into your PC
 >
You can add the admin user when creating the database, which happens automatically the first time you run the project. Just enter your data instead of mine in file that is located in path
> LoadBalancer/LoadBalancer.DAL/ApplicationDbContext.cs
> 
 To run the app just open the solution in Visual Studio located in path
 > LoadBalancer/LoadBalancer.sln

Then set up your connection string settings to **each** server **(_Loadbalacer_, _Loadbalacer1_ and _BackupServer_)** in files called **appsettings.json** 
>
Then just debug **each** server **(The YARP server must be last)**
>
**_©Load Balancer by Vlad Linnik_**
