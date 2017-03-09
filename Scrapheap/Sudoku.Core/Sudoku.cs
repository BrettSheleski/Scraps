using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Sudoku.Core
{
    public class Sudoku
    {
        private IEnumerable<int?> enumerable;

        public Sudoku(IList<IList<int?>> initialValues) : this()
        {
            if (initialValues.Count != Width)
                throw new ArgumentException($"{nameof(initialValues)} does not have {Width} values", nameof(initialValues));

            if (initialValues.Any(r => r.Count != Width))
                throw new ArgumentException($"all values within {nameof(initialValues)} must have {Width} values", nameof(initialValues));


            Initialize(initialValues.SelectMany(x => x));
        }

        void Initialize(IEnumerable<int?> initialValues)
        {
            int i = 0;
            foreach (var value in initialValues)
            {
                Cells[i].Value = value;
                ++i;
            }
        }

        protected Sudoku(IEnumerable<int?> initialValues)
        {
            Initialize(initialValues);
        }

        public Sudoku() : this(3) { }

        protected Sudoku(int baseSize)
        {
            this.BaseSize = baseSize;
            this.Width = baseSize * baseSize;

            this.Size = Width * Width;

            Rows = new ReadOnlyCollection<Set>(Enumerable.Range(0, Width).Select(i => new Set(Width)).ToArray());
            Columns = new ReadOnlyCollection<Set>(Enumerable.Range(0, Width).Select(i => new Set(Width)).ToArray());
            Squares = new ReadOnlyCollection<Set>(Enumerable.Range(0, Width).Select(i => new Set(Width)).ToArray());

            List<Cell> cells = new List<Cell>();

            Set row, column, square;
            Cell cell;
            for (int i = 0; i < Size; ++i)
            {
                row = GetRowByIndex(i);
                column = GetColumnByIndex(i);
                square = GetSquareByIndex(i);

                cell = new Cell(row, column, square, Width);
                row.AddCell(cell);
                column.AddCell(cell);
                square.AddCell(cell);
                cells.Add(cell);
            }

            this.Cells = new ReadOnlyCollection<Cell>(cells);
        }

       

        Sudoku Clone()
        {
            var clone = new Sudoku(this.Cells.Select(cell => cell.Value));


            return clone;
        }

        public Sudoku GetSolvedSudoku()
        {
            HashSet<int> possibleValuesRemaining;

            Sudoku solved = null;
            Cell cell;
            for(int i = 0; i < Cells.Count; ++i)
            {
                solved = this.Clone();

                cell = solved.Cells[i];
                possibleValuesRemaining = new HashSet<int>(Enumerable.Range(1, Width).Where(x => !cell.Row.Values.Contains(x) && !cell.Column.Values.Contains(x) && !cell.Square.Values.Contains(x)));
                


            }
            


            return solved;
        }

        /*
         
                    0          1         2 

                0  1   2 |  3  4  5 |  6  7  8
            0   9  10 11 | 12 13 14 | 15 16 17
                18 19 20 | 21 22 23 | 24 25 26
                ------------------------------
                27 28 29 | 30 31 32 | 33 34 35
            1   36 37 38 | 39 40 41 | 42 43 44
                45 46 47 | 48 49 50 | 51 52 53
                ------------------------------
                54 55 56 | 57 58 59 | 60 61 62
            2   63 64 65 | 66 67 68 | 69 70 71
                72 73 74 | 75 76 77 | 78 79 80
            
        */

        public Set GetRowByIndex(int index)
        {
            return Rows[index / Width];
        }

        public Set GetColumnByIndex(int index)
        {
            return Columns[index % Width];
        }

        public Set GetSquareByIndex(int index)
        {
            int rowIndex = index / (BaseSize * Width),
                columnIndex = index / BaseSize % BaseSize,
                squareIndex = columnIndex + (BaseSize * rowIndex);

            return Squares[squareIndex];
        }

        public int Width { get; }
        public int Size { get; }
        public int BaseSize { get; }

        public ReadOnlyCollection<Set> Rows { get; }

        public ReadOnlyCollection<Set> Columns { get; }

        public ReadOnlyCollection<Set> Squares { get; }

        public ReadOnlyCollection<Cell> Cells { get; }

    }
}