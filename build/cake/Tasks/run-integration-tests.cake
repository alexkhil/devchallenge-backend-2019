Task("Run-Integration-Tests")
    .Does(() =>
    {
        var dotNetCoreTestSettings = new DotNetCoreTestSettings
        {
            Configuration = configuration,
            NoRestore = false,
            NoBuild = false
        };

        var projectFiles = GetFiles(Paths.UnitTests);
        foreach(var file in projectFiles)
        {
            DotNetCoreTest(file.FullPath, dotNetCoreTestSettings);
        }
    });