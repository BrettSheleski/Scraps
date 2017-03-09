using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Core
{
    public class Cell
    {
        public Cell(Set row, Set column, Set square, int maxValue)
        {
            this.Row = row;
            this.Column = column;
            this.Square = square;

            this.MaxValue = maxValue;
        }

        public int MaxValue { get; }

        private int? _value;

        public int? Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value == value)
                    return;

                if (value == null)
                {
                    Row.Values.Remove(_value.Value);
                    Column.Values.Remove(_value.Value);
                    Square.Values.Remove(_value.Value);

                    _value = value;
                }
                else
                {
                    var result = TrySetValue(value.Value);

                    if (result == CellSetValueResult.Success)
                        return;
                    else if ((result & CellSetValueResult.OutOfRange) == CellSetValueResult.OutOfRange)
                        throw new ArgumentOutOfRangeException(nameof(value), $"value must be between 1 and ${MaxValue} (inclusive)");

                    else if ((result & CellSetValueResult.AlreadyExistsInContainingRow) == CellSetValueResult.AlreadyExistsInContainingRow)
                        throw new InvalidOperationException("value already exists in the containing row");

                    else if ((result & CellSetValueResult.AlreadyExistsInContainingColumn) == CellSetValueResult.AlreadyExistsInContainingColumn)
                        throw new InvalidOperationException("value already exists in the containing column");

                    else if ((result & CellSetValueResult.AlreadyExistsInContainingSquare) == CellSetValueResult.AlreadyExistsInContainingSquare)
                        throw new InvalidOperationException("value already exists in the containing square");
                    else
                        throw new InvalidOperationException();
                }
            }
        }

        public CellSetValueResult TrySetValue(int value)
        {
            if (value < 1 || value > MaxValue)
                return CellSetValueResult.OutOfRange;

            CellSetValueResult result = CellSetValueResult.Success;

            if (Row.Cells.Any(cell => cell.Value == value))
                result |= CellSetValueResult.AlreadyExistsInContainingRow;

            if (Column.Cells.Any(cell => cell.Value == value))
                result |= CellSetValueResult.AlreadyExistsInContainingColumn;

            if (Square.Cells.Any(cell => cell.Value == value))
                result |= CellSetValueResult.AlreadyExistsInContainingSquare;

            if (result == CellSetValueResult.Success)
            {
                _value = value;

                Row.Values.Add(value);
                Column.Values.Add(value);
                Square.Values.Add(value);
            }

            return result;
        }

        public Set Square { get; }
        public Set Column { get; }
        public Set Row { get; }
    }
}