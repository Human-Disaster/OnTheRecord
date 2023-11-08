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

		public TokenBase(string str)
		{
			String[] values = Parse(str);
			_baseCode = int.Parse(values[0]);
			nameCode = int.Parse(values[1]);
			tokenType = int.Parse(values[2]);
			unSeen = int.Parse(values[3]) == 1;
			special = int.Parse(values[4]);
			effectOverlappable = int.Parse(values[5]) == 1;
			overlapMax = int.Parse(values[6]);
			removeSituation1 = int.Parse(values[7]);
			removeSituation2 = int.Parse(values[8]);
			removeSituation3 = int.Parse(values[9]);

			// 토큰 코드, 승급시 개수 1 고정, targetCode로 승급 여부 판단
			promotionToken = new TokenInfo(int.Parse(values[11], 1, int.Parse(values[10])));
			// 토큰 코드, 강등시 개수, targetCode로 강등 여부 판단
			demotionToken = new TokenInfo(int.Parse(values[13], values[14], int.Parse(values[12]));

			hpValueWhenRemove = float.Parse(values[15]);
			apValueWhenRemove = float.Parse(values[16]);
			sanValueWhenRemove = float.Parse(values[17]);
			damageWhenRemove = int.Parse(values[18]) == 1;
			damageValueWhenRemove = float.Parse(values[19]);
			damageTypeWhenRemove = int.Parse(values[20]);
			addStatsCode = int.Parse(values[21]);
			mulStatsCode = int.Parse(values[22]);
		}

		public void SetAddStats(StatsBase? stats)
		{
			_addStats = stats;
		}

		public void SetMulStats(StatsBase? stats)
		{
			_mulStats = stats;
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
			return (this._tokenCode == other._tokenCode);
		}
	}
}
