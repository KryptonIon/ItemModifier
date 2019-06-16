# Item Modification Commands
This page contains a list of commands for item modification.
## Note
All of the below will require you to select an item in the hotbar like [this](HotbarSelection).

<a name="au"></a>

# AutoReuse

### Overview
Gets/Modifies the Auto Reuse of an item.

### Usage
To see the AutoReuse of an item, select it in the hotbar like [this](HotbarSelection), then do `/au`, to modify it do `/au [True|False]`. Examples:

`/au` Displays whether the AutoReuse of the currently selected item is true or false.

`/au false` Modifies the currently selected item's AutoReuse to false.

<a name="aun"></a>

### Notes
None.

<a name="ap"></a>

# Axe Power
### Overview
Gets/Modifies the Axe Power of an item.

### Usage
To see the Axe Power of an item, select it in the hotbar like [this](HotbarSelection), then do `/a`, to modify it do `/a [Axe Power]`. Examples:

`/a` Shows the Axe Power of the currently selected item.

`/a 50` Modifies the currently selected item's Axe Power to 50.

<a name="apn"></a>

### Notes
None.

<a name="bid"></a>

# Buff
### Overview
Gets/Modifies the BuffID of an item.

### Usage
To see the BuffID of an item, select it in the hotbar like [this](HotbarSelection), then do `/bid`, to modify it do `/bid [BuffID]`. Examples:

`/bid` Displays the BuffID of the currently selected item.

`/bid 175` Modifies the currently selected item's Buff to 175(Life Nebula).

<a name="bidn"></a>

### Notes
Using any of the nebula buffs may glitch them a bit(Always turns to 7 seconds).

<a name="bt"></a>

# BuffTime
### Overview
Gets/Modifies the BuffTime of an item.

### Usage
To see the BuffTime of an item, select it in the hotbar like [this](HotbarSelection), then do `/bt`, to modify it do `/bt [BuffTime]`. Examples:

`/bt` Displays the BuffTime of the currently selected item.

`/bt 20` Modifies the currently selected item's Buff Time to 20 seconds.

<a name="btn"></a>

### Notes
Using negative values(even though it's not possible due to the mod's implemented system to block it) will still make the buff appear on the top left but only for some fractions of a second.

<a name="ct"></a>

# CreateTile
### Overview
Gets/Modifies the CreateTile property of an item.

### Usage
To see the CreateTile of an item, select it in the hotbar like [this](HotbarSelection), then do `/ct`, to modify it do `/ct [TileID]`. Examples:

`/ct` Shows the CreateTile property of an item.

`/ct 26` Makes the current item place a demon altar.

<a name="ctn"></a>

### Notes 
The vanilla tile count is 469(1.3.5.3). 

When trying to make an item place the tile 470, the mod will simply ignore it and return a warning, the tile 471 is suggested instead. 

CreateTile is ignored when the current item shoots projectiles.

Tiles are not separated into 1x1 blocks, anything bigger than 1x1 will be bundled into 1 tile, eg. a table.

Despite using negative numbers do no harm, the mod still implements a feature to disable negative numbers(-1 is excluded). 

There's a secret tile in the vanilla game named Count, its TileID is 470, attempting to place this in tModLoader would place a pink block resembling the unloaded tile.

<a name="csc"></a>

# Critical Strike Chance
### Overview
Gets/Modifies the Critical Strike Chance of an item.

### Usage
To see the Critical Strike Chance of an item, select it in the hotbar like [this](HotbarSelection), then do `/c`, to modify it do `/c [Chance]`. Examples:

`/c` Displays the Critical Strike Chance of the currently selected item.

`/c 50` Modifies the currently selected item's Critical Strike Chance to 50.

<a name="cscn"></a>

### Notes
Negative Critical Strike Chance won't make the indicator disappear, it will write negative instead.

