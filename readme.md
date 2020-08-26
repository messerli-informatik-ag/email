# Email

![Build](https://github.com/messerli-informatik-ag/email/workflows/Build/badge.svg)
[![NuGet](https://img.shields.io/nuget/v/Messerli.Email.svg)](https://www.nuget.org/packages/Messerli.Email/)

A simple abstraction for sending emails.

## Example

```csharp
var mailCatcherConfig = new SmtpServerConfig(host: "localhost", port: 1025, useSsl: false);
var sender = new EmailSenderBuilder().Build(new EmailDelivery.SmtpDelivery(mailCatcherConfig));
var message = var message = new EmailMessageBuilder()
  .From(new MailboxAddress("pitcher@localhost", "Pitcher"))
  .AddRecipient(new MailboxAddress("mailcatcher@localhost", "MailCatcher"))
  .Subject("Catch me if you can")
  .AddBodyPart(new BodyPart.Alternatives(
      new BodyPart.Plain("Hello there"),
      new BodyPart.Html("<b>Hello there</b>")));
await sender.SendMessage(message);
```
