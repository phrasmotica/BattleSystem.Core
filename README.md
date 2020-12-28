# Battle System

This is a library that provides functionality for characters battling against other, targeting .NET 5.0.

[![NuGet](https://img.shields.io/nuget/v/BattleSystem.svg?logo=nuget)](https://www.nuget.org/packages/BattleSystem)

## Character

- has `M` max health
- starts with `M` current health
- dies if current health reaches 0
- has some number of [moves](#move)
- has attack, defence and speed [stats](#stat)

<a name="move"></a>
## Move

- has `U` max uses
- starts with `U` remaining uses
- cannot be used if remaining uses reaches 0
- contains some number of [move actions](#move-actions) executed in order

<a name="move-actions"></a>
## Move Actions

### Attack

- has `P` power
- can be configured to target any number of characters
- damages target for either:
    - `P` health
    - `P` percent of the target's max health
    -  `max{1, P * (A - D)}` health where:
        - `A` is the user's attack stat
        - `D` is the target's defence stat

### Buff

- has set of percentage changes for some stats
- changes those stats by the corresponding percentages
- can be configured to target any number of characters

### Heal

- has `H` power
- heals character for either up to `H` health or up to `H`% of their max health

### Protect

- nullifies all damage from next attack against the move target

<a name="stat"></a>
## Stat

- has `V` integer starting value
- has `M` decimal multiplier, starting at 1
- `M` can be altered by buffs

### Attack

- determines strength of attack actions that calculate damage based on stats

### Defence

- determines resistance to attack actions that calculate damage based on stats

### Speed

- determines the order in which characters' moves are processed (highest first)
