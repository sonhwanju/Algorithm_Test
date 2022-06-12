using System;
using System.Collections.Generic;

namespace AlgorithmTest5
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution s = new Solution();

            Console.WriteLine(s.solution("[](){}"));
        }
    }
    //괄호 회전하기 
    //https://programmers.co.kr/learn/courses/30/lessons/76502
    public class Solution
    {
        public int solution(string s)
        {
            int answer = 0;

            for (int i = 0; i < s.Length; i++)
            {
                answer = MatchBracket(s) ? answer + 1 : answer;
                s = RotateBracket(s);
            }

            return answer;
        }

        public string RotateBracket(string s)
        {
            return s[1..] + s.Substring(0, 1);
        }

        public bool MatchBracket(string s)
        {
            if (s.Length % 2 != 0) return false;

            Stack<char> stack = new Stack<char>();
            int cnt = 0;
            for (int i = 0; i < s.Length; i++)
            {
                switch (s[i])
                {
                    case '{':
                    case '[':
                    case '(':
                        stack.Push(s[i]);
                        break;
                    case '}':
                    case ']':
                    case ')':
                        if (stack.Count <= 0) continue;

                        char lastBracket = stack.Peek();
                        if ((lastBracket.Equals('{') && s[i].Equals('}')) || (lastBracket.Equals('[') && s[i].Equals(']')) || lastBracket.Equals('(') && s[i].Equals(')')) 
                        {
                            stack.Pop();
                            cnt++;
                        }
                        break;
                }
            }

            return cnt == s.Length / 2;
        }
    }
}
