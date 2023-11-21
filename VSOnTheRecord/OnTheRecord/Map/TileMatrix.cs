using OnTheRecord.Entity;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using ExternalStaticReference;
using OnTheRecord.BasicComponent;

/*
 * 왼쪽 위가 0,0 오른쪽 아래가 col - 1, row - 1
 */

namespace OnTheRecord.Map
{
	public class TileMatrix
	{
		//Tiles_matrix 관련 함수 생성 필요 (타일 변경 작업용, Activable 검색, 공격 스밀 범위 판정, 표시방식 결정, 타일 내부변수 변경, 이동범위 판정, ETC.....)
		private List<Tile> matrix;
		private int rowCount;
		private int colCount;

		public TileMatrix(int row, int col) {
			matrix = new List<Tile>();
			rowCount = row;
			colCount = col;
			for (int i = 0; i < row * col; i++)
				matrix.Add(new Tile());
		}

		public void RemoveRow(int rowIndex) {
			if (colCount == 0 || rowCount == 0 || rowIndex >= rowCount || rowIndex < 0)
				return;
			matrix.RemoveRange(rowIndex * colCount, colCount);
			--rowCount;
		}

		public void RemoveColumn(int colIndex) {
			if (colCount == 0 || rowCount == 0 || colIndex >= colCount || colIndex < 0)
				return;
			for (int i = rowCount - 1; i <= 0; --i)
				matrix.RemoveAt(colCount * i + colIndex);
			--colCount;
		}

		public bool IsValid(int row, int col)
		{
			if (row < 0 || row >= rowCount || col < 0 || col >= colCount)
				return false;
			return true;
		}

		public bool IsValid(int loc)
		{
			if (loc < 0 || loc >= rowCount * colCount)
				return false;
			return true;
		}

		public bool IsValidRow(int row)
		{
			if (row < 0 || row >= rowCount)
				return false;
			return true;
		}

		public bool IsValidCol(int col)
		{
			if (col < 0 || col >= colCount)
				return false;
			return true;
		}

		public void SetEntity(int row, int col, Entity.Entity entity) {
			if (IsValid(row, col))
				matrix[row * colCount + col].SetEntity(entity);
		}

		public void SetEntity(int loc, Entity.Entity entity)
		{
			if (IsValid(loc))
				matrix[loc].SetEntity(entity);
		}

		public void SetState(int row, int col, TileState state) {
			if (IsValid(row, col))
				matrix[row * colCount + col].SetState(state);
		}

		public void SetState(int loc, TileState state) {
			if (IsValid(loc))
				matrix[loc].SetState(state);
		}

		public Tile? GetTile(int row, int col)
		{
			if (!IsValid(row, col))
				return null;
			return matrix[row * colCount + col];
		}

		public Tile? GetTile(int loc)
		{
			if (!IsValid(loc))
				return null;
			return matrix[loc];
		}

		public TileState? GetState(int row, int col)
		{
			if (row < 0 || row >= rowCount || col < 0 || col >= colCount)
				return null;
			return matrix[row * colCount + col].GetState();
		}

		public TileState? GetState(int loc)
		{
			if (loc < 0 || loc >= rowCount * colCount)
				return null;
			return matrix[loc].GetState();
		}

		public Type? GetStateType(int row, int col)
		{
			if(row < 0 || row >= rowCount || col < 0 || col >= colCount)
				return null;
			return matrix[row * colCount + col].GetStateType();
		}

		public int GetLength(int d)
		{
			if (d == 0)
				return rowCount;
			return colCount;
		}

		public int GetRowLength()
		{
			return rowCount;
		}

		public int GetColLength()
		{
			return colCount;
		}

		private int GCD(int a, int b)
		{
			if (b == 0)
				return a;
			return GCD(b, a % b);
		}

		public bool CheckPenetaratebleRay(int startR, int startC, int endR, int endC)
		{
			if (!IsValid(startR, startC) || !IsValid(endR, endC))
				return false;
			if (startR == endR && startC == endC)
				return true;
			int rowDiff = endR - startR;
			int colDiff = endC - startC;
			int gcd = GCD(Math.Abs(rowDiff), Math.Abs(colDiff));
			rowDiff /= gcd;
			colDiff /= gcd;
			int rowAdd;
			if (rowDiff == 0)
				rowAdd = 0;
			else
				rowAdd = rowDiff > 0 ? 1 : -1;
			int colAdd;
			if (colDiff == 0)
				colAdd = 0;
			else
				colAdd = colDiff > 0 ? 1 : -1;
			int dist = Math.Abs(rowDiff) > Math.Abs(colDiff) ? Math.Abs(rowDiff) : Math.Abs(colDiff);
			bool direction = Math.Abs(rowDiff) > Math.Abs(colDiff);
			int check = Math.Abs(rowDiff) > Math.Abs(colDiff) ? Math.Abs(colDiff) : Math.Abs(rowDiff);
			int checkAdd = check * 2;

			while (true)
			{
				if (direction)
					startR += rowAdd;
				else
					startC += colAdd;
				if (check == dist)
				{
					if (direction)
						startC += colAdd;
					else
						startR += rowAdd;
					check -= dist * 2;
					if (!GetTile(startR - rowAdd, startC).IsPenetrable() || !GetTile(startR, startC - colAdd).IsPenetrable())
						return false;
				}
				if (startR == endR && startC == endC)
					return true;
				if (!GetTile(startR, startC).IsPenetrable())
					return false;
				if (check > dist)
				{
					if (direction)
						startC += colAdd;
					else
						startR += rowAdd;
					check -= dist * 2;
					if (startR == endR && startC == endC)
						return true;
					if (!GetTile(startR, startC).IsPenetrable())
						return false;
				}
				check += checkAdd;
			}
		}

