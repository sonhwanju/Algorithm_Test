using System;
using System.Linq;
using System.Collections.Generic;

namespace AlgorithmTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution s = new Solution();
            Console.WriteLine(s.solution(new int[,] {
                { 1, 0, 1, 1, 1 },
                { 1, 0, 1, 0, 1 },
                { 1, 0, 1, 1, 1 },
                { 1, 1, 1, 0, 1 },
                { 0, 0, 0, 0, 1 } }));

            Console.WriteLine(s.solution(new int[,] {
                { 1, 0, 1, 1, 1 },
                { 1, 0, 1, 0, 1 }, 
                { 1, 0, 1, 1, 1 }, 
                { 1, 1, 1, 0, 0 }, 
                { 0, 0, 0, 0, 1 }
            }));
        }
    }

    //게임 맵 최단거리
    //https://programmers.co.kr/learn/courses/30/lessons/1844

    class Solution
    {
        Queue<Point> q = new Queue<Point>();

        private int[,] dir = { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };

        public int solution(int[,] maps)
        {
            if (maps.GetLength(0) < 1 || maps.GetLength(0) > 100 || (maps.GetLength(0) == 1 && maps.GetLength(1) == 1)) return -1;

            return Bfs(0, 0, maps.GetLength(0) -1, maps.GetLength(1)-1, maps);
        }

        public int Bfs(int startX,int startY,int targetX,int targetY,int[,] map)
        {
            bool[,] visited = new bool[map.GetLength(0), map.GetLength(1)];
            Point start = new Point(startX, startY, -1, 1);
            visited[startX, startY] = true;
            q.Enqueue(start);

            while (q.Count > 0)
            {
                Point p = q.Dequeue();
                if (p.x == targetX && p.y == targetY)
                {
                    return p.moveCount;
                }

                for (int i = 0; i < dir.GetLength(0); i++)
                {
                    if (p.dir != -1 && Math.Abs(p.dir - i) == 2) continue;

                    int nextX = p.x + dir[i, 0];
                    int nextY = p.y + dir[i, 1];
                    int nextMoveCount = p.moveCount + 1;

                    if (nextX < 0 || nextY < 0 || nextX >= map.GetLength(0) || nextY >= map.GetLength(1) || map[nextX,nextY] == 0 || visited[nextX,nextY])
                    {
                        continue;
                    }
                    visited[nextX, nextY] = true;
                    Point curPoint = new Point(nextX, nextY, i, nextMoveCount);
                    q.Enqueue(curPoint);
                }
            }

            return -1;
        }
    }

    public class Point
    {
        public int x;
        public int y;
        public int dir;
        public int moveCount;

        public Point()
        {

        }

        public Point(int x, int y, int dir, int moveCount)
        {
            this.x = x;
            this.y = y;
            this.dir = dir;
            this.moveCount = moveCount;
        }
    }
}
