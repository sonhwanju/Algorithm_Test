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
            Console.WriteLine(s.solution(90, 500, new int[] { 70, 70, 0 }, new int[] { 0, 0, 500 }, new int[] { 100, 100, 2 }, new int[] { 4, 8, 1 }));
        }
    }

    public class Solution
    {
        //금과 은 운반하기
        //https://programmers.co.kr/learn/courses/30/lessons/86053?language=csharp

        //특정 시간 내에 구해낼 수 있는지를 봤어야함. 그래서 이분 탐색을 써서 했어야 하는듯
        //이분 탐색을 알고리즘 문제 풀때 안써봐서 못 생각했다
        //정확성 50이 뜬다.-> while문 조건에서 start < end가 아닌 start <= end로 하니 잘 됨.

        public long solution(int a, int b, int[] g, int[] s, int[] w, int[] t)
        {
            long start = 1;
            long end = (long)(Math.Pow(10, 9) * Math.Pow(10, 5) * 4);
            long answer = end;

            while (start <= end)
            {
                long mid = ((start + end) / 2);
                //Console.WriteLine(mid);
                long total = 0;
                long totalGold = 0;
                long totalSilver = 0;

                for (int i = 0; i < g.Length; i++)
                {
                    long time = t[i]; //편도 시간
                    long roundTime = time * 2; //왕복 시간
                    long moveCount = mid / roundTime; //몇번 옮길 수 있는가

                    if (mid % roundTime >= time) moveCount++; //편도로 한번 갈수 있는가
                    long maxTake = w[i] * moveCount; //최대로 옮길 수 있는 양

                    totalGold += Math.Min(g[i], maxTake); //가지고 있는 골드의 양과 최대로 옮길수 있는 양중에 작은놈을 토탈에 추가
                    totalSilver += Math.Min(s[i], maxTake); //가지고있는 실버의 양과 최대로 옮길 수 있는 양중에 작은놈 추가
                    total += Math.Min(g[i] + s[i], maxTake); //가지고있는 골드 + 실버의 양과 최대로 옮길 수 있는 양중에 작은놈 추가

                }
                if(total >= a+b && totalGold >= a && totalSilver >= b)
                {
                    end = mid - 1;
                    answer = Math.Min(answer, mid);
                }
                else
                {
                    start = mid + 1;
                }
            }
            return answer;
        }



        //public class City
        //{
        //    public int gold;
        //    public int silver;
        //    public int moveTime;
        //    public int capacity;

        //    public float ef;
        //    public int timeCount;

        //    public City()
        //    {

        //    }

        //    public City(int gold, int silver, int moveTime, int capacity)
        //    {
        //        this.gold = gold;
        //        this.silver = silver;
        //        this.moveTime = moveTime;
        //        this.capacity = capacity;

        //        this.ef = capacity / moveTime;
        //        this.timeCount = 0;
        //    }
        //}

        //public bool IsEnd(int a, int b, int curA,int curB)
        //{
        //    return !(a > curA || b > curB);
        //}

        //public bool IsSkipCity(City city,int a, int b, int curA, int curB)
        //{
        //    return (curA >= a && city.silver <= 0) || (curB >= b && city.gold <= 0);
        //}

        //public long solution(int a, int b, int[] g, int[] s, int[] w, int[] t)
        //{
        //    double db = Math.Pow(10, 9);
        //    if (a < 0 || b < 0 || a > db || b > db) return -1;
        //    if(!((g.Length == s.Length) && (s.Length == w.Length) && (w.Length == t.Length)) || g.Length < 1 || g.Length > Math.Pow(10, 5)) return -1;

        //    int goldCount = 0,silverCount = 0;

        //    for (int i = 0; i < g.Length; i++)
        //    {
        //        if (g[i] < 0 || s[i] < 0 || w[i] < 1 || t[i] < 1 || g[i] > db || s[i] > db || w[i] > Math.Pow(10, 2) || t[i] > Math.Pow(10, 5)) return -1;

        //        goldCount += g[i];
        //        silverCount += s[i];
        //    }

        //    if (a > goldCount || b > silverCount) return -1;

        //    List<City> cityList = new List<City>();
        //    int curA = 0, curB = 0;
        //    int idx = 0;

        //    for (int i = 0; i < g.Length; i++)
        //    {
        //        cityList.Add(new City(g[i], s[i], t[i], w[i]));
        //    }

        //    cityList.Sort((x, y) => y.ef.CompareTo(x.ef));

        //    do
        //    {
        //        for (int i = 0; i < cityList.Count; i++)
        //        {
        //            City city = cityList[i];

        //            if (IsSkipCity(city, a, b, curA, curB)) continue;

        //            int capacityCnt = 0;
        //            city.timeCount += idx > 0 ? (city.moveTime * 2) : city.moveTime;

        //            if (curA < a) //금이 아직 목표량에 도달하지 않았다면
        //            {
        //                int remain = a - curA;
        //                int temp = remain <= city.capacity ? (city.gold >= remain ? remain : city.gold) : 
        //                    (city.gold >= city.capacity ? city.capacity : city.gold) -> 이런식으로 햇음 댓을것같음Math.Min(city.gold,city.capacity);

        //                curA += temp;
        //                city.gold -= temp;
        //                capacityCnt += temp;
        //            }

        //            if (curB < b) //은이 아직 목표량에 도달하지 않았다면
        //            {
        //                int remainCapacity = city.capacity - capacityCnt;

        //                if (remainCapacity <= 0) continue;

        //                int remain = b - curB;
        //                int temp = remain <= remainCapacity ? (city.silver >= remain ? remain : city.silver) : 
        //                    (city.silver >= remainCapacity ? remainCapacity : city.silver);

        //                curB += temp;
        //                city.silver -= temp;
        //                capacityCnt += temp;
        //            }
        //        }
        //        idx++;
        //        Console.WriteLine($"{a}, {b}, {curA}, {curB}");
        //        if (idx >= 10000) return -1;
        //    } while (!IsEnd(a,b,curA,curB));


        //    return cityList.Max(x => x.timeCount);
        //}
    }
}
