using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRecord.BasicComponent
{
	public class PassiveSkillBase : Base
	{
		public readonly int nameCode;

		public PassiveSkillBase(string str) : base(str)
		{
			var values = base.Parse(str);
			nameCode = base.BaseIntParse(values[1]);
		}
	}
}
