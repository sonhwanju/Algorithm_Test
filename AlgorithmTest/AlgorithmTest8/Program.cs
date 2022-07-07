using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmTest8
{
    class Program
    {

        //최댓값과 최솟값
        //https://school.programmers.co.kr/learn/courses/30/lessons/12939
        static void Main(string[] args)
        {
            Solution s = new Solution();

            Console.WriteLine(s.solution("1 2 3 4"));
            //aabbaaaabbaa 240
        }

        public class Solution
        {
            public string solution(string s)
            {
                string[] strs = s.Split(" ");
                List<int> list = new List<int>();

                for (int i = 0; i < strs.Length; i++)
                {
                    if(int.TryParse(strs[i], out int result))
                    {
                        list.Add(result);
                    }
                }

                return $"{list.Min()} {list.Max()}";
            }
        }
    }

}
