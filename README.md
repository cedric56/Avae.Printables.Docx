# Avae.Printables.Docx

A crossplatform library for printing.

# Features

Cross-Platform : Leverage APIs adapted for multiple environments.

MIT Licensed: Freely use, modify, and distribute under the permissive MIT License.

# Getting Started

Follow these steps to integrate Avae.Printables into your Avalonia project.

# Prerequisites

An Avalonia project set up with .NET.

# Installation

Add Nuget Avae.Printables.Docx to Your Shared Project

# Configuration

Enable Printables.

````
 using Avae.Printables;
 public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePrintables()
            .UseDocxPrintables()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
````
Android

````
protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
{
    return base.CustomizeAppBuilder(builder)
        .UsePrintables(this, this)
        .UseDocxPrintables()
        .WithInterFont()
        .UseReactiveUI();
}
````

iOS
````
protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
{
    return base.CustomizeAppBuilder(builder)
        .UsePrintables()
        .UseDocxPrintables()
        .WithInterFont()
        .UseReactiveUI();
}
````

Browser (Please add nuget package Avae.Printables to project to make sure printing.js in wwwroot folder)

````
private static Task Main(string[] args) => BuildAvaloniaApp()
            .WithInterFont()
             .StartBrowserAppAsync("out").ContinueWith(async t =>
             {
                 await JSHost.ImportAsync("printing", "/printing.js");
             });

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePrintables()
            .UseDocxPrintables();
````

Linux (the following workflow is required)

https://github.com/lytico/GtkSharp.Workload

# Example: Print MainView

````
using Avae.Printable;

Printables.PrintAsync("C://Temp//test.docx");

````

# Built With

This package builds upon the excellent work of:

AvaloniaUI

OpenXmlPowerTools.Core

# License

Avae.Printables.Docx is licensed under the MIT License.

# Contributing

Contributions are welcome! Please submit issues or pull requests to the GitHub repository. Ensure your code follows the project’s coding standards.