# GMTKJam2021
Game for the GMTK Game Jam 2021!

Link to game: https://ripper53.itch.io/dashing-ball

## Credits
Albar (Ripper): Design and Programming

Armando Font (ProfessorIrony): Design, Music [https://soundcloud.com/houseofvixens]

Lyckstolp: SFX [lyckstolp@gmail.com] [Discord: Lyckstolp#3858]

Aman Singla (XENO Ghoul Pro): Art

## Process
The process of programming the game will only be described, because I (Albar) was the programmer for the game. I spent most of my time perfecting the interaction between the player and ball which they can dash to after they have thrown it. Since the player will be throwing the ball and dashing to it as a primary means to move around (which they will be doing a lot of), I thought it would be best to knock it out first, and base the game around this mechanic. The musician pitched in with their music, sound designer pitched in their sound design, and the artist pitched in with their art. I had the musician work on level design with their spare time which they got out a few levels with (in fact, all the levels from the game are theirs). I had a lot of programming to do in 48 hours, and because of that I knew I would barely have time to design the levels and I would have to make them as well.

The throwing was straight forward. Wherever the player aims, throw the ball there. Have a bar that indicates their charge level, and once they release their mouse button, throw the ball depending on how much they have charged. Once they throw the ball, they can dash to it. It will dash in a straight line, through walls, to the ball. The ball had six raycasts, one for each direction in three dimensions to figure out if there was a wall near it, so I could offset the player’s position by that distance to prevent it from clipping into the wall. I added a visual indicator, a line stretching from the ball to the ground, which helped with knowing on which platform the ball was over. The musician also suggested a great mechanic, where the ball may start charging while the player is still in their dash. Allowing the ball to be charged prematurely, then thrown as soon as the player reached the ball, kept the pace of the game fast. I checked if any enemies were in the trajectory of the dash to the ball, and if they were, they would be killed. To really sell the impact, I slowed down the time when a hit would occur, played a visual indicator around the screen, and played a sound cue depending on which enemy was killed.

## What I Learned
The player controls were fun! The feedback we got said playing around with dashing to the ball was fun of its own accord. Meaning, our hook was successful. However, we did not have the time to polish the game. And since our estimations of the duration it will take to finish the game (before the polishing phase), the interactions with the level were not the best, the enemy A.I. was not the smartest, the enemies sometimes were hard to see, they would sometimes spawn too close to the player, and we lacked a pause system.
