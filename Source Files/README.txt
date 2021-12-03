--------- Github Repo ---------
https://github.com/BrandonMCoffey/State-Machine-Project


------ Game Description -------
For my project, I decided to recreate a popular board game by Jacob Fryxelius, called Terraforming Mars.

Terraforming Mars is complex in nature and features two (or more) players who control a corporation or company that is
working to Terraform Mars to allow humanity to live on the surface. To do this, Mars needs to develop an atmosphere with
at least 14% Oxygen, a temperature of 8* celcius (from its starting -30*), and 12 tiles worth of oceans. To do this,
players take turns performing standard projects such as building oceans or forests or  enacting patents, which can
sabotage the enemy, place tiles for cheaper, or increase player production of resources.

The game conists of a central hexagonal grid, utop an image of Mars. Tiles can be placed on this grid.
The state of Mars (Oxygen, Water, and Temperature) are displayed on the right side, as vials, which fill up as you play.
Projects and patents are shown on the left side, under different tabs. Clicking on them will open the project and describe
	what it does and allows you to enact said project or patent.
Resources are shown beneath the projects and patents. Two numbers are listed per resource: the amount and production level.

After both players have taken a turn, the current generation ends. Players enter the production phase, where resources are
earned depending on their production level. Player Honor provides additional currency, and energy is converted into heat.

As players take turns, each time they increase the oxygen level, temperature, or water level of the planet they gain Honor.
Honor determines who wins the game, and additional honor can be earned from awards (when the game ends) or milestones reached.


---------- Controls -----------
LEFT CLICK - Interact with UI and Grid

ENTER - Ends turn (Also a button to end turn)
ESCAPE - Pause Game


----------- States ------------
MAIN MENU - Choose to start the game or exit.
MODE SELECTION - Choose to play against an AI or a friend (hotseat - Switch who gets the keyboard)
SETUP - Starts Game. Sets up planet grid and the players / AI.

INTRODUCTION - A set of UI popups that explain how the game is played. They reveal relevant parts of the game as you read.

PLAYER TURN - Your turn. You can perform Projects or Patents. You could also fund an award or claim a milestone.
			Typically followed up by placing a tile or adding to planet status (shown on right)

OPPONENT TURN - Opponent turn. Either AI controlled or (if hotseat) another user turn.
			Three different AI difficulty chosen on the main menu. Performs like a player.

PRODUCTION PHASE - Each player gains their corresponding resources. Visuals display how much of each.
RESEARCH PHASE - Allows user controlled players to purchase patents for $4 credits each.

GAME OVER - Static scene that tells you who won the game. Button on screen to return to Main Menu.


--------- Innovation ----------
Hotseat Gameplay
	- In addition to being able to play against an AI, I allowed players to choose Hotseat mode, which allows two people
	at the same computer to play against each other. Given this generic player system, it is also possible to set AI vs AI.
AI Difficulties
	- There are three included AI difficulties: Easy, Medium, and Hard. The easy AI will occasionaly not do anything and
	does not use its resources well. In contrast, the Hard AI has more of a strategy and will work against you in the best
	way I could implement. The medium difficulty AI is a middleground between the two.
Resizeable UI
	- Because this game is primarily focused on UI, everything is resizable and will work for any monitor aspect ratio
	from square to widescreen. It was interesting learning how to effectively leverage Unity's UI and use Layout Elements,
	Size Fitters, and other components I had never used before this project.
Modeless Windows
	- In the game, you can open Awards and Milestone menus, which can be dragged around the screen and resized between
	defined min and max sizes. I made this into a simple tool that I can add to any UI window and it will work automatically.


---- Project Difficulties -----
Terraforming Mars proved to be a massive undertaking, and I ended up not completing the game by the deadline.

I would like to mention a few of the reasons why I faced such difficulty:
 - Restarting. Early on (Checkpoint A) I had an entirely different game. It was a isometric 3D tactics grid-based game.
	This game was reminiscent of Into the Breach, and as I progressed through it (after the checkpoint), I became
	disinterested in the project and I did not have a solid enough foundation for the game.
 - So, on November 18, I was insprired and fell in love with the idea of the board game, Terraforming Mars. I sketched up
	UI and ideas and it was incredibly in depth and interesting, in contrast to the original idea I had. For the next
	3 or 4 days until I left for Thanksgiving break I worked on ideation and bringing a basic setup into Unity. Then,
	after break I worked really hard bringing together all of the game, and after approximately 10 days of work I now
	have a finished game that I am proud of. Terraforming Mars.
 - The complexity of building this game was absurd for a 10 day project. I ended with 12,000+ lines of code and a good
	number of editor tools used to facilitate parts of making this game.


------------ Notes-------------
Terraforming Mars is definitely one of my most proud accomplishments. Before turning this in I spent a few hours playtesting
and really enjoyed the game. My girlfriend and some other friends also enjoyed playing, and I think the game is successful.
There are still some other things that I will continue to work on for Terraforming Mars, but I really like what I came out with.

I know that I am turning this in as a late project, and I'm okay with that. What i had two nights ago before midnight was
incomplete, buggy, and not feature complete. The AI barely worked at that point because I was distracted by making the gameplay.

I would love feedback on this project and to see how others percieve the game. I think I'll continue making it look professional
and expand upon the features and AI and art to make the game look and feel great to play.

~ Brandon Coffey