Almost all items have a base Critical Strike Chance, more on that [here](https://terraria.gamepedia.com/Critical_hit).

<a name="c"></a>

# Consumable
### Overview
Gets/Modifies the Consumability of an item.

### Usage
To see the Consumability of an item, select it in the hotbar like [this](HotbarSelection), then do `/cons`, to modify it do `/cons [True|False]`. Examples:

`/cons` Displays the Consumability of the currently selected item.

`/cons false` Modifies the currently selected item's Consumability to false.

<a name="cn"></a>

### Notes
Despite setting this value to false, in specific circumstances, the item may still get consumed.

<a name="d"></a>

# Damage
### Overview
Gets/Modifies the Damage of an item.

### Usage
To see the Damage of an item, select it in the hotbar like [this](HotbarSelection), then do `/d`, to modify it do `/d [Damage]`. Examples:

`/d` Shows the Damage of the currently selected item.

`/d 20` Modifies the currently selected item's Damage to 20.

<a name="dn"></a>

### Notes
Negative damage will make the damage and critical strike chance indicator disappear.

Negative damage will not heal the enemy, it will ignore hits altogether.

<a name="hp"></a>

# Hammer Power
### Overview
Gets/Modifies the Hammer Power of an item.

### Usage
To see the Hammer Power of an item, select it in the hotbar like [this](HotbarSelection), then do `/h`, to modify it do `/h [Hammer Power]`. Examples:

`/h` Shows the Hammer Power of the currently selected item.

`/h 50` Modifies the currently selected item's Hammer Power to 50.

<a name="hpn"></a>

### Notes
None.

<a name="hl"></a>

# HealLife
### Overview
Gets/Modifies the Heal Life of an item.

### Usage
To see the HealLife of an item, select it in the hotbar like [this](HotbarSelection), then do `/hl`, To modify it, do `/hl [HP]`. Examples:

`/hl` Displays the currently selected item's HealLife value.

`/hl 50` Makes the current item heal 50 every time it's used.

<a name="hln"></a>

### Notes
Setting this value to negative will do nothing.

Modifying this property won't make the item consumable nor apply potion sickness. Potion sickness is caused by item.potion, and consumability is item.consumable.

<a name="hm"></a>

# HealMana
### Overview
Gets/Modifies the Heal Mana of an item.

### Usage
To see the Heal Mana of an item, select it in the hotbar like [this](HotbarSelection), then do `/hm`, to modify it, do `/hm <Mana>`. Examples:

`/hm` Shows the Heal Mana value of the currently selected item.

`/hm 50` Modifies the currently selected item's Heal Mana to 50. 

<a name="hmn"></a>

### Notes
Setting this value to negative will do nothing. 

Even though the consumable value is not touched, if a mana flower is equipped the item with a modified Heal Mana value will be consumed automatically.

<a name="kb"></a>

# Knockback
### Overview
Gets/Modifies the Knockback of an item.

### Usage
To see the Knockback of an item, select it in the hotbar like [this](HotbarSelection), then do `/kb`, to modify it do `/kb [Knockback]`. Examples:

`/kb` Shows the Knockback of the currently selected item.

`/kb 50` Modifies the Knockback of the currently selected item to 50.

<a name="kbn"></a>

### Notes
Negative knockback won't pull the enemy instead of pushing it.

Negative knockback will ignore knockback altogether.

<a name="ms"></a>

# MaxStack
### Overview
Gets/Modifies the MaxStack of an item.

### Usage
To see the MaxStack of an item, select it in the hotbar like [this](HotbarSelection), then do `/ms`, to modify it do `/ms [Stack]`. Examples:

`/ms` Shows the MaxStack of the currently selected item.

`/ms 20` Modifies the MaxStack of the currently selected item to 20.

<a name="msn"></a>

### Notes
Modifying the MaxStack of items with a MaxStack of 1(Pickaxes, Axes, Tools, etc) will cause unexpected stacking behavior.

<a name="pp"></a>

# Pickaxe Power
### Overview
Gets/Modifies the Pickaxe Power of an item.

### Usage
To see the Pickaxe Power of an item, select it in the hotbar like [this](HotbarSelection), then do `/p`, to modify it do `/p [Pickaxe Power]`. Examples:

`/h` Shows the Pickaxe Power of the currently selected item.

`/h 50` Modifies the currently selected item's Pickaxe Power to 50

<a name="ppn"></a>

### Notes
None.

<a name="pot"></a>

# Potion
### Overview
Gets/Modifies the Potion of an item.

### Usage
To see the Potion of an item, select it in the hotbar like [this](HotbarSelection), then do `/pot`, to modify it do `/pot [True/False]`. Examples:

`/pot` Displays the Potion of the currently selected item.

`/pot true` Modifies the currently selected item's Potion to true.

<a name="pon"></a>

### Notes
This does not modify whether the item gives buffs or not, this defines whether an item will give the Potion Sickness debuff when the item is used.

<a name="s"></a>

# Shoot
### Overview
Gets/Modifies the Shoot property of an item.

### Usage
To see the Shoot(ProjectileID) of an item, select it in the hotbar like [this](HotbarSelection), then do `/s`, to modify it, do `/s [ProjectileID]`. Examples:

`/s` To see the Shoot property of the currently selected item.

`/s 193` To modify the Shoot property of the currently selected item to shoot smoke bombs.

<a name="sn"></a>

### Notes
The vanilla projectile count is 713(1.3.5.3).

If an item shoots projectiles, [CreateTile](CreateTile) will be ignored.

There's a secret projectile in the vanilla game named Count, its ProjectileID is 714, attempting to shoot this will result in a wall of exceptions causing lag then leading into client crash.

<a name="ss"></a>

# ShootSpeed
### Overview
Gets/Modifies the Shoot Speed of an item.

### Usage
To see the Shoot Speed of an item, select it in the hotbar like [this](HotbarSelection), then do `/ss`, to modify it, do `/ss [Speed]`. Examples:

`/ss` Shows the currently selected item's Shoot Speed.

`/ss 40` Modifies the currently selected item's Shoot Speed to 40.

<a name="ssn"></a>

### Notes
If set to negative, the projectile will shoot towards the opposite direction of what it's supposed to be.

There seems to be a cap to how fast a projectile can go, setting a projectile's Shoot Speed to 99999 won't be as dramatic as expected.

<a name="st"></a>

# Stack
### Overview
Gets/Modifies the Stack of an item.

### Usage
To see the Stack of an item, select it in the hotbar like [this](HotbarSelection), then do `/st`, to modify it do `/st [Stack]`. Examples:

`/st` Displays the Stack of the currently selected item.

`/st 20` Modifies the currently selected item's Stack to 20.

<a name="stn"></a>

### Notes
Setting stack to negative will make the item disappear.

<a name="tb"></a>

# TileBoost
### Overview
Gets/Modifies the +Range property of an item.

### Usage
To see the +Range(TileBoost) of an item, select it in the hotbar like [this](HotbarSelection), then do `/tb`, to modify it do `/tb [Tiles]`. Examples:

`/tb` Shows the current TileBoost value of the currently selected item.

`/tb 50` Modifies the TileBoost value of the currently selected item to 50, will display +50 Range when hovering above the item.

<a name="tbn"></a>

### Notes
This does not define how long a projectile can go.

It can be spotted in Copper Pickaxe/Axe (-3 Range), or Luminite Tools(+4 Range).

<a name="ua"></a>

# UseAnimation
### Overview
Gets/Modifies the UseAnimation of an item.

### Usage
To see the UseAnimation time of an item, select it in the hotbar like [this](HotbarSelection), then do `/ua`, to modify it do `/ua [UseAnimation]`. Examples:

`/ua` Shows the UseAnimation property of an item.

`/ua 20` Modifies the current item's UseAnimation to 20.

<a name="uan"></a>

### Notes
It makes the item's animation longer/shorter. 

If the value is negative a sort of curse will be set upon your character.

<a name="ut"></a>

# UseTime
### Overview
Gets/Modifies the UseTime of an item.

### Usage
To see the UseTime of an item, select it in the hotbar like [this](HotbarSelection), then do `/ut`, to modify it do `/ut [UseTime]`. Examples:

`/ut` Shows the UseTime property of an item.

`/ut 20` Modifies the current item's UseTime to 20.

<a name="utn"></a>

### Notes
UseTime has an indirect tie to how many shots/projectiles are fired/shot, eg. UseTime 10, UseAnimation 20, will fire 2 shots. 

If the value is negative a sort of curse will be set upon your character.