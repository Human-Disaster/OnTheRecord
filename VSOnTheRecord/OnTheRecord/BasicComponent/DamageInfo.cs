using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRecord.BasicComponent
{
	public class DamageInfo
	{
		public readonly bool damageWhether;
		public readonly int damageValue;
		public readonly int damageType;

		public DamageInfo(bool damageWhether, int damageValue, int damageType)
		{
			this.damageWhether = true;
			this.damageValue = damageValue;
			this.damageType = damageType;
		}
	}
}
