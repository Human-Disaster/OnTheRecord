using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

/*
* 
*/
namespace OnTheRecord.BasicComponent
{
	public class ActiveSkillBase : Base
	{
		public readonly int nameCode;

		public readonly int preProcessCode;
		public readonly int postProcessCode;
		public readonly int constraintCode;
		public readonly int cooltime
		{
			get
			{
				if (constraintCode / 100000 % 10 != 2)
					return 0;
				else
					return constraintCode / 10000 % 10;
			}
		}
		public readonly int availableCount
		{
			get
			{
				if (constraintCode / 100000 % 10 != 3)
					return 0;
				else
					return constraintCode / 10000 % 10;
			}
		}

		public readonly int skillType;

		public readonly int hpConsumption;
		public readonly int apConsumption;
		public readonly int sanConsumption;

		public readonly int aimmingType;
		public readonly int aimmingRange;
		public readonly int effectType;
		public readonly int effectRange;

		public readonly bool recoveryWhether;
		public readonly int hpRecoveryValue;
		public readonly int apRecoveryValue;
		public readonly int sanRecoveryValue;

		public readonly DamageInfo damage1;
		public readonly DamageInfo damage2;
		public readonly DamageInfo damage3;
		public readonly TokenInfo grantToken1;
		public readonly TokenInfo grantToken2;
		public readonly TokenInfo grantToken3;
		public readonly TokenInfo removeToken1;
		public readonly TokenInfo removeToken2;
		public readonly TokenInfo removeToken3;

		public readonly int tileConditionCode;
		private TileConditionBase? _tileCondition = null;
		public ref readonly TileConditionBase? tileCondition => ref _tileCondition;
		public readonly int tileConditionTurn;

		public readonly int addStatsCode;
		private StatsBase? _addStats = null;
		public ref readonly StatsBase? addStats => ref _addStats;
		public readonly int mulStatsCode;
		private StatsBase? _mulStats = null;
		public ref readonly StatsBase? mulStats => ref _mulStats;

		public ActiveSkillBase(string str) : base(str)
		{
			string[] values = Parse(str);
			nameCode = int.Parse(values[1]);
			preProcessCode = int.Parse(values[2]);
			postProcessCode = int.Parse(values[3]);
			constraintCode = int.Parse(values[4]);
			skillType = int.Parse(values[5]);
			hpConsumption = int.Parse(values[6]);
			apConsumption = int.Parse(values[7]);
			sanConsumption = int.Parse(values[8]);
			aimmingType = int.Parse(values[9]);
			aimmingRange = int.Parse(values[10]);
			effectType = int.Parse(values[11]);
			effectRange = int.Parse(values[12]);
			recoveryWhether = int.Parse(values[13]) == 1;
			hpRecoveryValue = int.Parse(values[14]);
			apRecoveryValue = int.Parse(values[15]);
			sanRecoveryValue = int.Parse(values[16]);
			damage1 = new DamageInfo(int.Parse(values[17]) == 1, int.Parse(values[18]), int.Parse(values[19]));
			damage2 = new DamageInfo(int.Parse(values[20]) == 1, int.Parse(values[21]), int.Parse(values[22]));
			damage3 = new DamageInfo(int.Parse(values[23]) == 1, int.Parse(values[24]), int.Parse(values[25]));
			grantToken1 = new TokenInfo(int.Parse(values[27]), int.Parse(values[28]), int.Parse(values[26]));
			grantToken2	= new TokenInfo(int.Parse(values[30]), int.Parse(values[31]), int.Parse(values[29]));
			grantToken3 = new TokenInfo(int.Parse(values[33]), int.Parse(values[34]), int.Parse(values[32]));
			removeToken1 = new TokenInfo(int.Parse(values[36]), int.Parse(values[37]), int.Parse(values[35]));
			removeToken2 = new TokenInfo(int.Parse(values[39]), int.Parse(values[40]), int.Parse(values[38]));
			removeToken3 = new TokenInfo(int.Parse(values[42]), int.Parse(values[43]), int.Parse(values[41]));
			tileConditionCode = int.Parse(values[44]);
			tileConditionTurn = int.Parse(values[45]);
			addStatsCode = int.Parse(values[46]);
			mulStatsCode = int.Parse(values[47]);
		}

		public void SetTileCondition(TileConditionBase? tileCondition)
		{
			_tileCondition = tileCondition;
		}

		public void SetAddStats(StatsBase? stats)
		{
			_addStats = stats;
		}

		public void SetMulStats(StatsBase? stats)
		{
			_mulStats = stats;
		}
	}
}
