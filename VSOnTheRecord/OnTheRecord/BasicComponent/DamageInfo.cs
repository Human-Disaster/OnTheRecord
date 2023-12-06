using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRecord.BasicComponent
{
	public class DamageInfo
	{
		public readonly int damageTarget;
		public readonly int damageValue;
		public readonly int damageType;

		public DamageInfo(int damageTarget, int damageValue, int damageType)
		{
			this.damageTarget = damageTarget;
			this.damageValue = damageValue;
			this.damageType = damageType;
		}
	}
}
