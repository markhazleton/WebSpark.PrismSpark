# Projects and dependencies analysis

This document provides a comprehensive overview of the projects and their dependencies in the context of upgrading to .NETCoreApp,Version=v10.0.

## Table of Contents

- [Executive Summary](#executive-Summary)
  - [Highlevel Metrics](#highlevel-metrics)
  - [Projects Compatibility](#projects-compatibility)
  - [Package Compatibility](#package-compatibility)
  - [API Compatibility](#api-compatibility)
- [Aggregate NuGet packages details](#aggregate-nuget-packages-details)
- [Top API Migration Challenges](#top-api-migration-challenges)
  - [Technologies and Features](#technologies-and-features)
  - [Most Frequent API Issues](#most-frequent-api-issues)
- [Projects Relationship Graph](#projects-relationship-graph)
- [Project Details](#project-details)

  - [WebSpark.PrismSpark.Demo\WebSpark.PrismSpark.Demo.csproj](#websparkprismsparkdemowebsparkprismsparkdemocsproj)
  - [WebSpark.PrismSpark.Tests\WebSpark.PrismSpark.Tests.csproj](#websparkprismsparktestswebsparkprismsparktestscsproj)
  - [WebSpark.PrismSpark\WebSpark.PrismSpark.csproj](#websparkprismsparkwebsparkprismsparkcsproj)


## Executive Summary

### Highlevel Metrics

| Metric | Count | Status |
| :--- | :---: | :--- |
| Total Projects | 3 | All require upgrade |
| Total NuGet Packages | 8 | All compatible |
| Total Code Files | 77 |  |
| Total Code Files with Incidents | 9 |  |
| Total Lines of Code | 14287 |  |
| Total Number of Issues | 13 |  |
| Estimated LOC to modify | 7+ | at least 0.0% of codebase |

### Projects Compatibility

| Project | Target Framework | Difficulty | Package Issues | API Issues | Est. LOC Impact | Description |
| :--- | :---: | :---: | :---: | :---: | :---: | :--- |
| [WebSpark.PrismSpark.Demo\WebSpark.PrismSpark.Demo.csproj](#websparkprismsparkdemowebsparkprismsparkdemocsproj) | net9.0 | 🟢 Low | 1 | 7 | 7+ | AspNetCore, Sdk Style = True |
| [WebSpark.PrismSpark.Tests\WebSpark.PrismSpark.Tests.csproj](#websparkprismsparktestswebsparkprismsparktestscsproj) | net9.0 | 🟢 Low | 1 | 0 |  | DotNetCoreApp, Sdk Style = True |
| [WebSpark.PrismSpark\WebSpark.PrismSpark.csproj](#websparkprismsparkwebsparkprismsparkcsproj) | net9.0 | 🟢 Low | 1 | 0 |  | ClassLibrary, Sdk Style = True |

### Package Compatibility

| Status | Count | Percentage |
| :--- | :---: | :---: |
| ✅ Compatible | 8 | 100.0% |
| ⚠️ Incompatible | 0 | 0.0% |
| 🔄 Upgrade Recommended | 0 | 0.0% |
| ***Total NuGet Packages*** | ***8*** | ***100%*** |

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 1 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 6 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 8620 |  |
| ***Total APIs Analyzed*** | ***8627*** |  |

## Aggregate NuGet packages details

| Package | Current Version | Suggested Version | Projects | Description |
| :--- | :---: | :---: | :--- | :--- |
| coverlet.collector | 6.0.4 |  | [WebSpark.PrismSpark.Tests.csproj](#websparkprismsparktestswebsparkprismsparktestscsproj) | ✅Compatible |
| DotNet.ReproducibleBuilds | 1.2.25 |  | [WebSpark.PrismSpark.csproj](#websparkprismsparkwebsparkprismsparkcsproj) | ✅Compatible |
| Microsoft.NET.Test.Sdk | 17.14.1 |  | [WebSpark.PrismSpark.Tests.csproj](#websparkprismsparktestswebsparkprismsparktestscsproj) | ✅Compatible |
| Microsoft.SourceLink.GitHub | 8.0.0 |  | [WebSpark.PrismSpark.csproj](#websparkprismsparkwebsparkprismsparkcsproj) | ✅Compatible |
| WebSpark.Bootswatch | 1.20.1 |  | [WebSpark.PrismSpark.Demo.csproj](#websparkprismsparkdemowebsparkprismsparkdemocsproj) | ✅Compatible |
| WebSpark.HttpClientUtility | 1.1.0 |  | [WebSpark.PrismSpark.Demo.csproj](#websparkprismsparkdemowebsparkprismsparkdemocsproj) | ✅Compatible |
| xunit | 2.9.3 |  | [WebSpark.PrismSpark.Tests.csproj](#websparkprismsparktestswebsparkprismsparktestscsproj) | ✅Compatible |
| xunit.runner.visualstudio | 3.1.1 |  | [WebSpark.PrismSpark.Tests.csproj](#websparkprismsparktestswebsparkprismsparktestscsproj) | ✅Compatible |

## Top API Migration Challenges

### Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |

### Most Frequent API Issues

| API | Count | Percentage | Category |
| :--- | :---: | :---: | :--- |
| T:System.Text.Json.JsonDocument | 4 | 57.1% | Behavioral Change |
| M:Microsoft.AspNetCore.Builder.ExceptionHandlerExtensions.UseExceptionHandler(Microsoft.AspNetCore.Builder.IApplicationBuilder,System.String) | 1 | 14.3% | Behavioral Change |
| M:Microsoft.Extensions.Logging.ConsoleLoggerExtensions.AddConsole(Microsoft.Extensions.Logging.ILoggingBuilder) | 1 | 14.3% | Behavioral Change |
| M:System.TimeSpan.FromMinutes(System.Int64) | 1 | 14.3% | Source Incompatible |

## Projects Relationship Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart LR
    P1["<b>📦&nbsp;WebSpark.PrismSpark.csproj</b><br/><small>net9.0</small>"]
    P2["<b>📦&nbsp;WebSpark.PrismSpark.Tests.csproj</b><br/><small>net9.0</small>"]
    P3["<b>📦&nbsp;WebSpark.PrismSpark.Demo.csproj</b><br/><small>net9.0</small>"]
    P2 --> P1
    P3 --> P1
    click P1 "#websparkprismsparkwebsparkprismsparkcsproj"
    click P2 "#websparkprismsparktestswebsparkprismsparktestscsproj"
    click P3 "#websparkprismsparkdemowebsparkprismsparkdemocsproj"

```

## Project Details

<a id="websparkprismsparkdemowebsparkprismsparkdemocsproj"></a>
### WebSpark.PrismSpark.Demo\WebSpark.PrismSpark.Demo.csproj

#### Project Info

- **Current Target Framework:** net9.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** AspNetCore
- **Dependencies**: 1
- **Dependants**: 0
- **Number of Files**: 43
- **Number of Files with Incidents**: 5
- **Lines of Code**: 2821
- **Estimated LOC to modify**: 7+ (at least 0.2% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["WebSpark.PrismSpark.Demo.csproj"]
        MAIN["<b>📦&nbsp;WebSpark.PrismSpark.Demo.csproj</b><br/><small>net9.0</small>"]
        click MAIN "#websparkprismsparkdemowebsparkprismsparkdemocsproj"
    end
    subgraph downstream["Dependencies (1"]
        P1["<b>📦&nbsp;WebSpark.PrismSpark.csproj</b><br/><small>net9.0</small>"]
        click P1 "#websparkprismsparkwebsparkprismsparkcsproj"
    end
    MAIN --> P1

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 1 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 6 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 3464 |  |
| ***Total APIs Analyzed*** | ***3471*** |  |

<a id="websparkprismsparktestswebsparkprismsparktestscsproj"></a>
### WebSpark.PrismSpark.Tests\WebSpark.PrismSpark.Tests.csproj

#### Project Info

- **Current Target Framework:** net9.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** DotNetCoreApp
- **Dependencies**: 1
- **Dependants**: 0
- **Number of Files**: 10
- **Number of Files with Incidents**: 2
- **Lines of Code**: 735
- **Estimated LOC to modify**: 0+ (at least 0.0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["WebSpark.PrismSpark.Tests.csproj"]
        MAIN["<b>📦&nbsp;WebSpark.PrismSpark.Tests.csproj</b><br/><small>net9.0</small>"]
        click MAIN "#websparkprismsparktestswebsparkprismsparktestscsproj"
    end
    subgraph downstream["Dependencies (1"]
        P1["<b>📦&nbsp;WebSpark.PrismSpark.csproj</b><br/><small>net9.0</small>"]
        click P1 "#websparkprismsparkwebsparkprismsparkcsproj"
    end
    MAIN --> P1

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 929 |  |
| ***Total APIs Analyzed*** | ***929*** |  |

<a id="websparkprismsparkwebsparkprismsparkcsproj"></a>
### WebSpark.PrismSpark\WebSpark.PrismSpark.csproj

#### Project Info

- **Current Target Framework:** net9.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 0
- **Dependants**: 2
- **Number of Files**: 46
- **Number of Files with Incidents**: 2
- **Lines of Code**: 10731
- **Estimated LOC to modify**: 0+ (at least 0.0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (2)"]
        P2["<b>📦&nbsp;WebSpark.PrismSpark.Tests.csproj</b><br/><small>net9.0</small>"]
        P3["<b>📦&nbsp;WebSpark.PrismSpark.Demo.csproj</b><br/><small>net9.0</small>"]
        click P2 "#websparkprismsparktestswebsparkprismsparktestscsproj"
        click P3 "#websparkprismsparkdemowebsparkprismsparkdemocsproj"
    end
    subgraph current["WebSpark.PrismSpark.csproj"]
        MAIN["<b>📦&nbsp;WebSpark.PrismSpark.csproj</b><br/><small>net9.0</small>"]
        click MAIN "#websparkprismsparkwebsparkprismsparkcsproj"
    end
    P2 --> MAIN
    P3 --> MAIN

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 4227 |  |
| ***Total APIs Analyzed*** | ***4227*** |  |

