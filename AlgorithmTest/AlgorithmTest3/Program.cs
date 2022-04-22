using System;
using System.Linq;
using System.Collections.Generic;

namespace AlgorithmTest3
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution s = new Solution();
            Console.WriteLine(s.solution("hit", "cog", new string[] { "hot", "dot", "dog", "lot", "log", "cog" }));
        }
    }
    //https://programmers.co.kr/learn/courses/30/lessons/43163
    //단어 변환
    public class Solution
    {
        public int solution(string begin, string target, string[] words)
        {
            int answer = words.Length;
            int idx = -1;
            bool[] visited = new bool[words.Length];

            if (words.Length < 3 || words.Length > 50 || begin.Equals(target) || !words.Contains(target) || visited.Length != words.Distinct().ToArray().Length) return 0;

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length < 3 || words[i].Length > 10 || (idx != -1 && idx != words[i].Length)) return 0;
                idx = words[i].Length;
            }

            Dfs(begin, target, words, 0, visited, ref answer);

            return answer;
        }

        public void Dfs(string begin, string target, string[] words,int count,bool[] visited, ref int answer)
        {
            if (count >= words.Length) return;

            if(begin == target)
            {
                answer = Math.Min(answer, count);
                return;
            }

            for (int i = 0; i < words.Length; i++)
            {
                int cnt = GetCount(begin, words[i]);
                if(!visited[i] && cnt == 1)
                {
                    visited[i] = true;
                    Dfs(words[i], target, words, count + 1, visited,ref answer);
                    visited[i] = false;
                }
            }
            return;
        }

        public int GetCount(string s1, string s2)
        {
            int cnt = 0;

            for (int i = 0; i < s1.Length; i++)
            {
                if(!s1[i].Equals(s2[i]))
                {
                    cnt++;
                }
            }


            return cnt;
        }
    }
}
