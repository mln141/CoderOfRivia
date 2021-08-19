using System;
using System.Collections.Generic;
using System.Linq;
//    https://app.codility.com/programmers/challenges/https://app.codility.com/programmers/challenges/https://app.codility.com/programmers/challenges/https://app.codility.com/programmers/challenges/https://app.codility.com/programmers/challenges/https://app.codility.com/programmers/challenges/https://app.codility.com/programmers/challenges/https://app.codility.com/programmers/challenges/https://app.codility.com/programmers/challenges/https://app.codility.com/programmers/challenges/https://app.codility.com/programmers/challenges/https://app.codility.com/programmers/challenges/https://app.codility.com/programmers/challenges/https://app.codility.com/programmers/challenges/https://app.codility.com/programmers/challenges/https://app.codility.com/programmers/challenges///

namespace CoderOfRivia
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var s = new Solution();
            var r = s.solution(new []{
                int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue,
                int.MaxValue-2, 0,0,0,
                0,0,0,0,
                int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue });
           
            var e = new[]
            {
                int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue,
                int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue,
                int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue,
                int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue,
            };

            var b = r.SequenceEqual(e);
        }
    }

    internal class Solution
    {
        public int[] solution(int[] A)
        {
            var dim = (int) Math.Sqrt(A?.Length ?? 0);
            long[] rs;
            long[] cs;
            SumCol(A, out cs, out rs);
            if (cs.Length == 0 || rs.Length == 0) return A;
            var cm = cs.Max();
            var rm = rs.Max();
            cm = cm < rm ? rm : cm;
            var rows = new Dictionary<int, long>();
            var cols = new Dictionary<int, long>();
            for (var i = 0; i < cs.Length; i++)
                if (cm > cs[i])
                    cols.Add(i,  (cm - cs[i]));
            for (var i = 0; i < rs.Length; i++)
                if (cm > rs[i])
                    rows.Add(i,  (cm - rs[i]));

            while (rows.Count > 0)
            {
                var r = rows.FirstOrDefault().Key;
                var c = cols
                    .FirstOrDefault(x=> A[r * dim + x.Key]<int.MaxValue)
                    .Key;
                if (!rows.ContainsKey(r) || !cols.ContainsKey(c)) break;
                var deltaL =  Math.Min(rows[r], cols[c]);
                var delta =(int) Math.Min(int.MaxValue - A[r * dim + c],deltaL);
                A[r * dim + c]+=delta;
                if (rows[r] > delta) rows[r] = rows[r] - delta;
                else rows.Remove(r);
                if (cols[c] > delta) cols[c] = cols[c] - delta;
                else cols.Remove(c);
            }

            return A;
        }

        private void  SumCol(int[] m, out long[] col, out long[] row)
        {
            var d = (int) Math.Sqrt(m?.Length ?? 0);
            col = new long[d];
            row = new long[d];
            for (var c = 0; c < d; c++)
            for (var r = 0; r < d; r++)
            {
                col[c] += m[r * d + c];
                row[r] += m[r * d + c];
            }
        }
    }
}