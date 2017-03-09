using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sudoku.Core.Tests
{
    [TestClass]
    public class SudokuTests
    {
        [TestMethod]
        public void CanCreateNewEmptySudoku()
        {
            // Setup

            // Act
            var sudoku = new Sudoku();

            // Verify
        }

        [TestMethod]
        public void CanCreateInitialSudoku()
        {
            // Setup

            // from: http://www.websudoku.com/?level=1&set_id=274943708

            int?[][] values = new int?[][]
            {
                new int?[] {2, null, null, 7, null, 6, null, null, null },
                new int?[] {null, 6,3, 9, 5, 4, null, null, null },
                new int?[] {null, null, 9, null, 1, null, null, null, null },

                new int?[] {null, 7, 8, null, null, null, null, null, 4 },
                new int?[] {6, null, 2, 4, 7, 3, 9, null, 5 },
                new int?[] {3, null, null, null, null, null, 1, 2, null },

                new int?[] {null, null, null, null, 4, null, 6, null, null },
                new int?[] {null, null, null, 5, 6, 7, 8, 3, null },
                new int?[] {null, null, null, 8, null, 1, null, null, 9 },
            };

            // Act
            var sudoku = new Sudoku(values);

            // Verify
        }

        [TestMethod]
        public void CanSolve()
        {
            // Setup

            // from: http://www.websudoku.com/?level=1&set_id=274943708

            int?[][] values = new int?[][]
            {
                new int?[] {2, null, null, 7, null, 6, null, null, null },
                new int?[] {null, 6,3, 9, 5, 4, null, null, null },
                new int?[] {null, null, 9, null, 1, null, null, null, null },

                new int?[] {null, 7, 8, null, null, null, null, null, 4 },
                new int?[] {6, null, 2, 4, 7, 3, 9, null, 5 },
                new int?[] {3, null, null, null, null, null, 1, 2, null },

                new int?[] {null, null, null, null, 4, null, 6, null, null },
                new int?[] {null, null, null, 5, 6, 7, 8, 3, null },
                new int?[] {null, null, null, 8, null, 1, null, null, 9 },
            };
            var sudoku = new Sudoku(values);

            // Act
            sudoku.GetSolvedSudoku();


            // Verify
        }
    }
}
