# SftpClientApp -- Under Development

Machine connecitvity is annoying, alot of files get written by machines, that have to be pushed into the cloud... for whatever reasons. So you wont have time at work to develop a kick as sftp client that does everthing from security, configuration to logging. I quess you are a Person from IT or OT and your job is to do that crap so here i will explain you how to do that and keep your sanity.


## Deployment Database

so for that fun you need to deploy your database, follow these steps:

```
1. git clone
2. Restore Nuget-Packages
3. change the appsettings file to fit your database
4. run the database update command
5. add a sftp config to the Database
```
**database update command**
>dotnet ef database update

**Add new Migration**
>dotnet ef migrations add <Migration_NAme>

Example SQL for creating a config:
#ToDo

## Install Client

Take the exe and run it via the Taskplanner, i would advice you to let it run every 10 minutes.
You might ask yourselfe why not a windows service, good question. Installing a windows service sucks thats why, dont make your life harder than it already is 😉. Often times you will face machines which are configured and restricted to a point of no return. 

```
1.  Open the Task Scheduler: You can search for "Task Scheduler" in the Start menu and open it.
 
2.  Create a new task: In the Task Scheduler, go to the "Action" menu and select "Create Basic Task".
    
3.  Name and describe the task: Enter a name and optionally a description for your task, then click "Next".
    
4.  Trigger the task: Select "Daily" and click "Next". Then set the start time for the task and set the "Recur every" option to 1 days, then click "Next".
    
5.  Set the daily task to repeat: In the "Advanced settings" for the task, check "Repeat task every:" and select "10 minutes" from the dropdown. Set "for a duration of" to "Indefinitely".
    
6.  Action: Select "Start a program" and click "Next".
    
7.  Start a program: Click "Browse" and navigate to the .exe file you want to run. Click "Next".
    
8.  Finish: Review the settings for your task and click "Finish".
```
**Here a little advice:**
i use self contained independent deployment because, you dont want to install the .net dependencys on machines and rather try to be portable.

## What do i plan for this project

```
1. Right now maintaining the sftp client in the Database sucks. I want to close the loop with a sftpgo Server and a webui to maintain the Clients.
2.  For that i am working on Active Directory and Ldap Authentication... I know your ISEC department wants you to authenticate the users atleast against the ldarp.
3. And i want to build and installer
4. this should also work for linux
5. publish a docker image
```
## Credits:

i used 
https://github.com/zeevl/Renci.SshNet/blob/master/Renci.SshNet/SftpClient.cs

*Thank you and much love ❤ to zeevl*
