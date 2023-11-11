using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRecord.BasicComponent
{
	public class ConsumableBase : Base
	{
		public readonly int nameCode;
		public readonly int specialCode;	// 특수 매커니즘 코드
		public readonly int skillCode;
		private ActiveSkillBase? _skill = null;
		public ref readonly ActiveSkillBase? skill => ref _skill;

		public ConsumableBase(string str) : base(str)
		{
			string[] values = base.Parse(str);
			specialCode = base.BaseIntParse(values[1]);
			skillCode = base.BaseIntParse(values[2]);
		}

		public void SetSkill(ActiveSkillBase skill)
		{
			_skill = skill;
		}
	}
}