		public bool CheckRay(int startR, int startC, int endR, int endC)
		{
			if (!IsValid(startR, startC) || !IsValid(endR, endC))
				return false;
			if (startR == endR && startC == endC)
				return true;
			int rowDiff = endR - startR;
			int colDiff = endC - startC;
			int gcd = GCD(Math.Abs(rowDiff), Math.Abs(colDiff));
			rowDiff /= gcd;
			colDiff /= gcd;
			int rowAdd;
			if (rowDiff == 0)
				rowAdd = 0;
			else
				rowAdd = rowDiff > 0 ? 1 : -1;
			int colAdd;
			if (colDiff == 0)
				colAdd = 0;
			else
				colAdd = colDiff > 0 ? 1 : -1;
			int dist = Math.Abs(rowDiff) > Math.Abs(colDiff) ? Math.Abs(rowDiff) : Math.Abs(colDiff);
			bool direction = Math.Abs(rowDiff) > Math.Abs(colDiff);
			int check = Math.Abs(rowDiff) > Math.Abs(colDiff) ? Math.Abs(colDiff) : Math.Abs(rowDiff);
			int checkAdd = check * 2;

			while (true)
			{
				if (direction)
					startR += rowAdd;
				else
					startC += colAdd;
				if (check == dist)
				{
					if (direction)
						startC += colAdd;
					else
						startR += rowAdd;
					if (!GetTile(startR - rowAdd, startC).IsMovable() || !GetTile(startR, startC - colAdd).IsMovable())
						return false;
					check -= dist * 2;
				}
				if (startR == endR && startC == endC)
					return true;
				if (!GetTile(startR, startC).IsMovable())
					return false;
				if (check > dist)
				{
					if (direction)
						startC += colAdd;
					else
						startR += rowAdd;
					if (startR == endR && startC == endC)
						return true;
					if (!GetTile(startR, startC).IsMovable())
						return false;
					check -= dist * 2;
				}
				check += checkAdd;
			}
		}

		public void HighlightOff()
		{
			for (int i = 0; i < rowCount * colCount; i++)
				matrix[i].HighlightOff();
		}

		public void HighlightOn(int row, int col)
		{
			if (IsValid(row, col))
				return;
			matrix[row * colCount + col].HighlightOn();
		}

		private void MakeSquareRange(int row, int col, int range, List<int> tilesNum)
		{
			if (!IsValid(row, col))
				return;
			int startRow = row - range;
			int endRow = row + range;
			int startCol = col - range;
			int endCol = col + range;
			if (startRow < 0)
				startRow = 0;
			if (endRow >= rowCount)
				endRow = rowCount - 1;
			if (startCol < 0)
				startCol = 0;
			if (endCol >= colCount)
				endCol = colCount - 1;
			for (int i = startRow; i <= endRow; i++)
				for (int j = startCol; j <= endCol; j++)
					tilesNum.Add(i * colCount + j);
		}

		private void MakeCircleRange(int row, int col, int range, List<int> tilesNum)
		{
			if (!IsValid(row, col))
				return;
			int startRow = row - range;
			int endRow = row + range;
			int startCol = col - range;
			int endCol = col + range;
			range *= range;
			if (startRow < 0)
				startRow = 0;
			if (endRow >= rowCount)
				endRow = rowCount - 1;
			if (startCol < 0)
				startCol = 0;
			if (endCol >= colCount)
				endCol = colCount - 1;
			for (int i = startRow; i <= endRow; i++)
				for (int j = startCol; j <= endCol; j++)
					if ((row - i) * (row - i) + (col - j) * (col - j) <= range)
						tilesNum.Add(i * colCount + j);
		}

		private void MakeDiamondRange(int row, int col, int range, List<int> tilesNum)
		{
			if (!IsValid(row, col))
				return;
			int startRow = row - range;
			int endRow = row + range;
			int startCol = col - range;
			int endCol = col + range;
			if (startRow < 0)
				startRow = 0;
			if (endRow >= rowCount)
				endRow = rowCount - 1;
			if (startCol < 0)
				startCol = 0;
			if (endCol >= colCount)
				endCol = colCount - 1;
			for (int i = startRow; i <= endRow; i++)
				for (int j = startCol; j <= endCol; j++)
					if (Math.Abs(row - i) + Math.Abs(col - j) <= range)
						tilesNum.Add(i * colCount + j);
		}

