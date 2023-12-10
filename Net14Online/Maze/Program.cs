﻿using Maze.ConsolePlay;
using Maze.LevelStaff;

var builder = new LevelBuilder();
var level = builder.BuildV0(16, 16);

// player push the button
var consoleController = new ConsoleController();
consoleController.Play();

var drawer = new LevelDrawer();
drawer.Draw(level);