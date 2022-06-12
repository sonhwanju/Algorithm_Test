using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AlgorithmTest4
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution s = new Solution();
            Console.WriteLine(s.solution(3, 1, 3, new int[,] { { 1, 2, 2 }, { 3, 2, 3 } }, new int[] { 2 }));
            Console.WriteLine(s.solution(4, 1, 4, new int[,] { { 1, 2, 1 }, { 3, 2, 1 }, { 2, 4, 1 } }, new int[] { 2, 3 }));
        }
    }

    public class Solution
    {
        public int solution(int n, int start, int end, int[,] roads, int[] traps)
        {
            int roadsLength = roads.GetLength(0);
            int trapsLength = traps.GetLength(0);
            if (n < 2 || n > 1000 || start < 1 || start > n || end < 1 || end > n || roadsLength < 1 || roadsLength > 3000 || trapsLength < 0 || trapsLength > 10) return -1;

            int answer = int.MaxValue;
            PriorityQueue<Room> roomQueue = new PriorityQueue<Room>();

            roomQueue.Push(new Room(start, 0,roads));
            while (roomQueue.Count > 0)
            {
                Room r = roomQueue.Pop();

                if (r.curRoom == end)
                {
                    answer = Math.Min(answer, r.moveCount);
                    continue;
                }

                if (r.moveCount >= answer) break;

                //for (int j = 0; j < trapsLength; j++)
                //{
                //    if (r.curRoom == traps[j])
                //    {
                //        r.roads = Swap(r.roads, r);
                //        break;
                //    }
                //}

                if(traps.Contains(r.curRoom))
                {
                    r.roads = Swap(r.roads, r);
                }

                for (int i = 0; i < roadsLength; i++)
                {
                    if (roads[i, 0] > n || roads[i, 1] > n || roads[i, 0] < 1 || roads[i, 1] < 1 || roads[i,2] < 1 || roads[i,2] > 3000) continue;

                    if(r.roads[i,0] == r.curRoom)
                    {
                        //Console.WriteLine($"{r.curRoom} -> {r.roads[i,1]}");
                        int nextMoveCount = r.moveCount + r.roads[i, 2];

                        if (nextMoveCount >= answer) continue;

                        //Room nextRoom = new Room(r.roads[i, 1], nextMoveCount, traps.Contains(r.roads[i,1]) ? Swap(r.roads,r) :r.roads);
                        Room nextRoom = new Room(r.roads[i, 1], nextMoveCount, r.roads);
                        roomQueue.Push(nextRoom);
                    }
                }
            }


            return answer;
        }

        public int[,] Swap(int[,] roads,Room room)
        {
            for (int i = 0; i < roads.GetLength(0); i++)
            {
                if(roads[i,0] == room.curRoom || roads[i,1] == room.curRoom)
                {
                    int temp = roads[i, 0];
                    roads[i, 0] = roads[i, 1];
                    roads[i, 1] = temp;
                }
            }

            return roads;
        }
    }

    public class PriorityQueue<T> where T : IComparable<T>
    {
        List<T> _heap = new List<T>();

        public int Count { get { return _heap.Count; } }

        public void Push(T data)
        {
            //맨 처음에는 힙의 맨 끝에다가 데이터를 삽입한다.
            _heap.Add(data);

            int now = _heap.Count - 1; //현재 힙의 인덱스 카운트
            while (now > 0)
            {
                int next = (now - 1) / 2;
                // 0보다 작으면 now가 next보다 작다 
                if (_heap[now].CompareTo(_heap[next]) < 0)
                {
                    break;
                }

                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                now = next;
            }
        }

        public T Pop()
        {
            T ret = _heap[0];

            int lastIndex = _heap.Count - 1; //마지막 녀석을 가져옵니다.
            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex);
            lastIndex--;

            //이제 찾아서 내려가기
            int now = 0;
            while (true)
            {
                int left = 2 * now + 1;
                int right = 2 * now + 2;

                int next = now;

                if (left <= lastIndex && _heap[next].CompareTo(_heap[left]) < 0)
                {
                    next = left;
                }

                if (right <= lastIndex && _heap[next].CompareTo(_heap[right]) < 0)
                {
                    next = right;
                }

                if (next == now)
                    break;

                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                now = next;
            }

            return ret;
        }

        public T Peek()
        {
            return _heap.Count == 0 ? default(T) : _heap[0];
        }
    }

    public class Room : IComparable<Room>
    {
        public int curRoom;
        public int moveCount;
        public int[,] roads;

        public Room()
        {

        }

        public Room(int curRoom, int moveCount, int[,] roads)
        {
            this.curRoom = curRoom;
            this.moveCount = moveCount;
            this.roads = roads;
        }

        public int CompareTo(Room other)
        {
            return moveCount.CompareTo(other.moveCount);
        }
    }
}


//public int solution(int n, int start, int end, int[,] roads, int[] traps)
//{
//    int roadsLength = roads.GetLength(0);
//    int trapsLength = traps.GetLength(0);
//    if (n < 2 || n > 1000 || start < 1 || start > n || end < 1 || end > n || roadsLength < 1 || roadsLength > 3000 || trapsLength < 0 || trapsLength > 10) return -1;

//    int answer = int.MaxValue;
//    PriorityQueue<Room> roomQueue = new PriorityQueue<Room>();

//    roomQueue.Push(new Room(start, 0, roads));
//    while (roomQueue.Count > 0)
//    {
//        Room r = roomQueue.Pop();

//        if (r.curRoom == end)
//        {
//            answer = Math.Min(answer, r.moveCount);
//            continue;
//        }

//        if (r.moveCount >= answer) break;

//        for (int j = 0; j < trapsLength; j++)
//        {
//            if (r.curRoom == traps[j])
//            {
//                r.roads = Swap(r.roads, r);
//                break;
//            }
//        }

//        for (int i = 0; i < roadsLength; i++)
//        {
//            if (roads[i, 0] > n || roads[i, 1] > n || roads[i, 0] < 1 || roads[i, 1] < 1 || roads[i, 2] < 1 || roads[i, 2] > 3000) continue;

//            if (r.roads[i, 0] == r.curRoom)
//            {
//                //Console.WriteLine($"{r.curRoom} -> {r.roads[i,1]}");
//                int nextMoveCount = r.moveCount + r.roads[i, 2];

//                if (nextMoveCount >= answer) continue;

//                Room nextRoom = new Room(r.roads[i, 1], nextMoveCount, r.roads);
//                roomQueue.Push(nextRoom);
//            }
//        }
//    }


//    return answer;
//}