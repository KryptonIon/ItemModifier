# Overview
Gets the IDs of an item.

# Usage
Select an item in the hotbar like [this](HotbarSelection), then do `/id [ID Type]`.

`/id` returns ItemID and necessary values.<sup>[^1](#params)</sup>

`/id buff` returns BuffID of the item.

# Parameters
| Property Name                         |
|---------------------------------------|
| ItemID Item or I                      |
| TileID Tile or T                      |
| BuffID Buff or B                      |
| ProjectileID ProjID Proj P Shoot or S |

# Notes
<a name="params">Doing `/id` with no arguments will not just return the ItemID but will also return TileID, BuffID or ProjectileID if necessary(can be disabled by `/settings shun false`).

Parameters are Capitalization Insensitive.