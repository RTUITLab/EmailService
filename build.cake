var target = Argument("target", "PublishEmailService");
var configuration = Argument("configuration", "Release");

var servicePublishDir = "deploy/emailservice-build";
var serviceProject = "src/EmailService/EmailService.csproj";

Setup(ctx =>
{
   CleanDirectory(servicePublishDir);
});

Task("RestoreSolution")
.Does(() => {
   DotNetCoreRestore("./src/EmailService.sln");
});

Task("BuildEmailService")
   .IsDependentOn("RestoreSolution")
   .Does(() =>
{
   var settings = new DotNetCoreBuildSettings {
      Configuration = configuration
   };
   DotNetCoreBuild(serviceProject, settings);
});

Task("PublishEmailService")
   .IsDependentOn("BuildEmailService")
   .Does(() =>
{
   var settings = new DotNetCorePublishSettings
   {
      Configuration = configuration,
      OutputDirectory = servicePublishDir
   };

   DotNetCorePublish(serviceProject, settings);
});

RunTarget(target);