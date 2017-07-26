#r @"..\tools\FAKE.Core\tools\FakeLib.dll"
open Fake 
open Fake.XUnit2Helper
open Fake.Git.Information

let commit = getCurrentSHA1(".")

let authors = ["Phil Haack"]

// project name and description
let projectName = "Rothko"
let projectDescription = "Abstractions!"
let projectSummary = "This package is a modified signed build of Rothko from https://github.com/editor-tools/Rothko/commit/" + commit

// directories
let buildDir = "./src/bin"
let packagingDir = "./packaging/"
let testResultsDir = "./testresults"

RestorePackages()

let releaseNotes = 
    ReadFile ".\ReleaseNotes.md"
    |> ReleaseNotesHelper.parseReleaseNotes

Target "Clean" (fun _ ->
    CleanDirs [buildDir; packagingDir; testResultsDir]
)

open Fake.AssemblyInfoFile

Target "AssemblyInfo" (fun _ ->
    CreateCSharpAssemblyInfo "./SolutionInfo.cs"
      [ Attribute.Product projectName
        Attribute.Version releaseNotes.AssemblyVersion
        Attribute.FileVersion releaseNotes.AssemblyVersion ]
)

Target "BuildApp" (fun _ ->
    MSBuildWithDefaults "Build" ["./Rothko.sln"]
    |> Log "AppBuild-Output: "
)

Target "UnitTests" (fun _ ->
    !! "./Tests/bin/Release/**/Tests.dll"
    |> xUnit2 (fun p -> 
            {p with 
                XmlOutput = true
                OutputDir = testResultsDir })
)

Target "Package" (fun _ ->
    let net45Dir = packagingDir @@ "lib/net45/"
    CleanDirs [net45Dir]

    CopyFile net45Dir (buildDir @@ "Release/rothko.dll")
    CopyFiles packagingDir ["LICENSE-MIT.txt"; "README.md"; "ReleaseNotes.md"]

    NuGet (fun p -> 
        {p with
            Authors = authors
            Project = projectName
            Description = projectDescription
            OutputPath = packagingDir
            Summary = projectSummary
            WorkingDir = packagingDir
            Version = releaseNotes.AssemblyVersion + "-ghfvs"
            ReleaseNotes = toLines releaseNotes.Notes
            AccessKey = getBuildParamOrDefault "nugetkey" ""
            Publish = hasBuildParam "nugetkey" }) "Rothko.nuspec"
)


Target "Default" DoNothing

"Clean"
   ==> "AssemblyInfo"
   ==> "BuildApp"
   ==> "UnitTests"
   ==> "Default"
   ==> "Package"

RunTargetOrDefault "Default"