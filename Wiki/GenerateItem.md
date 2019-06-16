# Overview
The GenerateItem command can generate a normal item, or optionally a modified item. 2 Messages will show in chat describing the item, one of them can be disabled by doing `/setting shpr false` more on settings [here](Settings).

# Usage
To use the GenerateItem Command, simply join a world and do `/gi [ItemID]`. To generate a modified item instead of a normal one do `/gi [ItemID] (Parameters)[Property] [Value]`. Examples:

`/gi 757` Spawns a normal Terra Blade.

`/gi 757 s 5` Spawns a Terra Blade that shoots Jester's Arrows.

`/gi 757 s 5 ct 26 hl 50` Spawns a Terra Blade that shoots Jester's Arrows and places Demon Altars while healing 50 hp.

`/setitem magic mirr` `/gi` Spawns a Magic Mirror.

`/setitem magic mirror` `/gi s 5 ct 26` Sets SetItem to Magic Mirror and Spawns a Magic Mirror that places Demon Altars and shoots Jester's Arrows.

# Parameters
A list of parameters can be seen in-game via `/gi params` or [here](Parameters).