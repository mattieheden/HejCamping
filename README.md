# HejCamping
Hej &amp; welcome to our camping webapp.

We have delpoyed it with Azure and a .tech domain to publish it. So in order to deploy it you need the following things:
Tech domain:
- a free tier .tech domain
- a free email from .tech

Azure:
- Email Communication Services Domain
- Communication Service
- Email Communication Service
- SQL Server
- SQL Database
- Azure Container Registry
- Azure Container App

Enviroment variables you need to set in your container app is:
```
DATABASE_CONNECTION_STRING='Connectionstring;'
AZURE_EMAIL_SENDER='DoNotReply@hejcamping.tech'
AZURE_EMAIL_OWNER='donotreply@hejcamping.tech'
AZURE_EMAIL_CONNECTION_STRING='Connectionstring;'
```

Happy camping!
