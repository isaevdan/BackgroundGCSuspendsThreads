using System;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundGCSuspendsThreads
{
    internal class Program
    {
        private static decimal[] decimals;
        private static decimal[] decimals1;
        private static string[] strings;
        private static bool[] bools;
        private static Cell[] cells;
        private static Cell[] cells2;
        private static Cell[] cells3;
        
        public static void Main(string[] args)
        {
            const int size = 10_000_000;
            //Initialize heap with lots of big objects
            decimals = Enumerable.Range(0, size).Select(Convert.ToDecimal).ToArray();
            decimals1 = Enumerable.Range(0, size).Select(Convert.ToDecimal).ToArray();
            strings = Enumerable.Range(0, size).Select(e => e.ToString()).ToArray();
            strings = Enumerable.Range(0, size).Select(e => e.ToString()).ToArray();
            bools = Enumerable.Range(0, size).Select(e => e % 2 == 0).ToArray();
            cells = decimals.Select(e => new Cell(e)).ToArray();
            cells2 = decimals.Select(e => new Cell(e)).ToArray();
            cells3 = decimals.Select(e => new Cell(e)).ToArray();
            
            GC.Collect();
            
            Task<Column> job1 = Task.Run(() => Job(size));
            Task<Column> job2 = Task.Run(() => Job(size));
            Task.WaitAll(job1, job2);
        }

        static Column Job(int size)
        {
            var builder = new ColumnBuilder();
            for (int i = 0; i < size; i++)
            {
                builder.Add(decimals[i]);
            }

            return builder.ToColumn();
        }
    }
}
