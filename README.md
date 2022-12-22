# Cadastral Tools

Cadastral Tools is a set of tools/commands for cadastral drafting in Autodesk AutoCAD.

## Building from source

### 1. Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- .NET Framework 4.6.1
- Git for Windows
    
### 2. Clone the repository

```
git clone https://github.com/puppetsw/AUS-Cadastral-Tools
```

This will create a local copy of the repository.

### 3. Running the project

To build Cadastral Tools for development, open the `AUS-Cadastral-Tools.sln` item in Visual Studio. 

Change the processor architecture type to x64 in the main toolbar of Visual Studio. See below image.
![image](https://user-images.githubusercontent.com/79826944/209029395-ae4d389b-1575-4ca5-882c-2e662b393cb1.png)

Currently the project is setup to run on AutoCAD 2017 but this can be changed by modifying the `Start action` in the project properties.

Right click on `AUS-Cadastral-Tools` in the solution explorer and select Properties.

Click the `Debug` tab and modify the `Start external program` path to match your version of AutoCAD. See below image. You will also need to update the references for the Autodesk DLLs such as `accoremgd.dll` `accui.dll` `acmgd.dll`. 

![image](https://user-images.githubusercontent.com/79826944/209059436-bd32d16c-2193-432c-8c1c-9bdc8be63505.png)


## Contributing

Feel free to contribute in anyway you see fit.

## Screenshots

Example of INVERSECHOS command.

![recordingexample1](https://user-images.githubusercontent.com/79826944/209060657-dab86d82-03af-47b7-9838-ec5d6f2bba45.gif)

## List of avaliable commands
| Command Name | Description |
| ------------------------ | --------------------------------- |
| INVERSECL | Displays inverse information between two points in the command window. Displays bearing, distance, delta X, delta Y, delta Z and slope % values. |
| INVERSEOS | Displays same information as the INVERSECL command but displays on the screen using transient graphics. |
| INVERSECHOS | Displays inverse information along a base line specified by two points at perpendicular offsets. |
| PTDISTANCEONLINE | Create points on a base line between two specified points. New points are created using a typed distance. Elevation is interploated using the Z value of the specified points. |
| PTINTFOURPTS | Creates a point at the intersection of four points. |
| PTBETWEENPTS | Creates a point between two specified points. |
| PTOFFSETBETWEENPTS | Creates a point between two specified points at an offset from the base line. |
| PTINTANGLEDISTANCE | Creates a point at the intersection of a bearing and a distance. User can pick which intersection point to use from the two possible calculations. |
| PTINTDISTANCES | Creates a point at the intersection of two distances. User can pick which intersection point to use from the two possible calculations. |
| PTINTANGLES | Creates a point at the intersection of two bearings. |
| PTANGLEDISTANCE | Creates a point at a bearing and distance. |
| PTOFFSETLINE | Creates a point at an offset from a found intersection. |
| PTPRODOFLINEDIST | Creates a point on the production of a line at a specified distance. |

