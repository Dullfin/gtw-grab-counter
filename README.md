# GTWGrabCounter
A grab counter mod for the Get To Work speedrunning community

This mod was built for BepInEx 5.x

## Building

Run `dotnet build GrabCounter.sln -c Release`. The project defaults to the
standard Windows Steam installation path for the game's managed assemblies.
For another installation, pass
`-p:GameManagedPath="path\to\Get To Work_Data\Managed"`.