		private void MakeCrossRange(int row, int col, int range, List<int> tilesNum)
		{
			if (!IsValid(row, col))
				return;
			tilesNum.Add(row * colCount + col);
			for (int i = 1; i <= range; i++)
			{
				if (IsValidRow(row - i))
					tilesNum.Add((row - i) * colCount + col);
				if (IsValidRow(row + i))
					tilesNum.Add((row + i) * colCount + col);
				if (IsValidCol(col - i))
					tilesNum.Add(row * colCount + col - i);
				if (IsValidCol(col + i))
					tilesNum.Add(row * colCount + col + i);
			}
		}

		private void MakeXShapeRange(int row, int col, int range, List<int> tilesNum)
		{
			if (!IsValid(row, col))
				return;
			tilesNum.Add(row * colCount + col);
			for (int i = 1; i <= range; i++)
			{
				if (IsValid(row - i, col - i))
					tilesNum.Add((row - i) * colCount + col - i);
				if (IsValid(row - i, col + i))
					tilesNum.Add((row - i) * colCount + col + i);
				if (IsValid(row + i, col - i))
					tilesNum.Add((row + i) * colCount + col - i);
				if (IsValid(row + i, col + i))
					tilesNum.Add((row + i) * colCount + col + i);
			}
		}

		private void MakeSpreadRange(int row, int col, int range, List<int> tilesNum)
		{
			if (!IsValid(row, col))
				return;
			tilesNum.Add(row * colCount + col);
			for (int i = 1; i <= range; i++)
			{
				if (IsValidRow(row - i))
					tilesNum.Add((row - i) * colCount + col);
				if (IsValidRow(row + i))
					tilesNum.Add((row + i) * colCount + col);
				if (IsValidCol(col - i))
					tilesNum.Add(row * colCount + col - i);
				if (IsValidCol(col + i))
					tilesNum.Add(row * colCount + col + i);
				if (IsValid(row - i, col - i))
					tilesNum.Add((row - i) * colCount + col - i);
				if (IsValid(row - i, col + i))
					tilesNum.Add((row - i) * colCount + col + i);
				if (IsValid(row + i, col - i))
					tilesNum.Add((row + i) * colCount + col - i);
				if (IsValid(row + i, col + i))
					tilesNum.Add((row + i) * colCount + col + i);
			}
		}

		private List<int> SkillAimRange(int row, int col, int aimType, int range)
		{
			List<int> result = new List<int>();
			switch (aimType)
			{
				case (int)AimingCode.Square:
					MakeSquareRange(row, col, range, result);
					break;
				case (int)AimingCode.Circle:
					MakeCircleRange(row, col, range, result);
					break;
				case (int)AimingCode.Diamond:
					MakeDiamondRange(row, col, range, result);
					break;
				case (int)AimingCode.Cross:
					MakeCrossRange(row, col, range, result);
					break;
				case (int)AimingCode.XShape:
					MakeXShapeRange(row, col, range, result);
					break;
				case (int)AimingCode.Spread:
					MakeSpreadRange(row, col, range, result);
					break;
			}
			return result;
		}

		public List<int> HighlightSkillRange(int row, int col, ActiveSkillBase skillBase)
		{
			if (!IsValid(row, col) || GetTile(row, col) is null || !GetTile(row, col).IsMovable())
				return new List<int>();
            HighlightOff();
			List<int> result = SkillAimRange(row, col, skillBase.aimmingType, skillBase.aimmingRange);
			for (int i = result.Count - 1; i >= 0; i--)
			{
				if (!GetTile(result[i]).IsMovable())
					result.RemoveAt(i);
			}
			switch (skillBase.skillType % 100)
			{
				case (int)SkillTypePostCode.Target:
					for (int i = result.Count - 1; i >= 0; i--)
					{
						if (GetTile(result[i]).GetEntity() is null)
							result.RemoveAt(i);
					}
					break;
				default:
					break;
			}
			switch (skillBase.skillType / 100 % 100)
			{
				case (int)SkillTypePreCode.Penetrate:
					for (int i = result.Count - 1; i >= 0; i--)
					{
						if (!CheckPenetaratebleRay(row, col, result[i] / colCount, result[i] % colCount))
							result.RemoveAt(i);
					}
					break;
				case (int)SkillTypePreCode.Direct:
				case (int)SkillTypePreCode.Travel:
				case (int)SkillTypePreCode.Charge:
				case (int)SkillTypePreCode.Jump:
					for (int i = result.Count - 1; i >= 0; i--)
					{
						if (!CheckRay(row, col, result[i] / colCount, result[i] % colCount))
							result.RemoveAt(i);
					}
					break;
			}
			return result;
		}
	}
}

