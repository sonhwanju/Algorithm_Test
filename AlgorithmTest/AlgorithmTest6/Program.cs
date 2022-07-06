using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmTest6
{


    //숫자 게임
    //https://school.programmers.co.kr/learn/courses/30/lessons/12987
    class Program
    {
        static void Main(string[] args)
        {
            Solution s = new Solution();

            Console.WriteLine(s.solution(new int[] { 5, 1, 3, 7 }, new int[] { 2, 2, 6, 8 }));
        }
    }

    public class Solution
    {
        public int solution(int[] A, int[] B)
        {
            if (!A.Length.Equals(B.Length) || A.Length <= 0 || A.Length > 100000) return -1;

            int answer = 0;

            Array.Sort(A);
            Array.Sort(B);

            LinkedList<int> aList = new LinkedList<int>(A);
            LinkedList<int> bList = new LinkedList<int>(B);

            while (aList.Count >0  && bList.Count > 0)
            {
                int a = aList.First.Value;
                int b = bList.First.Value;

                aList.RemoveFirst();
                bList.RemoveFirst();

                while (b <= a)
                {
                    if (bList.Count <= 0) return answer;
                    b = bList.First.Value;
                    bList.RemoveFirst();
                }

                answer++;
            }

            //for (int i = 0; i < aList.Count; i++)
            //{
            //    int g = GetBNum(aList[i], bList);

            //    if (g <= -1) return answer;

            //    bList.Remove(g);
            //    answer++;
            //}

            return answer;
        }

        public int GetBNum(int aNum,List<int> B)
        {
            int s = int.MaxValue;

            for (int i = 0; i < B.Count; i++)
            {
                int c = B[i];

                if (c > aNum && s > c)
                {
                    s = c;
                }
            }
            if (s.Equals(int.MaxValue)) return -1;

            return s;
        }
    }
}
