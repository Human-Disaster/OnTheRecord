using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRecord.Map
{
	class Map
	{
		List<Room?> _rooms;
		List<int> _path;
		// 0b0001: right
		// 0b0010: left
		// 0b0100: down
		// 0b1000: up
		List<bool> _visited;
		int _row;
		int _col;

		public Map(int roomCount, int row, int col)
		{
			_rooms = new List<Room?>(row * col);
			_path = new List<int>(row * col);
			_visited = new List<bool>(row * col);
			_row = row;
			_col = col;
			_visited[(row / 2) * col + col / 2] = true;
			List<bool> generatedRoom = new List<bool>(row * col);
			List<int> posiblePath = new List<int>();
			SortedSet<int> addedPosible = new SortedSet<int>();
			int y = row / 2;
			int x = col / 2;
			int last;
			int nextPath;
			bool flag;
			int rand;
			int	roomNum = 0;
			last = y * col + x;
			generatedRoom[last] = true;
			// Initialize the map
			while (roomNum++ < roomCount)
			{
				if (x != 0 && !addedPosible.Contains((y * (col - 1) + x - 1) * 2))
				{
					posiblePath.Add((y * (col - 1) + x - 1) * 2);
					addedPosible.Add((y * (col - 1) + x - 1) * 2);
				}
				if (x < col - 1 && !addedPosible.Contains((y * (col - 1) + x) * 2))
				{
					posiblePath.Add((y * (col - 1) + x) * 2);
					addedPosible.Add((y * (col - 1) + x) * 2);
				}
				if (y != 0 && !addedPosible.Contains(((y - 1) * col + x) * 2 + 1))
				{
					posiblePath.Add(((y - 1) * col + x) * 2 + 1);
					addedPosible.Add(((y - 1) * col + x) * 2 + 1);
				}
				if (y < row - 1 && !addedPosible.Contains((y * col + x) * 2 + 1))
				{
					posiblePath.Add((y * col + x) * 2 + 1);
					addedPosible.Add((y * col + x) * 2 + 1);
				}

				flag = false;
				while (!flag)
				{
					rand = Checkrand.Checkrand_int(0, posiblePath.Count);
					nextPath = posiblePath[rand];
					posiblePath.RemoveAt(rand);
					if ((nextPath & 1) == 0)
					{
						nextPath /= 2;
						y = nextPath / (col - 1);
						x = nextPath % (col - 1);
						_path[y * col + x] |= 0b0001;
						_path[y * col + x + 1] |= 0b0010;
						if (!generatedRoom[y * col + x])
						{
							flag = true;
							break;
						}
						else if (!generatedRoom[y * col + x + 1])
						{
							flag = true;
							x++;
							break;
						}
					}
					else
					{
						nextPath /= 2;
						y = nextPath / col;
						x = nextPath % col;
						_path[y * col + x] |= 0b0100;
						_path[(y + 1) * col + x] |= 0b1000;
						if (!generatedRoom[y * col + x])
						{
							flag = true;
							break;
						}
						else if (!generatedRoom[(y + 1) * col + x])
						{
							flag = true;
							y++;
							break;
						}
					}
				}
				last = y * col + x;
				generatedRoom[last] = true;
			}
			// Generate the rooms
			for (x = 0; x < col * row; ++x)
			{
				if (generatedRoom[x])
				{
					//_rooms.Add(여기에 원하는 룸 집어넣기)
					//집어넣을 룸의 이어져있는 path를 확인하고 싶으면 _path[x] 참조
				}
				else
					_rooms.Add(null);
			}
			// _rooms[last] is the boss room
		}
	}
}
