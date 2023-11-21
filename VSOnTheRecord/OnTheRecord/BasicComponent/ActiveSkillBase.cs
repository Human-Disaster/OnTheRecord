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
		private enum ParsNum
		{
			BaseCode,
			NameCode,
			PreProcessCode,
			PostProcessCode,
			ConstraintCode,
			SkillType,
			HpConsumption,
			ApConsumption,
			SanConsumption,
			AimmingType,
			AimmingRange,
			EffectType,
			EffectRange,
			EffectTargetType,
			RecoveryWhether,
			HpRecoveryValue,
			ApRecoveryValue,
			SanRecoveryValue,
			Damage1Whether,
			Damage1Value,
			Damage1Type,
			Damage2Whether,
			Damage2Value,
			Damage2Type,
			Damage3Whether,
			Damage3Value,
			Damage3Type,
			GrantToken1Code,
			GrantToken1Amount,
			GrantToken1Target,
			GrantToken2Code,
			GrantToken2Amount,
			GrantToken2Target,
			GrantToken3Code,
			GrantToken3Amount,
			GrantToken3Target,
			RemoveToken1Code,
			RemoveToken1Amount,
			RemoveToken1Target,
			RemoveToken2Code,
			RemoveToken2Amount,
			RemoveToken2Target,
			RemoveToken3Code,
			RemoveToken3Amount,
			RemoveToken3Target,
			TileConditionCode,
			TileConditionTurn,
			AddStatsCode,
			MulStatsCode
		}

		public readonly int nameCode;

		public readonly int preProcessCode;
		public readonly int postProcessCode;
		public readonly int constraintCode;

		public int GetCooltime()
		{
			if (constraintCode / 100000 % 10 != 2)
				return 0;
			else
				return constraintCode / 10000 % 10;
		}

		public int GetAvailableCount()
		{
			if (constraintCode / 100000 % 10 != 3)
				return 0;
			else
				return constraintCode / 10000 % 10;
		}

		public readonly int skillType;

		public readonly int hpConsumption;
		public readonly int apConsumption;
		public readonly int sanConsumption;

		public readonly int aimmingType;
		public readonly int aimmingRange;
		public readonly int effectType;
		public readonly int effectRange;

		public readonly int effectTargetType;

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
			nameCode = int.Parse(values[(int)ParsNum.NameCode]);
			preProcessCode = int.Parse(values[(int)ParsNum.PreProcessCode]);
			postProcessCode = int.Parse(values[(int)ParsNum.PostProcessCode]);
			constraintCode = int.Parse(values[(int)ParsNum.ConstraintCode]);
			skillType = int.Parse(values[(int)ParsNum.SkillType]);
			hpConsumption = int.Parse(values[(int)ParsNum.HpConsumption]);
			apConsumption = int.Parse(values[(int)ParsNum.ApConsumption]);
			sanConsumption = int.Parse(values[(int)ParsNum.SanConsumption]);
			aimmingType = int.Parse(values[(int)ParsNum.AimmingType]);
			aimmingRange = int.Parse(values[(int)ParsNum.AimmingRange]);
			effectType = int.Parse(values[(int)ParsNum.EffectType]);
			effectRange = int.Parse(values[(int)ParsNum.EffectRange]);
			effectTargetType = int.Parse(values[(int)ParsNum.EffectTargetType]);
			recoveryWhether = int.Parse(values[(int)ParsNum.RecoveryWhether]) == 1;
			hpRecoveryValue = int.Parse(values[(int)ParsNum.HpRecoveryValue]);
			apRecoveryValue = int.Parse(values[(int)ParsNum.ApRecoveryValue]);
			sanRecoveryValue = int.Parse(values[(int)ParsNum.SanRecoveryValue]);
			damage1 = new DamageInfo(int.Parse(values[(int)ParsNum.Damage1Whether]) == 1, int.Parse(values[(int)ParsNum.Damage1Type]), int.Parse(values[(int)ParsNum.Damage1Value]));
			damage2 = new DamageInfo(int.Parse(values[(int)ParsNum.Damage2Whether]) == 1, int.Parse(values[(int)ParsNum.Damage2Type]), int.Parse(values[(int)ParsNum.Damage2Value]));
			damage3 = new DamageInfo(int.Parse(values[(int)ParsNum.Damage3Whether]) == 1, int.Parse(values[(int)ParsNum.Damage3Type]), int.Parse(values[(int)ParsNum.Damage3Value]));
			grantToken1 = new TokenInfo(int.Parse(values[(int)ParsNum.GrantToken1Amount]), int.Parse(values[(int)ParsNum.GrantToken1Target]), int.Parse(values[(int)ParsNum.GrantToken1Code]));
			grantToken2 = new TokenInfo(int.Parse(values[(int)ParsNum.GrantToken2Amount]), int.Parse(values[(int)ParsNum.GrantToken2Target]), int.Parse(values[(int)ParsNum.GrantToken2Code]));
			grantToken3 = new TokenInfo(int.Parse(values[(int)ParsNum.GrantToken3Amount]), int.Parse(values[(int)ParsNum.GrantToken3Target]), int.Parse(values[(int)ParsNum.GrantToken3Code]));
			removeToken1 = new TokenInfo(int.Parse(values[(int)ParsNum.RemoveToken1Amount]), int.Parse(values[(int)ParsNum.RemoveToken1Target]), int.Parse(values[(int)ParsNum.RemoveToken1Code]));
			removeToken2 = new TokenInfo(int.Parse(values[(int)ParsNum.RemoveToken2Amount]), int.Parse(values[(int)ParsNum.RemoveToken2Target]), int.Parse(values[(int)ParsNum.RemoveToken2Code]));
			removeToken3 = new TokenInfo(int.Parse(values[(int)ParsNum.RemoveToken3Amount]), int.Parse(values[(int)ParsNum.RemoveToken3Target]), int.Parse(values[(int)ParsNum.RemoveToken3Code]));
			tileConditionCode = int.Parse(values[(int)ParsNum.TileConditionCode]);
			tileConditionTurn = int.Parse(values[(int)ParsNum.TileConditionTurn]);
			addStatsCode = int.Parse(values[(int)ParsNum.AddStatsCode]);
			mulStatsCode = int.Parse(values[(int)ParsNum.MulStatsCode]);
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
