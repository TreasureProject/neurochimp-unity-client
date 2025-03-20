INTRO:
	Hey there first of all thank you for downloading this asset!
	In this readme file I will quickly summarize what is included in this asset and where you can find relevant scripts.


INCLUDED:
	A* methods (Astar/Needed Scripts/A Star Pathfinding):
		Here you can find the functions you should be interacting with, and some comments describing them. I recomend you take a look!
		*	async (int, int)[] GeneratePath(int startX, int startY, int goalX, int goalY, bool[,] walkableMap, bool manhattanHeuristic = true)
		*	(int, int)[] GeneratePathSync(int startX, int startY, int goalX, int goalY, bool[,] walkableMap, bool manhattanHeuristic = true)

		*	async (int, int)[] GeneratePath(int startX, int startY, int goalX, int goalY, float[,] costMap, bool manhattanHeuristic = true)
		*	(int, int)[] GeneratePathSync(int startX, int startY, int goalX, int goalY, float[,] costMap, bool manhattanHeuristic = true)

	Documentation (AStar/Needed Scripts/AStarPathfinding Documentation.pdf)
		A documentation in the for of a PDF explaining how to use the included A* methods
		
	Examples:
		Random pathfinding ants (Astar/Example 1):
			In this example, many pathfinders are spawned on a map and constantly pathfind to random locations.

		Click to Draw Path (Astar/Example 2):
			In this example, you click on point A and the point B, if a path exists betwen those points it will be drawn.

		A minimal example of the A* async functions (Astar/Example 3):
			This example contains minimal examples for the async GeneratePath method, with both a cost map and a walkable map 

	What can I use?:
		Everything is included for you to use! 
		The A* code is the main part of this asset, but if you find something you would like to use in the example scenes, you are free to do so as well.


A MINIMAL EXAMPLE:
	using AStar;

	private async void myMethod(){
		bool[,] walkableMap = {
		{true, false, true, true},
		{true, false, true, true},
		{true, false, true, true},
		{true, true, true, true}
		};

		(int, int)[] path = await AStarPathfinding.GeneratePath(0, 0, 3, 0, walkableMap);
	}

	The path should after this method is done cointain the following:
	path == [(0, 0), (0, 1), (0, 2), (1, 3), (2, 2), (2, 1), (2, 0)]

	The path is a list of cordinates (x, y) that will take you from (startX, startY) to (goalX, goalY)

DEVELOPERS NOTE:
	If you find this AStarPathfinding asset useful in your Unity projects, 
	I would greatly appreciate it if you could take a moment to leave a review.

	https://assetstore.unity.com/packages/tools/behavior-ai/astar-2d-grid-pathfinding-250080#reviews

	Your feedback is invaluable to me and allows me to make more informed decisions about how to improve this asset.
