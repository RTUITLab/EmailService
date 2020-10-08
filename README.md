# EmailService
Email micro service.  
It's a simple service just to send emails.

## Requirements

.Net Core 3.1

## Run from docker images

1. Create and fill `.env` file
```ini
SENDER_EMAIL=sender account email
SENDER_PASSWORD=sender account password
SMTP_HOST=sender smtp port
SMTP_PORT=sender smtp port
```

2. Pull latest docker image
```bash
docker-compose pull
```

3. Run service
```
docker-compose up
```
Now you can send requests to localhost:5000, authorization header value placed in `src/EmailService/appsettings.Development.json`. See [How to use](#how-to-use) for api format.

## Configuration

In ```src/EmailService/appsettings.json``` file add this configuration
```js
{
  // configuration omitted
  "EmailSenderOptions": {
    "Email": "email address from which your service will send emails. example@gmail.com",
    "Password": "password for email address from which your service will send emails. superhardpassword",
    "SmtpHost": "smtp server or some another host. smtp.mail.ru",
    "SmtpPort": 25 //Use your smtp port
  },
  "HeaderAuthorizationOptions": {
    "Key": "some random key for Header Authoriztion"
  },
  "LOGS_ACCESS_TOKEN": "some token for access to logs via websocket"
  // configuration omitted
}
```
## Run
```bash
cd ./src/EmailService
dotnet run
```
API will be available on [localhost:5000](http://localhost:5000)

## How to use

There is one available POST request on url /api/email/send.  
Request body (__all__ fields are __required__!):  
```js
{
  "Email": "email address to which send email. test@test.com",
  "Subject": "email's subject. My first Test subject",
  "Body": "email's message in html. Just a test mail.<br>Second line."
}
```
### curl exampple how to send email
Email address which would receive email: example@mail.ru  
Subject of email: Confirm your email  
Body of email: ``` Please visit this <a>link</a> to confirm your email.```
```bash
curl --header "Content-Type: application/json" \
  --header "Authorization: some random key for Header Authoriztion"
  --request POST \
  --data '{"email":"example@mail.ru","subject":"Confirm your email", "body":"Please visit this <a>link</a> to confirm your email."}' \
  http://localhost:5000/api/email/send
```

## How to use RTUITLab.EmailService.Client package

1. Install [RTUITLab.EmailService.Client package](https://dev.azure.com/rtuitlab/RTU%20IT%20Lab/_packaging?_a=package&feed=ITLab&package=RTUITLab.EmailService.Client&protocolType=NuGet) to your ASP.Net Core project
2. Extend your appsettings.json file with following code:  
```js
{
  // configuration omitted
  "EmailSenderOptions": {
    "BaseAddress": "url to your email service that you've created upper. http://localhost:5000",
    "Key": "some random key for Header Authoriztion. Check instructions above"
  }
  // configuration omitted
}
```
3. Edit Startup.cs file:
```c#
// Add email service
services.AddEmailSender(Configuration
    .GetSection(nameof(EmailSenderOptions))
    .Get<EmailSenderOptions>());
```
4. By following code you'll send request to your email service. Email service will deliver emails:
```c#
RTUITLab.EmailService.Client.IEmailSender sender;
await sender.SendEmailAsync("test@test.com", "Email letter subject", "<h1>Html body</h1>")
```
```c#
// Example with some random controller
public class EmailSendController : ControllerBase
{
    private readonly RTUITLab.EmailService.Client.IEmailSender sender;

    public EmailSender(RTUITLab.EmailService.Client.IEmailSender sender)
    {
        this.sender = sender;
        DoStuff();
    }

    private async Task DoStuff()
    {
      // await sender.SendEmailAsync("email to send", "subject of email", "html body - message to send via email");
      await sender.SendEmailAsync("test@test.com", "Email letter subject", "<h1>Html body</h1>");
    }
}
```