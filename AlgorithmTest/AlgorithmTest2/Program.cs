using System;
using System.Linq;
using System.Collections.Generic;

namespace AlgorithmTest2
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution s = new Solution();

            Console.WriteLine(s.solution(10,10,new int[] { 100 }, new int[] { 100 }, new int[] { 7 }, new int[] { 10 }));
            //Console.WriteLine(s.solution(90, 500, new int[] { 70, 70, 0 }, new int[] { 0, 0, 500 }, new int[] { 100, 100, 2 }, new int[] { 4, 8, 1 }));
        }
    }

    public class City
    {
        public int gold;
        public int silver;
        public int moveTime;
        public int capacity;

        public float ef;
        public int timeCount;

        public City()
        {

        }

        public City(int gold, int silver, int moveTime, int capacity)
        {
            this.gold = gold;
            this.silver = silver;
            this.moveTime = moveTime;
            this.capacity = capacity;

            this.ef = capacity / moveTime;
            this.timeCount = 0;
        }
    }

    public class Solution
    {
        public bool IsEnd(int a, int b, int curA,int curB)
        {
            return !(a > curA || b > curB);
        }

        public bool IsSkipCity(City city,int a, int b, int curA, int curB)
        {
            return (curA >= a && city.silver <= 0) || (curB >= b && city.gold <= 0);
        }

        public long solution(int a, int b, int[] g, int[] s, int[] w, int[] t)
        {
            double db = Math.Pow(10, 9);
            if (a < 0 || b < 0 || a > db || b > db) return -1;
            if(!((g.Length == s.Length) && (s.Length == w.Length) && (w.Length == t.Length)) || g.Length < 1 || g.Length > Math.Pow(10, 5)) return -1;

            int goldCount = 0,silverCount = 0;

            for (int i = 0; i < g.Length; i++)
            {
                if (g[i] < 0 || s[i] < 0 || w[i] < 1 || t[i] < 1 || g[i] > db || s[i] > db || w[i] > Math.Pow(10, 2) || t[i] > Math.Pow(10, 5)) return -1;

                goldCount += g[i];
                silverCount += s[i];
            }

            if (a > goldCount || b > silverCount) return -1;

            List<City> cityList = new List<City>();
            int curA = 0, curB = 0;
            int idx = 0;

            for (int i = 0; i < g.Length; i++)
            {
                cityList.Add(new City(g[i], s[i], t[i], w[i]));
            }

            cityList.Sort((x, y) => y.ef.CompareTo(x.ef));

            do
            {
                for (int i = 0; i < cityList.Count; i++)
                {
                    City city = cityList[i];

                    if (IsSkipCity(city, a, b, curA, curB)) continue;

                    int capacityCnt = 0;
                    city.timeCount += idx > 0 ? (city.moveTime * 2) : city.moveTime;

                    if (curA < a) //골드가 아직 목표량에 도달하지 않았다면
                    {
                        int remain = a - curA;
                        if (remain < city.capacity) // 총 목표 용량이 적재용량보다 적을때 ( 물량만 있으면 한번에 다 옮기기 가능)
                        {
                            int temp = city.gold >= remain ? remain : city.gold; //골드가 남은 적재용
                            curA += temp;
                            city.gold -= temp;
                            capacityCnt += temp;
                        }
                        else // 총 목표 용량이 적재용량보다 클때
                        {
                            int temp = city.gold >= city.capacity ? city.capacity : city.gold;
                            curA += temp;
                            city.gold -= temp;
                            capacityCnt += temp;
                        }
                    }

                    if (curB < b)
                    {
                        int remainCapacity = city.capacity - capacityCnt;

                        if (remainCapacity <= 0) continue;

                        int remain = b - curB;

                        if (remain < remainCapacity) //남아있는 적재용량이 총 목표 용량보다 클때
                        {
                            int temp2 = city.silver >= remain ? remain : city.silver;
                            curB += temp2;
                            city.silver -= temp2;
                            capacityCnt += temp2;
                        }
                        else
                        {
                            int tmp = city.silver >= remainCapacity ? remainCapacity : city.silver;
                            curB += tmp;
                            city.silver -= tmp;
                            capacityCnt += tmp;
                        }
                    }

                }
                idx++;
                Console.WriteLine($"{a}, {b}, {curA}, {curB}");
                if (idx >= 10000) return -1;
            } while (!IsEnd(a,b,curA,curB));
            

            return cityList.Max(x => x.timeCount);
        }
    }
}
