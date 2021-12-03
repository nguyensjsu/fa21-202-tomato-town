# Tomato Town (Replace with your Team Name and Journal)

# Team Journals
Hardwin Bui: https://github.com/nguyensjsu/fa21-202-tomato-town/blob/main/HardwinProjectJournal.md<br>
Kenneth Yang: https://github.com/nguyensjsu/fa21-202-tomato-town/blob/main/KennethProjectJournal.md

# Class Diagram

<img src="images/class-diagram.png" width="1100">

# Game State Diagram

<img src="images/game-state-diagram.png" width="500">

# Key Design Features

<h3>Composite Pattern</h3> 
In Unity, it is more efficient if there is one class calling the Update() and FixUpdate() functions. With this in mind, we used the Composite Pattern to setup the architecture in a way so that one main class, the GameManager.cs singleton class, that takes in all classes that share its Composite Pattern. The GameManager class will then call Update and FixUpdate for each of its child classes.
<br><br><img src="images/composite-pattern.png" width="500">

<h3>State Pattern</h3> 
The State Pattern was used to organize the behaviors of the main agents of the game; the player, minions, and enemies. This organization allowed for more modularization and made adding additional mechanics cleaner. Below are all the classes that implement the State Pattern:
<br><br><img src="images/state-pattern.png" width="500">

<h3>Strategy Pattern</h3> 
The Strategy Pattern is used to decide how enemies should be spawned in each level of the game. This allowed us to set the difficulty of each level. Adding additional levels in future updates would also be easy as we'd just have to make a new class that implements the pattern.
<br><br><img src="images/strategy-pattern.png" width="500">

# Asset Credits

Background Art:
https://quintino-pixels.itch.io/wasteland-plataformer-tileset<br>
Character Art:
https://clembod.itch.io/warrior-free-animation-set<br>
Minion Art:
https://pixelfrog-assets.itch.io/pixel-adventure-2<br>
Skeleton Art:
https://jesse-m.itch.io/skeleton-pack<br>
Fly Guy Art:
https://luizmelo.itch.io/monsters-creatures-fantasy<br>
UI Art:
https://o-lobster.itch.io/platformmetroidvania-pixel-art-asset-pack<br>
Music:
https://svl.itch.io/rpg-music-pack-svl
