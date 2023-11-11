using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRecord.BasicComponent
{
	public class ArmBase : Base
	{
		public readonly int normalAttackCode;
		private ActiveSkillBase? _normalAttack = null;
		public ref readonly ActiveSkillBase? normalAttack => ref _normalAttack;
		public readonly int skillAttackCode;
		private ActiveSkillBase? _skillAttack = null;
		public ref readonly ActiveSkillBase? skillAttack => ref _skillAttack;
		public readonly int specialSkillCode;
		private ActiveSkillBase? _specialSkill = null;
		public ref readonly ActiveSkillBase? specialSkill => ref _specialSkill;

		public ArmBase(string str) : base(str)
		{
			string[] values = base.Parse(str);
			normalAttackCode = base.BaseIntParse(values[1]);
			skillAttackCode = base.BaseIntParse(values[2]);
			specialSkillCode = base.BaseIntParse(values[3]);
		}

		public void SetNormalAttack(ActiveSkillBase skill)
		{
			_normalAttack = skill;
		}

		public void SetSkillAttack(ActiveSkillBase skill)
		{
			_skillAttack = skill;
		}

		public void SetSpecialSkill(ActiveSkillBase skill)
		{
			_specialSkill = skill;
		}
	}
}
