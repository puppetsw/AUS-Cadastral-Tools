# Cadastral Tools

![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.6.1-blue)
![AutoCAD](https://img.shields.io/badge/AutoCAD-2022-red)
![Platforms](https://img.shields.io/badge/Plugins-Windows-lightgray.svg)
[![License](http://img.shields.io/:license-MIT-blue.svg)](http://opensource.org/licenses/MIT)

# Description

Cadastral Tools is a set of tools/commands for cadastral drafting in Autodesk AutoCAD. This plugin contains commands to help with point creation and common measurement practices with cadastral surveying.

The library was originally developed for AutoCAD R21.0 (2017) and `.NET Framework 4.6.1`. I have included project files for later releases of Autodesk AutoCAD.

# Commands
* `InverseCL` to display inverse information between two points in the command window. 
* `InverseOS` to display similar information as the `InverseCL` command but displays on the screen using transient graphics.
* `InverseCHOS` to display inverse information along a baseline specified by two points at perpendicular offsets.
* `PtDistanceOnline` to create points on a baseline between two specified points. 
* `PtIntFourPts` to create a point at the intersection of four points.
* `PtBetweenPts` to create a point between two specified points.
* `PtOffsetBetweenPts` to create a point between two specified points at an offset from the baseline.
* `PtIntAngleDistance` to create a point at the intersection of a bearing and a distance.
* `PtIntDistances` to create a point at the intersection of two distances.
* `PtIntAngles` to create a point at the intersection of two bearings.
* `PtAngleDistance` to create a point at a bearing and distance.
* `PtOffsetLine` to create a point at an offset from a found intersection.
* `PtProdOfLineDist` to create a point on the production of a line at a specified distance.

## Screenshots

Example of INVERSECHOS command.

![recordingexample1](https://user-images.githubusercontent.com/79826944/209060657-dab86d82-03af-47b7-9838-ec5d6f2bba45.gif)

## Building the source

### 1. Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- .NET Framework 4.6.1 to 4.8.1
- Git for Windows
    
### 2. Cloning the repo

```
git clone https://github.com/puppetsw/AUS-Cadastral-Tools
```

### 3. Running the plugin

To build `Cadastral Tools`, open the `AUS-Cadastral-Tools.sln` item in Visual Studio. 

If you need to build for a different release of AutoCAD, you can use one of the exisitng release projects by removing the `AUS-Cadastral-Tools.R21` project and adding the relevant existing project from the source directory.

Right click on `AUS-Cadastral-Tools.R21` project in the solution explorer and select Properties.

Click the `Debug` tab and modify the `Start external program` path to match your version of AutoCAD. See below image.

![image](https://user-images.githubusercontent.com/79826944/209059436-bd32d16c-2193-432c-8c1c-9bdc8be63505.png)


## Contributing

Feel free to contribute in anyway you see fit.

## License

This plugin is licensed under the terms of the [MIT License](http://opensource.org/licenses/MIT). Please see the [LICENSE](LICENSE) file for full details.

