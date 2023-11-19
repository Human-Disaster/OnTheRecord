using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRecord.BasicComponent
{
	public class SpecialNPCBase : Base
	{
		private enum ParsNum
		{
			BaseCode,
			NameCode,
			AICode,
			SizeRow,
			SizeCol,
			GroupAddStatsCode,
			GroupMulStatsCode,
			RollAddStatsCode,
			RollMulStatsCode,
			ProficiAddStatsCode,
			ProficiMulStatsCode,
			ActiveSkillCode1,
			ActiveSkillCode2,
			ActiveSkillCode3,
			ActiveSkillCode4,
			ActiveSkillCode5,
			ActiveSkillCode6
		}
		public const int activeSkillCodeNum = 6;
		public readonly int nameCode;
		public readonly int aiCode;
		public readonly int sizeRow;
		public readonly int sizeCol;
		public readonly int groupAddStatsCode;
		private StatsBase? _groupAddStats = null;
		public ref readonly StatsBase? groupAddStats => ref _groupAddStats;
		public readonly int groupMulStatsCode;
		private StatsBase? _groupMulStats = null;
		public ref readonly StatsBase? groupMulStats => ref _groupMulStats;
		public readonly int rollAddStatsCode;
		private StatsBase? _rollAddStats = null;
		public ref readonly StatsBase? rollAddStats => ref _rollAddStats;
		public readonly int rollMulStatsCode;
		private StatsBase? _rollMulStats = null;
		public ref readonly StatsBase? rollMulStats => ref _rollMulStats;
		public readonly int proficiAddStatsCode;
		private StatsBase? _proficiAddStats = null;
		public ref readonly StatsBase? proficiAddStats => ref _proficiAddStats;
		public readonly int proficiMulStatsCode;
		private StatsBase? _proficiMulStats = null;
		public ref readonly StatsBase? proficiMulStats => ref _proficiMulStats;
		private readonly int[] _activeSkillCode = new int[activeSkillCodeNum];
		public ReadOnlySpan<int> activeSkillCode => new ReadOnlySpan<int>(_activeSkillCode);
		private readonly ActiveSkillBase?[] _activeSkill = new ActiveSkillBase?[activeSkillCodeNum];
		public ref readonly ActiveSkillBase?[] activeSkill => ref _activeSkill;

		public SpecialNPCBase(string str) : base(str)
		{
			string[] values = base.Parse(str);
			nameCode = base.BaseIntParse(values[(int)ParsNum.NameCode]);
			aiCode = base.BaseIntParse(values[(int)ParsNum.AICode]);
			sizeRow = base.BaseIntParse(values[(int)ParsNum.SizeRow]);
			sizeCol = base.BaseIntParse(values[(int)ParsNum.SizeCol]);
			groupAddStatsCode = base.BaseIntParse(values[(int)ParsNum.GroupAddStatsCode]);
			groupMulStatsCode = base.BaseIntParse(values[(int)ParsNum.GroupMulStatsCode]);
			rollAddStatsCode = base.BaseIntParse(values[(int)ParsNum.RollAddStatsCode]);
			rollMulStatsCode = base.BaseIntParse(values[(int)ParsNum.RollMulStatsCode]);
			proficiAddStatsCode = base.BaseIntParse(values[(int)ParsNum.ProficiAddStatsCode]);
			proficiMulStatsCode = base.BaseIntParse(values[(int)ParsNum.ProficiMulStatsCode]);
			for (int i = 0; i < activeSkillCodeNum; i++)
			{
				_activeSkillCode[i] = base.BaseIntParse(values[(int)ParsNum.ActiveSkillCode1 + i]);
			}

		}

		public void SetGroupAddStats(StatsBase stats)
		{
			_groupAddStats = stats;
		}

		public void SetGroupMulStats(StatsBase stats)
		{
			_groupMulStats = stats;
		}

		public void SetRollAddStats(StatsBase stats)
		{
			_rollAddStats = stats;
		}

		public void SetRollMulStats(StatsBase stats)
		{
			_rollMulStats = stats;
		}

		public void SetProficiAddStats(StatsBase stats)
		{
			_proficiAddStats = stats;
		}

		public void SetProficiMulStats(StatsBase stats)
		{
			_proficiMulStats = stats;
		}

		public void SetActiveSkill(ActiveSkillBase skill, int index)
		{
			_activeSkill[index] = skill;
		}
	}
}
