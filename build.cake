//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////
var csprojPath = "src/**/*.csproj";
var packageOutputFolder = "dist";

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{   
    CleanDirectory(packageOutputFolder);
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var settings = new DotNetCoreBuildSettings
    {
        Configuration = configuration
    };

    var projects = GetFiles(csprojPath);
    foreach (var project in projects)
    {
        DotNetCoreBuild(project.FullPath, settings);    
    }
});

Task("Package")
    .IsDependentOn("Build")
    .Does(() =>
{

    var settings = new DotNetCorePackSettings
    {
        Configuration = configuration,
        OutputDirectory = packageOutputFolder,
        NoBuild = true
    };

    var projects = GetFiles(csprojPath);
    foreach (var project in projects)
    {
        DotNetCorePack(project.FullPath, settings);    
    }
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Package");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);