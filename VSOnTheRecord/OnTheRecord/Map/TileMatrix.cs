using OnTheRecord.Entity;
using System;

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

        public bool IsValid(int row, int col)
        {
            if (row < 0 || row >= rowCount || col < 0 || col >= colCount)
                return false;
            return true;
        }
    }
}

