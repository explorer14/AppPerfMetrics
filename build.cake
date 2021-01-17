#tool "nuget:?package=OctopusTools"
#addin nuget:?package=Cake.SemVer
#addin nuget:?package=semver&version=2.0.4
#addin nuget:?package=Cake.FileHelpers

using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;

var target = Argument("target", "Build");
var artifactDirPath = "./artifacts/";
var packagePublishDirPath = "./publish/";
var semVer = CreateSemVer(1,0,0);
var solutionFilePath = "./AppPerfMetrics.sln";
var nugetApiKey = EnvironmentVariable("NUGET_API_KEY");

Setup(ctx=>
{
    var buildNumber = EnvironmentVariable("BUILD_BUILDNUMBER");

    if(!string.IsNullOrWhiteSpace(buildNumber))
    {
        Information($"The build number was {buildNumber}");
        semVer = buildNumber;
    }
    else
    {
        Information($"The build number was empty, using the default semantic version of {semVer.ToString()}");
    }

	SetUpNuget();
});

void SetUpNuget()
{
	var feed = new
	{
		Name = "SkynetNuget",
	    Source = "https://skynetcode.pkgs.visualstudio.com/_packaging/skynetpackagefeed/nuget/v3/index.json"
	};

	if (!NuGetHasSource(source:feed.Source))
	{
	    var nugetSourceSettings = new NuGetSourcesSettings
                             {
                                 UserName = "skynetcode",
                                 Password = EnvironmentVariable("SYSTEM_ACCESSTOKEN"),
                                 Verbosity = NuGetVerbosity.Detailed
                             };		

		NuGetAddSource(
		    name:feed.Name,
		    source:feed.Source,
		    settings:nugetSourceSettings);
	}	
}


Teardown(ctx=>
{

});

Task("Restore")
    .Does(() => {		
		DotNetCoreRestore(solutionFilePath);	
});

Task("Build")
	.IsDependentOn("Restore")
    .Does(()=>{
		var config = new DotNetCoreBuildSettings
		{
			Configuration = "Release",
			NoRestore = true
		};
        DotNetCoreBuild(solutionFilePath, config);
});

Task("Test")
	 .IsDependentOn("Build")
     .Does(() =>
 {
     var settings = new DotNetCoreTestSettings
     {
         Configuration = "Release",
		 NoBuild = true
     };

     var projectFiles = GetFiles("./tests/**/*Tests.csproj");

     foreach(var file in projectFiles)
     {
         Information($"Running test suite on: {file.FullPath}");
         DotNetCoreTest(file.FullPath, settings);
     }
 });

Task("PushToNuget")
	.IsDependentOn("Pack")
	.Does(()=>
{
    PushToNugetFeed(
        "https://skynetcode.pkgs.visualstudio.com/_packaging/skynetpackagefeed/nuget/v3/index.json", 
        "gibberish");
        
    PushToNugetFeed("https://api.nuget.org/v3/index.json",
        nugetApiKey);
});

void PushToNugetFeed(string source, string apiKey)
{
    var files = GetFiles("./artifacts/AppPerformanceMetricsSender.*.nupkg");

    foreach(var file in files)
    {
        Information(file.FullPath);
        var settings = new DotNetCoreNuGetPushSettings
        {
            Source = source,
            ApiKey = apiKey,
            SkipDuplicate = true
        };

        DotNetCoreNuGetPush(file.FullPath, settings);
    }
}

Task("Pack")
	.IsDependentOn("Test")
	.Does(()=>{
		var settings = new DotNetCorePackSettings
		{
		    Configuration = "Release",
		    OutputDirectory = artifactDirPath,
			NoBuild = true,
			NoRestore = true
		};

		DotNetCorePack("./src/AppPerformanceMetricsSender/AppPerformanceMetricsSender.csproj", 
            settings);
});


RunTarget(target);