using System.Collections.Generic;
using OnTheRecord.BasicComponent;

namespace OnTheRecord.Entity
{
	public class ActivableComparer : IComparer<Activable>
	{
		public int Compare(Activable x, Activable y)
		{
			if (a.tokenList.GetStack((int)TokenCode.FirstStrike) > 0 &&
				b.tokenList.GetStack((int)TokenCode.FirstStrike) == 0)
				return 1;
			else if (a.tokenList.GetStack((int)TokenCode.FirstStrike) == 0 &&
				b.tokenList.GetStack((int)TokenCode.FirstStrike) > 0)
				return -1;
			if (x.stats.speed > y.stats.speed)
				return 1;
			else if (x.stats.speed < y.stats.speed)
				return -1;
			else
				return 0;
		}
	}
}