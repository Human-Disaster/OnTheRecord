using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OnTheRecord.BasicComponent
{
	public class TokenBase : IComparable
	{
		private static string _csvWordSplit = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";

		private readonly int _tokenCode;
		public readonly int nameCode = 0;
		public readonly int tokenType = 0;
		public readonly bool unSeen = false;
		public readonly int special;
		public readonly bool effectOverlappable = false;
		public readonly int overlapMax;
		public readonly int removeSituation1;
		public readonly int removeSituation2;
		public readonly int removeSituation3;
		public readonly bool promotionable = false;
		public readonly int promotionTokenCode = 0;
		private TokenBase? _promotionToken = null;
		public ref readonly TokenBase promotionToken => ref _promotionToken;
		public readonly bool demotionable = false;
		public readonly int demotionTokenCode = 0;
		private TokenBase? _demotionToken = null;
		public ref readonly TokenBase demotionToken => ref _demotionToken;
		public readonly int demotionTokenAmount = 0;
		public readonly float hpValueWhenRemove = 0;
		public readonly float apValueWhenRemove = 0;
		public readonly float sanValueWhenRemove = 0;
		public readonly bool damageWhenRemove = false;
		public readonly float damageValueWhenRemove = 0;
		public readonly int damageTypeWhenRemove = 0;
		public readonly int addStatsCode;
		private StatsBase? _addStats = null;
		public ref readonly StatsBase addStats => ref _addStats;
		public readonly int mulStatsCode;
		private StatsBase? _mulStats = null;
		public ref readonly StatsBase mulStats => ref _mulStats;

		public TokenBase(string str)
		{
			var values = Regex.Split(str, _csvWordSplit);
			_tokenCode = int.Parse(values[0]);
			nameCode = int.Parse(values[1]);
			tokenType = int.Parse(values[2]);
			unSeen = int.Parse(values[3]) == 1;
			special = int.Parse(values[4]);
			effectOverlappable = int.Parse(values[5]) == 1;
			overlapMax = int.Parse(values[6]);
			removeSituation1 = int.Parse(values[7]);
			removeSituation2 = int.Parse(values[8]);
			removeSituation3 = int.Parse(values[9]);
			promotionable = int.Parse(values[10]) == 1;
			promotionTokenCode = int.Parse(values[11]);
			demotionable = int.Parse(values[12]) == 1;
			demotionTokenCode = int.Parse(values[13]);
			demotionTokenAmount = int.Parse(values[14]);
			hpValueWhenRemove = float.Parse(values[15]);
			apValueWhenRemove = float.Parse(values[16]);
			sanValueWhenRemove = float.Parse(values[17]);
			damageWhenRemove = int.Parse(values[18]) == 1;
			damageValueWhenRemove = float.Parse(values[19]);
			damageTypeWhenRemove = int.Parse(values[20]);
			addStatsCode = int.Parse(values[21]);
			mulStatsCode = int.Parse(values[22]);
		}

		public TokenBase(int code)
		{
			_tokenCode = code;
		}

		public int CompareTo(object? obj)
		{
			if (obj == null)
				return 1;
			TokenBase? other = obj as TokenBase;
			if (other is not null)
				return this._tokenCode.CompareTo(other._tokenCode);
			else
				throw new ArgumentException("Object is not a TokenBase");
		}

		public void SetPromotionToken(TokenBase? token)
		{
			_promotionToken = token;
		}

		public void SetDemotionToken(TokenBase? token)
		{
			_demotionToken = token;
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
