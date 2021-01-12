Unity editor script for improved default namespaces in newly created scripts.
By default this script omits 2 levels of subdirectories, e.g.
1. _Assets/Scripts/Script.cs_
```c#
namespace DefaultProjectNamespace {

}
```

2. _Assets/Scripts/Actors/Player/Script.cs_
```c#
namespace DefaultProjectNamespace.Actors.Player {

}
```
HOW TO USE:
1. Place **SetDefaultNamespace** script in desired project **Assets/Editor** directory.
2. Edit **81-C# Script-NewBehaviourScript.cs** file (by default found in __C:\Program Files\Unity\Hub\Editor\\*{your_unity_version}*\\Editor\Data\Resources\ScriptTemplates__ or __C:\Program Files\Unity\Editor\Data\Resources\ScriptTemplates__):
```c#
#NAMESPACE#
    // Rest of the template goes here
#NAMESPACEEND#
```
