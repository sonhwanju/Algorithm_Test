using System;

namespace AlgorithmTest10
{
    class Program
    {
        //예상 대진표
        //https://school.programmers.co.kr/learn/courses/30/lessons/12985
        static void Main(string[] args)
        {
            Solution s = new Solution();
            Console.WriteLine(s.solution(8, 1, 2));
        }
    }

    class Solution
    {
        public int solution(int n, int a, int b)
        {
            int answer = 1;

            if(a > b)
            {
                int temp = a;
                a = b;
                b = temp;
            }

            while (true)
            {
                //1일경우 0이 되므로 + 1을 해줌
                if ((a + 1) / 2 == (b + 1) / 2)
                {
                    break;
                }
                a = a % 2 == 0 ? a / 2 : a / 2 + 1;
                b = b % 2 == 0 ? b / 2 : b / 2 + 1;
                
                answer++;
            }

            return answer;
        }
    }
}
