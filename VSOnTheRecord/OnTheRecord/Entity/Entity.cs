using OnTheRecord.BasicComponent;

namespace OnTheRecord.Entity
{
	public class Entity
	{
		public readonly bool penetrateable;
		public readonly int a;
		public Entity(bool penetrateable)
		{
			this.penetrateable = penetrateable;
			a = -1;
		}
		public Entity(bool penetrateable, int a)
		{
			this.penetrateable = penetrateable;
			this.a = a;
		}
	}
}