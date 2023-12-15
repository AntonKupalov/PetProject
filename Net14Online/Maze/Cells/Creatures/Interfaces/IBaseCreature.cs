﻿using Maze.Cells.CellInterfaces;

namespace Maze.Cells.Creatures.Interfaces
{
    public interface IBaseCreature : IBaseCell
    {
        int Age { get; set; }
        int Hp { get; set; }
        int Money { get; set; }

        IBaseCell ChooseCellToStep();
    }
}