using System.Collections.Generic;
using OnTheRecord.BasicComponent;
using ExternalStaticReference;

namespace OnTheRecord.Entity
{
	public class ActivableComparer : IComparer<Activable>
	{
		public int Compare(Activable x, Activable y)
		{
			if (x.tokenList.GetTokenStack((int)TokenCode.FirstStrike) > 0 &&
				y.tokenList.GetTokenStack((int)TokenCode.FirstStrike) == 0)
				return 1;
			else if (x.tokenList.GetTokenStack((int)TokenCode.FirstStrike) == 0 &&
				y.tokenList.GetTokenStack((int)TokenCode.FirstStrike) > 0)
				return -1;
			if (x.finalStats.spdS > y.finalStats.spdS)
				return 1;
			else if (x.finalStats.spdS < y.finalStats.spdS)
				return -1;
			else
				return 0;
		}
	}
}