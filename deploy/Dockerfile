FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app

COPY emailservice-build .

ENV ASPNETCORE_URLS=http://*:5000
ENTRYPOINT ["dotnet", "EmailService.dll"]