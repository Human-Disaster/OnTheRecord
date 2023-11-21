using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRecord.Map
{

    public class Room
    {
        private TileMatrix tiles;
        public int[,] seed;

        public Room()
        {
            tiles = new TileMatrix(0, 0);
            seed = new int[0, 0];
        }

        public Room(TileMatrix tiles)
        {
            this.tiles = tiles;
            seed = new int[tiles.GetLength(0), tiles.GetLength(1)];
        }

        public Room(int row, int col, float rate, TileState plain, int[,] weight)
        {
            tiles = new TileMatrix(row, col);
            seed = new int[tiles.GetLength(0), tiles.GetLength(1)];
            Room_generate(weight, plain, (int)(row * col * rate));
        }

        public Room(int row, int col, int max, TileState plain, int[,] weight)
        {
            tiles = new TileMatrix(row, col);
            seed = new int[tiles.GetLength(0), tiles.GetLength(1)];
            Room_generate(weight, plain, max);
        }

        private enum RoomParsNum
        {
            Movable = 0,
            Wall = 1
        }

        public Room(params object[] objs)
        {
            int[] needPars = (int[])objs[0];
            tiles = new TileMatrix((int)objs[1], (int)objs[2]);
            object obj;
            TileState movable = new PlainTileState();
            TileState wall = new WallTileState();
            for (int loc = 0; loc < (int)objs[1] * (int)objs[2]; loc++)
            {
                if (needPars[loc] == (int)RoomParsNum.Wall)
                    tiles.SetState(loc, wall);
                else
                    tiles.SetState(loc, movable);
                {
                    obj = objs[needPars[loc] + 1];
                    if (!(obj as TileState is null))
                        tiles.SetState(loc, obj as TileState);
                    else if (!(obj as Entity.Entity is null))
                    {
                        tiles.SetState(loc, movable);
                        tiles.SetEntity(loc, obj as Entity.Entity);
                    }
                    else
                        throw new Exception("Invalid parameter");
                }
            }
            seed = new int[0, 0];
        }

        public Tile? GetTile(int row, int col)
        {
			return tiles.GetTile(row, col);
		}

        public int GetRow()
        {
			return tiles.GetRowLength();
		}

        public int GetCol()
        {
            return tiles.GetColLength();
        }

        public void PrintMatrix()
        {
            string s;
            int i, j;

            i = -1;
            while (++i < tiles.GetLength(0))
            {
                s = string.Empty;
                j = -1;
                while (++j < tiles.GetLength(1))
                {
                    if (tiles.GetStateType(i, j) == null)
                        s += ". ";
                    else
                    {
                        s += seed[i, j].ToString("D1");
                        s += ' ';
                        //s += "x ";
                    }
                }
                Console.WriteLine(s);
            }
        }

        public void RemoveNullRowsAndColumns()
        {
            List<int> nullRows = new List<int>();
            List<int> nullCols = new List<int>();

            // Find rows and columns with only null entities
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                bool hasEntity = false;
                for (int j = 0; j < tiles.GetLength(1); j++)
                    if (tiles.GetStateType(i, j) != null)
                    {
                        hasEntity = true;
                        break;
                    }
                if (!hasEntity)
                    nullRows.Add(i);
            }

            for (int i = 0; i < tiles.GetLength(1); i++)
            {
                bool hasEntity = false;
                for (int j = 0; j < tiles.GetLength(0); j++)
                    if (tiles.GetStateType(j, i) != null)
                    {
                        hasEntity = true;
                        break;
                    }
                if (!hasEntity)
                    nullCols.Add(i);
            }

            // Remove null rows and columns
            nullRows.Sort();
            for (int i = nullRows.Count - 1; i >= 0; i--)
                tiles.RemoveRow(nullRows[i]);

            nullCols.Sort();
            for (int i = nullCols.Count - 1; i >= 0; i--)
                tiles.RemoveColumn(nullCols[i]);
        }

        private void Room_generate(int[,] weight, TileState plain, int max)
        {
            // 1. weight의 행과 열이 짝수인 경우 함수 종료
            if (weight.GetLength(0) % 2 == 0 || weight.GetLength(1) % 2 == 0)
                return;

            List<int> coords = new List<int>();
            int rowSize = tiles.GetLength(0), colSize = tiles.GetLength(1);
            int[,] map = new int[rowSize, colSize];
            int count = 0;
            int randMax = 1;
            int cx, cy, next, i, j;
            int interverSize = 0;
            int[] interverCount = new int[10];

            // 구간 초기화
            for (i = 0; i < weight.GetLength(0); ++i)
                for (j = 0; j < weight.GetLength(1); ++j)
                    interverSize += weight[i, j];
            interverSize /= 10;
            ++interverSize;

            // 현재 위치를 중앙으로 초기화
            int currentRow = rowSize / 2;
            int currentCol = colSize / 2;

            while (++count < max)
            {
                // 2. tiles의 currentPosition 좌표 Tile의 SetEntity(empty) 호출
                tiles.SetState(currentRow, currentCol, plain);
                seed[currentRow, currentCol] = map[currentRow, currentCol] / interverSize;
                ++interverCount[seed[currentRow, currentCol]];

                // 3. currentPosition 좌표를 coords에서 찾아서 지워
                coords.Remove(currentRow * colSize + currentCol);

                // 4. randMax에서 currentPosition 좌표의 map의 값을 빼
                randMax -= map[currentRow, currentCol];

                // 5. randMax에 currentPosition 좌표의 상하좌우의 타일 중 Entity가 empty가 아니고 coords에 없는 좌표들에 해당되는 map의 값들을 더하고 coords에 추가해 
                if (currentRow > 0 &&
                    tiles.GetStateType(currentRow - 1, currentCol) != typeof(PlainTileState) &&
                    !coords.Contains((currentRow - 1) * colSize + currentCol))
                    {
                        randMax += map[currentRow - 1, currentCol];
                        coords.Add((currentRow - 1) * colSize + currentCol);
                    }
                if (currentRow < rowSize - 1 &&
                    tiles.GetStateType(currentRow + 1, currentCol) != typeof(PlainTileState) &&
                    !coords.Contains((currentRow + 1) * colSize + currentCol))
                    {
                        randMax += map[currentRow + 1, currentCol];
                        coords.Add((currentRow + 1) * colSize + currentCol);
                    }
                if (currentCol > 0 &&
                    tiles.GetStateType(currentRow, currentCol - 1) != typeof(PlainTileState) &&
                    !coords.Contains(currentRow * colSize + currentCol - 1))
                    {
                        randMax += map[currentRow, currentCol - 1];
                        coords.Add(currentRow * colSize + currentCol - 1);
                    }
                if (currentCol < colSize - 1 &&
                    tiles.GetStateType(currentRow, currentCol + 1) != typeof(PlainTileState) &&
                    !coords.Contains(currentRow * colSize + currentCol + 1))
                    {
                        randMax += map[currentRow, currentCol + 1];
                        coords.Add(currentRow * colSize + currentCol + 1);
                    }

                // 6. currentPosition 좌표를 weight 중앙의 기준으로 삼아서 weight의 각 원소를 해당되는 map에 더해 그리고 그 좌표가 coords에 있는 경우에 원소를 randMax에 더햊
                cx = currentRow - weight.GetLength(0) / 2;
                cy = currentCol - weight.GetLength(1) / 2;
                for (i = 0; i < weight.GetLength(0); i++)
                    for (j = 0; j < weight.GetLength(1); j++)
                        if (i + cx >= 0 && i + cx < rowSize &&
                            j + cy >= 0 && j + cy < colSize)
                        {
                            map[i + cx, j + cy] += weight[i, j];
                            if (coords.Contains((i + cx) * colSize + j + cy))
                                randMax += weight[i, j];
                        }

                // 7. 랜덤한 정수를 하나 Checkrand_int를 이용해서 최대 randMax가 되도록 뽑아.
                next = Checkrand.Checkrand_int(0, randMax);

                // 8. coords의 첫번째 좌표부터 시작해서 해당되는 좌표들의 map의 값을 방금 뽑은 랜덤한 정수에 빼는 작업을 반복해. 이 반복문의 조건은 방금 뽑은 랜덤한 정수가 0 이하가 될 때 까지야.
                for (i = 0;; i++)
                {
                    next -= map[coords[i] / colSize, coords[i] % colSize];
                    if (next <= 0)
                        break;
                }

                // 9. 8.에서 탈출하게 해준 그 좌표를 currentPosition에 대입해.
                currentRow = coords[i] / colSize;
                currentCol = coords[i] % colSize;
                //this.PrintMatrix();
            }
            for (i = 0; i < 10; ++i)
                Console.WriteLine(i + " : " + interverCount[i]);
        }
        
        public ActivableList GetActivalbleList()
        {
            ActivableList activableList = new ActivableList();
            int max = tiles.GetLength(0) * tiles.GetLength(1);
            for (int i = 0; i < max; i++)
            {
                Entity.Entity entity = tiles.GetEntity(i);
                if (!(entity is null) && entity is ActivableEntity)
                    activableList.Add(tiles.GetState(i, j));
            }
            return activableList;
        }
    }
}