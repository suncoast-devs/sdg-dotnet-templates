# SDG Console template

This is the template app SDG uses to start off learning C# and .NET

## What is does

This provides a basic console application with the following features:

- A gitignore
- A README
- Tasks that allows users to push to GitHub Automatically.

## How to work with Custom Templates

First, [read the docs](https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates).

Now with that in mind, The project has 3 main parts:

1. `sdg-console-template.nuspec`. This file contains the meta data for the package that is built, The only items that need touched are the `<files>` and the `<version>`

2. `SampleApp`. This is sample project. Any changes to the template happen here.

3. `SampleApp\template.config`. This contains the behavior of the package. This generally isn't touch unless you are changing the project type.

## How to update something

To update, I would recommend opening just the `SampleApp` Folder and working in that project like it was just a normal C# app. Get things working and then test it out.

## How to deploy

Install [nuget](https://docs.microsoft.com/en-us/nuget/reference/nuget-exe-cli-reference) and [set your API key for nuget.org](https://docs.microsoft.com/en-us/nuget/reference/cli-reference/cli-ref-setapikey)

1. Delete the `bin` and the `obj` folder
2. Bump the version number in the `sdg-console-template.nuspec`.
3. run `nuget pack .`
4. run `nuget push SDG.templates.Console.X.X.X.nupkg -Source https://www.nuget.org` with the correct version number

This will push it to Nuget. Nuget will index the package, and when it's done indexing (~ 1-30 minutes), it will available for install. To install on a students laptop

```sh
dotnet new --install SDG.templates.Console::X.X.X
```

To update after download, its the same command

```sh
dotnet new --install SDG.templates.Console::X.X.X
```
