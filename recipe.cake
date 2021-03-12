#load nuget:?package=Cake.Recipe&version=2.2.0

  ((CakeTask)BuildParameters.Tasks.TestNUnitTask.Task).Actions.Clear();
    
    BuildParameters.Tasks.TestNUnitTask
    .Does(() => RequireTool(ToolSettings.NUnitTool, () => {
        EnsureDirectoryExists(BuildParameters.Paths.Directories.NUnitTestResults);

        var resultsFile = BuildParameters.Paths.Directories.NUnitTestResults + "/NUnitResults.xml";
    

        if (BuildParameters.BuildAgentOperatingSystem == PlatformFamily.Windows)
        {
            OpenCover(tool => {
                tool.NUnit3(GetFiles(BuildParameters.Paths.Directories.PublishedNUnitTests + (BuildParameters.TestFilePattern ?? "/**/*Tests.dll")), new NUnit3Settings {
                    Results = new[] { new NUnit3Result { FileName = resultsFile } },           
                });
            },
            BuildParameters.Paths.Files.TestCoverageOutputFilePath,
            new OpenCoverSettings
            {
                OldStyle = true,
                ReturnTargetCodeOffset = 0
            }
                .WithFilter(ToolSettings.TestCoverageFilter)
                .ExcludeByAttribute(ToolSettings.TestCoverageExcludeByAttribute)
                .ExcludeByFile(ToolSettings.TestCoverageExcludeByFile));
        }

        if(AppVeyor.IsRunningOnAppVeyor)
        {
            AppVeyor.UploadTestResults(resultsFile, AppVeyorTestResultsType.NUnit3);
        }
    })
);


Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./Source",
    testDirectoryPath: "./Tests",
    title: "Anori.Extensions",
    repositoryOwner: "anorisoft",
    repositoryName: "Anori.Extensions",
    appVeyorAccountName: "anorisoft",
    shouldGenerateDocumentation: true,
    shouldRunDupFinder: false,
	shouldRunCodecov: true,
    shouldRunCoveralls: true,
    shouldRunDotNetCorePack: true);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    dupFinderExcludePattern: new string[]
    {
        BuildParameters.RootDirectoryPath + "/Source/Anori.Extensions*/**/*.AssemblyInfo.cs",
        BuildParameters.RootDirectoryPath + "/Tests/**/*.cs"
    },
    testCoverageFilter: "+[*]* -[NUnit*]* -[*.Tests]* -[EmptyFiles]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");


  

Build.RunDotNetCore();
