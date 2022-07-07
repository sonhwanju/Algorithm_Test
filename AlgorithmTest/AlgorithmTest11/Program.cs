using System;

namespace AlgorithmTest11
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution s = new Solution();

            Console.WriteLine(s.solution(new int[,] { { 1, 1, 1 },{ 1, 1, 1 },{ 1, 1, 1 } }, new int[] {1,0 }, new int[] {1,2 }));
        }
    }

    public class Solution
    {
        private int[,] dir = { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };

        public int solution(int[,] board, int[] aloc, int[] bloc)
        {
            int answer = int.MaxValue;

            Dfs(board, aloc, bloc, 0,true, ref answer);

            return answer.Equals(int.MaxValue) ? 0 : answer;
        }

        public void Dfs(int[,] board, int[] aloc, int[] bloc,int count,bool isATurn,ref int answer)
        {
            if(isATurn)
            {
                for (int i = 0; i < dir.GetLength(0); i++)
                {
                    if (board[aloc[0], aloc[1]].Equals(0))
                    {
                        answer = Math.Min(answer, count);
                        return;
                    }

                    int defaultRow = aloc[0];
                    int defaultColumn = aloc[1];

                    int nextRow = dir[i, 0] + defaultRow;
                    int nextColumn = dir[i, 1] + defaultColumn;

                    board[defaultRow, defaultColumn] = 0;
                    aloc[0] = nextRow;
                    aloc[1] = nextColumn;

                    Dfs(board, aloc, bloc, count + 1, !isATurn, ref answer);

                    aloc[0] = defaultRow;
                    aloc[1] = defaultColumn;
                    board[defaultRow, defaultColumn] = 1;
                }
            }
            else
            {
                for (int i = 0; i < dir.GetLength(0); i++)
                {
                    int defaultRow = bloc[0];
                    int defaultColumn = bloc[1];

                    if (defaultRow < 0 || defaultColumn < 0 || defaultRow >= board.GetLength(0) || defaultColumn >= board.GetLength(1)) return;

                    if (board[defaultRow, defaultColumn].Equals(0)) //이건 같은 자리에 있었을때의 처리
                    {
                        answer = Math.Min(answer, count);
                        return;
                    }

                    int nextRow = dir[i, 0] + defaultRow;
                    int nextColumn = dir[i, 1] + defaultColumn;

                    board[defaultRow, defaultColumn] = 0;
                    aloc[0] = nextRow;
                    aloc[1] = nextColumn;

                    Dfs(board, aloc, bloc, count + 1, !isATurn, ref answer);

                    aloc[0] = defaultRow;
                    aloc[1] = defaultColumn;
                    board[defaultRow, defaultColumn] = 1;
                }
            }
        }
    }
}
