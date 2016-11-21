# wumpus
My C# implementation of the Classic text game "Hunt The Wumpus" created by Gregory Yob

# How the Game Works

His game featured a cave with twenty rooms with connections between the rooms. Each room had three tunnels to other rooms. The goal was to use a “crooked arrow” to shoot the Wumpus, a creature which was in one of the rooms. The Wumpus remained in the same room unless you moved into his room or fired an arrow. If you did, there was a 75% chance that he would move to another room. If that happened to be your room, the game was over. To complicate matters, two rooms had deadly bottomless pits and two rooms had Superbats which would move you to a random room. (Yob described Superbats as a “sort of rapid transit system gone a little batty.”) The Wumpus was unaffected by both pits and Superbats.

# Solution

This solution will implement a custom graph library and implementation to represent the "caves" of the game.
The cave will be represented as a dodecahedron; where each vertex is a cave room and each edge is a hallway connecting said rooms.

