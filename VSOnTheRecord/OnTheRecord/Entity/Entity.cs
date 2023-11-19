using OnTheRecord.BasicComponent;

namespace OnTheRecord.Entity
{
	public class Entity
	{
		public readonly bool penetrateable;
		public Entity(bool penetrateable)
		{
			this.penetrateable = penetrateable;
		}
	}
}