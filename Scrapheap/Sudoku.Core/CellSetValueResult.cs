using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Core
{
    [Flags]
    public enum CellSetValueResult
    {
        OutOfRange = -1,
        Success = 0x000,
        AlreadyExistsInContainingRow = 0x001,
        AlreadyExistsInContainingColumn = 0x010,
        AlreadyExistsInContainingSquare = 0x100
    }
}
