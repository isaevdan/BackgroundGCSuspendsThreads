using System.Collections.Generic;

namespace BackgroundGCSuspendsThreads
{
    public class Cell
    {
        public decimal Value { get; }

        public Cell(decimal value)
        {
            Value = value;
        }
    }

    public class Column
    {
        private readonly Cell[] _vocabulary;
        private readonly int[] _vector;

        public Column(Cell[] vocabulary, int[] vector)
        {
            _vocabulary = vocabulary;
            _vector = vector;
        }
    }
    
    public class ColumnBuilder
    {
        readonly Dictionary<decimal, int> _dict = new Dictionary<decimal, int>();
        private int _currentIndex = 0;
        private readonly List<int> _vector = new List<int>();

        public void Add(decimal value)
        {
            if (_dict.TryGetValue(value, out var index))
                _vector.Add(index);
            else
            {
                _dict.Add(value, _currentIndex);
                _vector.Add(_currentIndex);
                _currentIndex++;
            }
        }

        public Column ToColumn()
        {
            var vocabulary = new Cell[_dict.Count];
            foreach (var cellPair in _dict)
            {
                vocabulary[cellPair.Value] = new Cell(cellPair.Key);
            }

            return new Column(vocabulary, _vector.ToArray());
        }
    }
}