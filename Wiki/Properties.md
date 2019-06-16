# Overview
Gets an item's properties. The Properties Command ignore ShowProperties setting.

# Usage
To see the Properties of an item, select it in the hotbar like [this](HotbarSelection), then do `/properties`. A list of properties will be returned upon successful execution. To get specific properties, do `/properties (Parameters)[Property]` A full list of available implemented properties are located [here](Parameters). Examples:

`/properties` Will display the properties of the currently selected item (Unnecessary properties are cleared, can be disabled using `/setting shun true` more on settings [here](Settings#shun)).

`/properties p h s ct` Will only get the properties: Pickaxe Power, Axe Power, Shoot, and CreateTile. This command uses the same parameters as GenerateItem and Set.

# Notes
Uses the same [parameters](Parameters) as GenerateItem and Set.