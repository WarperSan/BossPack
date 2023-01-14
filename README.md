# Boss Pack
This mod adds multiple bosses with their own mechanic. All details are explained in each section.

Are the bosses possible to beat in a normal game ? I have no idea. GL

Table of Content:
1. [The Demon Prince (PRINCE)](https://github.com/WarperSan/BossPack#1-the-demon-prince-prince)
2. [Queen of Jaws (CEVER)](https://github.com/WarperSan/BossPack#1-the-demon-prince-prince)
3. [Bloontonium Expert (ETDB)](https://github.com/WarperSan/BossPack/blob/main/README.md#3-bloontonium-expert-etdb)
4. [Flame of Terror](https://github.com/WarperSan/BossPack/blob/main/README.md#4-flame-of-terror)
5. [Ghost King (WIGHT) & Will-o'-the-wisp Bloon](https://github.com/WarperSan/BossPack/blob/main/README.md#5-ghost-king-wight)
6. [Goemon's Student (NINJA)](https://github.com/WarperSan/BossPack/edit/main/README.md#6-goemons-student)
7. [If you want to use BossPack's UI with your own custom boss](https://github.com/WarperSan/BossPack/edit/main/README.md#for-programmers)

Credits to all the people who did the art !

Notes: 
- A Red bloon has a speed of 25 and a Bad has a speed of 4.5
# 1. The Demon Prince (PRINCE)
**Immunity**: Purple

**Skull Effect**: Remove 50 * Math.Floor(Current Round / 40) lives
(Round 60 is also 50)

**Timer Effect**: Remove 2 lives

| Tier  | Health | Speed | # Skulls | Timer (s) |
| ------------- | ------------- | ------------- | ------------- | ------------- |
| 1  | 30k | 1.5 | 3 | 9 |
| 2  | 100k | 1.875 | 3 | 9 |
| 3  | 500k | 2.25 | 3 | 6 |
| 4  | 1M | 2.625 | 4 | 6 |
| 5  | 5M | 3 | 5 | 3 |

Credit: 
[Art](https://www.reddit.com/r/btd6/comments/bf7evw/new_bloon_vampire/)

# 2. Queen of Jaws (CEVER)
**Immunity**: None

**Skull Effect**: Speeds up for a short amount of time

**Timer Effect**: Each 6 seconds, she selects a random tower and if it's a mechanical tower, remove 5 lives. If the tower is alive, it sells it at 50% worth and remove 10 lives

Mechanical Towers:
- Dart 4xx
- Bomb 000
- Tack 000
- Sub 000
- Buccaneer 244
- Ace 000
- Heli 000
- Dartling 554
- Super Monkey 335
- Farm 333
- Spike 000
- Village x3x
The tower must have at least one of the tier >=. (A Dart 3xx will count as Alive when a 520 will count as Mechanical)

| Tier  | Health | Speed | # Skulls |
| ------------- | ------------- | ------------- | ------------- |
| 1  | 4.5k | 50 | 1 |
| 2  | 9k | 54 | 2 |
| 3  | 13.5k | 58 | 3 |
| 4  | 18k | 62 | 4 |
| 5  | 22.5k | 66 | 5 |

Credit: 
[Art & Mechanic Ideas](https://bloonsconception.fandom.com/wiki/Cever)

# 3. Bloontonium Expert (ETDB)
**Immunity**: Lead

**Skull Effect**: Spawns 100 Purples on the same spot, even on tier 1

**Timer Effect**: Regenerates 0.75% of it's max health each second

| Tier  | Health | Speed | # Skulls |
| ------------- | ------------- | ------------- | ------------- |
| 1  | 64k | 4.5 | 3 |
| 2  | 128k | 3.93 | 3 |
| 3  | 456k | 3.375 | 4 |
| 4  | 768k | 2.75 | 5 |
| 5  | 2.304M | 2.25 | 7 |

Credit: 
[Art](https://bloonsconception.fandom.com/wiki/E.T.D.B)

# 4. Flame of Terror
**Immunity**: Purple

**Skull Effect**: Removes 40% of your current cash amount

**Timer Effect**: Spawns 60 * Math.Floor(Current Round / 40) Ceramics every 30 seconds

| Tier  | Health | Speed | # Skulls |
| ------------- | ------------- | ------------- | ------------- |
| 1  | 70k | 6.5 | 1 |
| 2  | 150k | 6.5 | 2 |
| 3  | 350k | 6.5 | 3 |
| 4  | 800k | 6.5 | 5 |
| 5  | 1.25M | 6.5 | 7 |

Credit: 
[Art & Name](https://bloonsconception.fandom.com/wiki/Flame_of_Terror)

# 5. Ghost King (WIGHT)
**Immunity**: Purple

**Skull Effect**: Spawns 4 Will-o'-the-wisp Bloons which have 5k hp and makes the boss invicible until all the Will-o'-the-wisp bloons are popped

**Timer Effect**: Spawns 10 * (Current Round) / 20 Purples every second

| Tier  | Health | Speed | # Skulls |
| ------------- | ------------- | ------------- | ------------- |
| 1  | 30k | 1.5 | 3 |
| 2  | 100k | 1.875 | 3 |
| 3  | 500k | 2.25 | 3 |
| 4  | 1M | 2.625 | 4 |
| 5  | 5M | 3 | 5 |

# **Will-o'-the-wisp Bloon**
Bloon immune to every attack except Ice Monkey's attacks

Credit: 
[Art & Name](https://bloonsconception.fandom.com/wiki/W.I.G.H.T.)
 
# 6. Goemon's Student
**Immunity**: Zebra

**Skull Effect**: Stuns for 2 min (1 min for paragons) the most expensive tower that isn't a farm, a village and isn't already stunned

**Timer Effect**: Spawns X Camo Ddt, where X is the current tier of the boss

| Tier  | Health | Speed | # Skulls | Timer (s) |
| ------------- | ------------- | ------------- | ------------- | ------------- |
| 1  | 18k | 3 | 2 | 30 |
| 2  | 56.2k | 3 | 2 | 26.25 |
| 3  | 248k | 3 | 2 | 22.5 |
| 4  | 556.8k | 3 | 2 | 18.75 |
| 5  | 1.912M | 3 | 2 | 15 |
 
Credit: Art, Name & Main Mechanic from thijs

# If you want to use BossPack's UI with your own custom boss
If you create a custom boss and you give it a HealthPercentTriggerModel, your boss will be able to use the boss bar that is given by BossPack. 

Note that BossPack manages bosses that have **evenly spaced skulls** (e.g.: 2 skulls will be placed at 66% and 33%). Plus, the boss icon is taken from Bloon.bloonModel.icon
