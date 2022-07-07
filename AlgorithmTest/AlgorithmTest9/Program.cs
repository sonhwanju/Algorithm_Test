using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmTest9
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution s = new Solution();

            Console.WriteLine(s.solution(new int[] { 2,1,3,2},2));
        }
    }

    public class Solution
    {
        public int solution(int[] priorities, int location)
        {
            List<int> list = new List<int>(priorities);
            Queue<Priority> queue = new Queue<Priority>();
            int answer = 0;

            for (int i = 0; i < priorities.Length; i++)
            {
                queue.Enqueue(new Priority(priorities[i], i));
            }

            while (queue.Count > 0)
            {
                int max = list.Max();
                Priority cur = queue.Dequeue();

                while (!cur.index.Equals(max))
                {
                    queue.Enqueue(cur);

                    cur = queue.Dequeue();
                }

                list.Remove(max);
                answer++;

                if (cur.location.Equals(location)) break;
            }

            return answer;
        }

        class Priority
        {
            public int index;
            public int location;

            public Priority(int index, int location)
            {
                this.index = index;
                this.location = location;
            }
        }
    }
}
