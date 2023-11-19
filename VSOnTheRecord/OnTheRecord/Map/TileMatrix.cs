using OnTheRecord.Entity;
using System;
using System.Diagnostics;

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

		public void SetEntity(int row, int col, Entity.Entity entity) {
			if(row < 0 || row >= rowCount || col < 0 || col >= colCount) {
				return;
			}
			matrix[row * colCount + col].SetEntity(entity);
		}

		public void SetState(int row, int col, TileState state) {
			if(row < 0 || row >= rowCount || col < 0 || col >= colCount) {
				return;
			}
			matrix[row * colCount + col].SetState(state);
		}

		public Tile GetTile(int row, int col)
		{
			return matrix[row * colCount + col];
		}

		public TileState? GetState(int row, int col)
		{
			if(row < 0 || row >= rowCount || col < 0 || col >= colCount)
				return null;
			return matrix[row * colCount + col].GetState();
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

		public bool IsValid(int row, int col)
		{
			if (row < 0 || row >= rowCount || col < 0 || col >= colCount)
				return false;
			return true;
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
				check += checkAdd;
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
	}
}

