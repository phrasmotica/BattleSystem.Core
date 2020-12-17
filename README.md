# Battle System

## Character

- has `M` max health
- starts with `M` current health
- dies if current health reaches 0
- has 4 [moves](#move)
- has attack, defence and speed [stats](#stat)

<a name="move"></a>
## Move

- has `U` max uses
- starts with `U` remaining uses
- cannot be used if remaining uses reaches 0

### Attack

- has `P` power
- damages target for `max{1, P * (A - D)}` health where:
  - `A` is the user's attack stat
  - `D` is the target's defence stat

### Buff

- has set of percentage changes for some stats belonging to the user and enemy
- changes those stats by the corresponding percentages

### Heal

- has `H` power
- heals character for either up to `H` health or up to `H`% of their max health

<a name="stat"></a>
## Stat

- has `V` starting value
- can be altered by buffs
