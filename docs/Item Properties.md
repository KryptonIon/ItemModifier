---
title: Item Properties
nav_order: 5
---
# Item Properties
{:.no_toc}
The different properties of an item.
{:.fs-6 .fw-300}

---

## Table of contents
{:.no_toc .text-delta}

1. TOC
{:toc}

---

## Auto Use
If true, an item will be repeatedly used when the mouse is held down, else an item will only trigger once even if the mouse is held down.

---

## Consumable
If true, item will be consumed upon use.

A mana flower will still consume any item that heals mana despite this property being false.

---

## Potion Sickness
If true, potion sickness will be inflicted when the item is used.

---

## Damage Type
Options:
1. Melee
2. Magic
3. Ranged
4. Summon
5. Thrown

If any of the 5 is selected, the item will inflict said type of damage. If none of the 5 is selected, the item will inflict still damage but without a type.

---

## Accessory
If true, the item can be equipped as an accessory.

---

## Damage
Damage inflicted by the item.

---

## Crit Chance
Crit of an item; Chance that the item will inflict Critical Damage.

All weapons have a base crit chance, more on that [here](https://terraria.gamepedia.com/Critical_hit){:target="_blank"}.

---

## Knockback
Amount of knockback inflicted by the item.

Negative knockback doesn't do anything.

---

## Projectile Shot
Projectile that the item will shoot.

The vanilla projectile count is 713(1.3.5.3).

This property will conflict with Place Tile, a tile can't be placed if Projectile Shot is set.

---

## Shoot Speed
Gets/Modifies the Shoot Speed of an item.

If set to negative, the projectile will shoot towards the opposite direction.

Projectiles have a terminal velocity.

If a projectile goes fast enough, it may phase through blocks.

An ice rod with a shoot speed of 0 instantly places an ice block no matter the distance.

---

## Place Tile
Tile to be placed when item is used.

This property will conflict with Projectile Shot, a tile can't be placed if Projectile Shot is set.

This property will conflict with Buff Inflicted, a buff won't be inflicted as long a tile is being placed.

The vanilla tile count is 469(1.3.5.3).

The tModLoader tile count without any mods loaded is 471(v0.11.6.2). The tiles 470 and 471 are the unloaded tiles from tModLoader.

The TileID 0 belongs to the Dirt Block, while the TileID -1 is the default value for items.

---

## Added Range
Additional range the item will grant or take.

This can be seen in Copper Tools(-3 Range), Luminite Tools(+4 Range) or The Grand Design(+20 Range).

---

## Buff Inflicted
Buff to be inflicted when item is used.

---

## Buff Duration
(Redundant if Buff Inflicted is 0)Duration of the buff.

Using a negative duration will still inflict the buff but only for fractions of a second.

This property will conflict with Place Tile, a buff won't be inflicted as long a tile is being placed.

---

## HP Healed
Amount of HP healed when item is used.

---

## Mana Healed
Amount of Mana healed when item is used.

Even if the item is unconsumable, if Mana Healed is set, the item can still be consumed by a Mana Flower.

---

## Axe Power
Axe Power of the item.

---

## Pickaxe Power
Pickaxe Power of the item.

---

## Hammer Power
Hammer Power of the item.

---

## Amount
Amount of the item.

Setting this property to negative will make the item disappear.

---

## Max Stack
Defines how much an item can stack.

---

## Animation Duration
Duration of the item use animation.

Setting this property to negative will glitch the item, although the mod blocks it.

---

## Use Duration
Duration of the item being used.

Setting this property to negative will glitch the item, although the mod blocks it.

---

## Defense
Amount of Defense the item will grant when worn.

---

## Fishing Power
Fishing Pole Power of the item.

If the property is negative and the item is a fishing pole, the pole will fire infinite bobbers without a fishing line connected to them.

---

## Item Scale
Size of the item.

---

## Use Style
Options:
1. Swing
2. Drink
3. Stab
4. Above Head
5. Held

If Swing is selected, the item will be swung like a Broadsword,

If Drink is selected, the item will be drunk like a Potion,

if Stab is selected, the item will stab like a Shortsword,

If Above Head is selected, the item will be held above the head like a Life Crystal or a Mana Crystal,

If Held is selected, the item will be held like Staff, eg. Amethyst STaff, Poison Staff.