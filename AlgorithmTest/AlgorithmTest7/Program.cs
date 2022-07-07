using System;
using System.Collections.Generic;

namespace AlgorithmTest7
{

    //기능개발
    //https://school.programmers.co.kr/learn/courses/30/lessons/42586
    class Program
    {
        static void Main(string[] args)
        {
            Solution s = new Solution();


            foreach (int i in s.solution(new int[] { 93, 30, 55 }, new int[] { 1, 30, 5 }))
            {
                Console.WriteLine(i);
            }
        }
    }


    public class Solution
    {
        public int[] solution(int[] progresses, int[] speeds)
        {
            if (!progresses.Length.Equals(speeds.Length)) return new int[] { -1 };

            List<int> answer = new List<int>();
            List<int> list = new List<int>();

            for (int i = 0; i < progresses.Length; i++)
            {
                int cnt = 0;
                for (int j = progresses[i]; j < 100; j+= speeds[i])
                {
                    cnt++;
                }
                list.Add(cnt);
            }

            int temp = 0;
            int count = 0;

            for (int i = 0; i < list.Count; i++)
            {
                if(temp >= list[i])
                {
                    count++;
                }
                else
                {
                    if(count != 0)
                        answer.Add(count);
                    count = 1;
                    temp = list[i];
                }
            }

            answer.Add(count);

            return answer.ToArray();
        }
    }
}
