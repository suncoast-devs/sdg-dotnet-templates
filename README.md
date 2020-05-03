# SDG .NET Core templates

These is the templates SDG uses to start off learning C# and .NET

> Each Project has there own readme, which is just a more specific version of this.

## How to work with Custom Templates

First, [read the docs](https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates).

Now with that in mind, The project has 3 main parts:

1. `sdg-*.nuspec`. This file contains the meta data for the package that is built, The only items that need touched are the `<files>` and the `<version>`

2. `SampleApi`. This is sample project. Any changes to the template happen here.

3. `SampleApi\template.config`. This contains the behavior of the package. This generally isn't touch unless you are changing the project type.

## How to update something

To update, I would recommend opening just the `SampleApi` Folder and working in that project like it was just a normal C# app. Get things working and then test it out.

## How to deploy

Install [nuget](https://docs.microsoft.com/en-us/nuget/reference/nuget-exe-cli-reference) and [set your API key for nuget.org](https://docs.microsoft.com/en-us/nuget/reference/cli-reference/cli-ref-setapikey)

1. Delete the `bin` and the `obj` folder
2. Bump the version number in the `sdg-*.nuspec`.
3. run `nuget pack .`
4. run `nuget push SDG.templates.X.X.X.X.X.nupkg -Source https://www.nuget.org` with the correct version number

This will push it to Nuget. Nuget will index the package, and when it's done indexing (~ 1-30 minutes), it will available for install. To install on a students laptop

```sh
dotnet new --install SDG.templates.X.X::X.X.X
```

To update after download, its the same command

```sh
dotnet new --install SDG.templates.X.X::X.X.X
```
