FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app

COPY EmailService/emailservice-build .

ENTRYPOINT ["dotnet", "EmailService.dll"]