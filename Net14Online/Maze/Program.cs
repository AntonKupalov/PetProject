﻿using Maze.LevelStaff;

var builder = new LevelBuilder();
var level = builder.BuildV5();

// player push the button

var drawer = new LevelDrawer();
drawer.Draw(level);
Console.ReadLine();
