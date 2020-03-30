## EmailService
Email micro service.  
It's a simple service just to send emails.

## Requirements

.Net Core 3.1

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
  }
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