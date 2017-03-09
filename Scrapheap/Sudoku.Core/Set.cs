using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Core
{
    public class Set
    {
        public int Size { get; }

        public Cell[] Cells { get; }
        public HashSet<int> Values { get; }

        public Set(int size)
        {
            this.Size = size;
            Values = new HashSet<int>();
            Cells = new Core.Cell[Size];
        }

        internal void AddCell(Cell cell)
        {
            for (int i = 0; i < Size; ++i)
            {
                if (Cells[i] == null)
                {
                    Cells[i] = cell;
                    return;
                }
            }

            throw new InvalidOperationException();
        }

        public Cell this[int index]
        {
            get
            {
                return Cells[index];
            }
            internal set
            {
                Cells[index] = value;
            }
        }
    }
}
