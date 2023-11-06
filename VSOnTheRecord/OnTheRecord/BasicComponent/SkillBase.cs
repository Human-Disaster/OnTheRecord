using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OnTheRecord.BasicComponent
{
	public class ActiveSkillBase : IComparable
	{
		private static string _csvWordSplit = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";

		private readonly int _skillCode;
		public readonly int nameCode;
		public readonly int specialMechanismCode;
		public readonly int constraintCode;
		public readonly int hpConsumption;
		public readonly int apConsumption;
		public readonly int sanConsumption;
		public readonly int skillType;
		public readonly int aimmingRange;
		public readonly int effectRange;
		public readonly DamageInfo damage1;
		public readonly DamageInfo damage2;
		public readonly DamageInfo damage3;
		public readonly TokenInfo grantToken1;
		public readonly TokenInfo grantToken2;
		public readonly TokenInfo grantToken3;
		public readonly TokenInfo removeToken1;
		public readonly TokenInfo removeToken2;
		public readonly TokenInfo removeToken3;
		public readonly int addStatsCode;

		public readonly int mulStatsCode;

		public ActiveSkillBase(string str)
		{
			var values = Regex.Split(str, _csvWordSplit);
		}

		public ActiveSkillBase(int code)
		{
			_skillCode = code;
		}

		public int CompareTo(object? obj)
		{
			if (obj == null)
				return 1;
			ActiveSkillBase? other = obj as ActiveSkillBase;
			if (other is not null)
				return this._skillCode.CompareTo(other._skillCode);
			else
				throw new ArgumentException("Object is not a ActiveSkillBase");
		}
	}
}
