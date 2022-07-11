using System;

namespace AlgorithmTest11
{
    class Program
    {
        //이진 변환 반복하기
        //https://school.programmers.co.kr/learn/courses/30/lessons/70129
        //테스트 케이스는 정상 작동 하나 제출 시 내부적인 오류가 발생했다면서 안됨
        static void Main(string[] args)
        {
            Solution s = new Solution();

            foreach (int i in s.solution("110010101001"))
            {
                Console.WriteLine(i);
            }
        }
    }

    public class Solution
    {
        public int[] solution(string s)
        {
            int[] answer = new int[] {0,0 };
            int cnt = 0;

            while (s.Length > 1)
            {
                cnt = 0;

                for (int i = 0; i < s.Length; i++)
                {
                    if(s[i].ToString().Equals("0"))
                    {
                        cnt++;
                    }
                }

                s = s.Replace("0", ""); 
                s = Convert.ToString(s.Length, 2);

                answer[0]++;
                answer[1] += cnt;
            }
            return answer;
        }
    }
}
