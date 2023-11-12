using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OnTheRecord.BasicComponent
{
	public class TokenBase : Base
	{
		public readonly int nameCode;
		public readonly int tokenType;
		public readonly bool unSeen;
		public readonly int special;
		public readonly bool effectOverlappable;
		public readonly int overlapMax;
		public readonly int removeSituation1;
		public readonly int removeSituation2;
		public readonly int removeSituation3;

		public readonly TokenInfo promotionToken;
		public readonly TokenInfo demotionToken;

		public readonly float hpValueWhenRemove;
		public readonly float apValueWhenRemove;
		public readonly float sanValueWhenRemove;
		public readonly bool damageWhenRemove;
		public readonly float damageValueWhenRemove;
		public readonly int damageTypeWhenRemove;
		public readonly int addStatsCode;
		private StatsBase? _addStats = null;
		public ref readonly StatsBase? addStats => ref _addStats;
		public readonly int mulStatsCode;
		private StatsBase? _mulStats = null;
		public ref readonly StatsBase? mulStats => ref _mulStats;
		public readonly int secondMulStatsCode;
		private StatsBase? _secondMulStats = null;
		public ref readonly StatsBase? secondMulStats => ref _secondMulStats;

		public TokenBase(string str) : base(str)
		{
			string[] values = Parse(str);
			nameCode = base.BaseIntParse(values[1]);
			tokenType = base.BaseIntParse(values[2]);
			unSeen = base.BaseIntParse(values[3]) == 1;
			special = base.BaseIntParse(values[4]);
			effectOverlappable = base.BaseIntParse(values[5]) == 1;
			overlapMax = base.BaseIntParse(values[6]);
			removeSituation1 = base.BaseIntParse(values[7]);
			removeSituation2 = base.BaseIntParse(values[8]);
			removeSituation3 = base.BaseIntParse(values[9]);

			// 토큰 코드, 승급시 개수 1 고정, targetCode로 승급 여부 판단
			promotionToken = new TokenInfo(base.BaseIntParse(values[11]), 1, base.BaseIntParse(values[10]));
			// 토큰 코드, 강등시 개수, targetCode로 강등 여부 판단
			demotionToken = new TokenInfo(base.BaseIntParse(values[13]), base.BaseIntParse(values[14]), base.BaseIntParse(values[12]));

			hpValueWhenRemove = base.BaseFloatParse(values[15]);
			apValueWhenRemove = base.BaseFloatParse(values[16]);
			sanValueWhenRemove = base.BaseFloatParse(values[17]);
			damageWhenRemove = base.BaseIntParse(values[18]) == 1;
			damageValueWhenRemove = base.BaseFloatParse(values[19]);
			damageTypeWhenRemove = base.BaseIntParse(values[20]);
			addStatsCode = base.BaseIntParse(values[21]);
			mulStatsCode = base.BaseIntParse(values[22]);
			secondMulStatsCode = base.BaseIntParse(values[23]);
		}

		public void SetAddStats(StatsBase? stats)
		{
			_addStats = stats;
		}

		public void SetMulStats(StatsBase? stats)
		{
			_mulStats = stats;
		}

		public void SetSecondMulStats(StatsBase? stats)
		{
			_secondMulStats = stats;
		}

		public static bool operator ==(TokenBase a, TokenBase b)
		{
			if (((object)a) == null || ((object)b) == null)
				return Object.Equals(a, b);
			return a.Equals(b);
		}

		public static bool operator !=(TokenBase a, TokenBase b)
		{
			if (((object)a) == null || ((object)b) == null)
				return !(Object.Equals(a, b));
			return !(a.Equals(b));
		}

		public override bool Equals(object? obj)
		{
			if (obj == null)
				return false;
			TokenBase? other = obj as TokenBase;
			if (other is null)
				return false;
			return (this._baseCode == other._baseCode);
		}
	}
}
