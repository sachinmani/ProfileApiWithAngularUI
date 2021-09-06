# ProfileApiWithAngularUI
The .Net Core project need to be compiled using .net core 3.1. The project can be started either inside Visual studio or outside.
From Inside VS, make sure to not run the project using IIS express or IIS, as the project is configured to use kestrel server.
From outside VS, go to the right folder under bin to locate Profile.Api.exe and double click to start and run the API using kestrel web server.
Project has been configured to run swagger which can be located under the following url https://localhost:5001/docs/index.html

Angular UI
In order to run this angular UI app, we need to node and npm
Inside the Profile.Api project, go into the app/profile folder
Open the above location in command prompt and do execute  'npm install'
Then execute 'ng serve'
