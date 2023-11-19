namespace OnTheRecord.Map
{
	public class TileState
	{
		public readonly bool isMovable;
		public TileState(bool isMovable)
		{
			this.isMovable = isMovable;
		}
	}

	public class PlainTileState : TileState
	{
		public PlainTileState() : base(true)
		{
		}
	}

	public class WallTileState : TileState
	{
		public WallTileState() : base(false)
		{
		}
	}
}