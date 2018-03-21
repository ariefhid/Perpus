# Perpus Web API

this my e-library project approach with CodeFirst

# OS

- Windows 10
- Linux Ubuntu 16.04
- Nginx 

# Pre-requisites

- Net Core SDK version 2.0 ++ fro Windows ## https://www.microsoft.com/net/download/windows
- Net Core SDK version 2.0 ++ for linux ## https://www.microsoft.com/net/download/linux-package-manager/ubuntu16-04/sdk-current
- PostgreSQL version 9.0 ++ for linux ubuntu ## sudo apt-get install postgresql-9.6
- PostgreSQL version 10 for Windows ## https://www.enterprisedb.com/downloads/postgres-postgresql-downloads
- if you need other database checkout this https://docs.microsoft.com/en-us/ef/core/providers/ and install the package

# Configuration 

- Edit config database (Connection String) in appsettings.json
- Edit launchSetting.json in properties folder if you want change port

# Execute Script on Linux Terminal

- sudo gedit /etc/nginx/sites-available/default ## Reverse Proxy Setting

*Erase all existing contents and replace with the following. I'm assuming that your project runs on port 5000 by default. Modify your proxy pass as necessary.
     server {
         listen 80;
         location / {
             proxy_pass http://localhost:5000;
             proxy_http_version 1.1;
             proxy_set_header Upgrade $http_upgrade;
             proxy_set_header Connection keep-alive;
             proxy_set_header Host $host;
             proxy_cache_bypass $http_upgrade;
         }
     }*

- sudo nginx -t ## Verify the syntax of the nginx settings with the following
- cd [Directory Project]/Perpus/src/Perpus/
- dotnet Restore ## Restore all references on Project
- cd [Directory Project]/Perpus/src/Perpus.Domain/
- dotnet Restore ## Restore all references on Project
- cd [Directory Project]/Perpus/src/Perpus.DTO/
- dotnet Restore ## Restore all references on Project
- cd [Directory Project]/Perpus/src/Perpus.Mapper/
- dotnet Restore ## Restore all references on Project
- dotnet Build ## Build Project
- cd [Directory Project]/Perpus/src/Perpus/
- dotnet ef ## make sure dotnet ef already exist
- dotnet ef dbcontext list ## Make sure dbcontext already exist
- dotnet ef migrations add "Initial" -o /Perpus/Data/Logs ## Create Database based on class model project and migrations logs to /Perpus/Data/Logs
- dotnet ef database update ## update database
- dotnet Build ## Build Project
- dotnet run ## run dotnet in current project path for testing and make sure running well
- cd [Directory Project] ## .sln file folder
- dotnet publish -o [Your Directory (free)]
- sudo nano /etc/systemd/system/[SERVICE_NAME].service ## Create the kestrel service definition file

  *Enter the following into the new service definition file (case sensitive).
       [Unit]
       Description=Example .NET Web API Application running on Ubuntu
       [Service]
       WorkingDirectory=[PUBLISH_DIRECTORY]
       ExecStart=/usr/bin/dotnet [PUBLISH_DIRECTORY]/[PROJECTNAME].dll
       Restart=always
       RestartSec=10
       SyslogIdentifier=dotnet-example
       User=www-data
       Environment=ASPNETCORE_ENVIRONMENT=Production
       [Install]
       WantedBy=multi-user.target*
- sudo systemctl enable [SERVICE_NAME].service ## Enable your new service to auto-start on system boot. other command Restart, disable, status, stop, start, etc.
- Reboot
- sudo systemctl status [SERVICE_NAME].service ## Verify that your service is running without error.

# Testing

- Run in your Browser http://localhost:5000/swagger/

# Additional Doc
- https://medium.com/@JohGeoCoder/deploying-a-net-core-2-0-web-application-to-a-production-environment-on-ubuntu-16-04-using-nginx-683b7e831e6
