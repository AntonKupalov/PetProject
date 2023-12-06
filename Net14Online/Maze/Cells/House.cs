﻿using Maze.LevelStaff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Cells
{
    public class House : BaseCell
    {
        public House(int coordinateX, int coordinateY, Level level) : base(coordinateX, coordinateY, level)
        {
        }

        public override string Symbol => "^";

        public static void ChangeGroundToHouse(List<Ground> grounds, Level level)
        {
            for (int i = 0; i < grounds.Count; i = i + 4)
            {
                var randomGround = level.Cells.First(x => x.CoordinateX == grounds[i].CoordinateX && x.CoordinateY == grounds[i].CoordinateY);
                House house = new House(grounds[i].CoordinateX, grounds[i].CoordinateY, level);

                level.Cells.Remove(randomGround);
                level.Cells.Add(house);
            }
        }
    }
}
