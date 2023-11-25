using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRecord.BasicComponent
{
	public class PassiveSkill
	{
		public readonly PassiveSkillBase skillBase;

		public PassiveSkill(PassiveSkillBase skillBase)
		{
			this.skillBase = skillBase;
		}
		public PassiveSkill(int skillCode)
		{
			skillBase = OnMemoryTable.Instance().GetPassiveSkillBase(skillCode);
		}

		public void Passive_trggrcheck(int situation)
		{
			// todo 상황에 따라 패시브 동작
		}

		private void Passive_on() // to do 패시브 동작
		{
		}

		private void Passive_off()
		{
		}
	}
}
