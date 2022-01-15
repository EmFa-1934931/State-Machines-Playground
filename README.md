# State Machines Playground
 Project from the video game course playing with state machines. 
 Project finished on november 14th 2021
 
 ##Project presentation
 This project is a "game" that plays itself. Two teams battle eachother and try to take the opposing team's towers, until they've all fallen. The battle is set in a Legend of Zelda-style dungeon, and the ice and fire wizards fight eachother. The game features 6 states: normal, brave, flee, hide, safe and inactive.
 
 ###Normal state
 In this state, the wizard's main goal is to get in the range of a random opposing tower and shoot it with arrows. If an opposing wizard gets in its range, the wizard will fight it until it either dies or flees. When the wizard is not in combat, its health regenerates itself.
 
 ###Brave state
 If the wizard eliminated three ennemies, it becomes brave. It always moves towards the towers and doesn't engage by itself in combat unless it is attacked, the wizard attacking it then becomes instantly its only target. It's health also regenerates constantly, even in combat, and its speed increases slightly.
