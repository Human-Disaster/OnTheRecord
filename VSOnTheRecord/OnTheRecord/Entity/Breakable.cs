using OnTheRecord.BasicComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalStaticReference;

/*
 * finalStats 최종 스테이터스
 * = {(클래스 기본 스테이터스 * 전체 토큰 1차 곱 보정치) + 전체 합 보정치 } * (전체 2차 곱 보정치)
 * unchangeableStats 클래스 기본 스테이터스
 * sumStats 전체 합 보정치
 * = 전체 토큰 합 보정치 + 전체 장비 합 보정치
 * secondMulStats 전체 2차 곱 보정치
 * = 전체 토큰 2차 곱 보정치 + 전체 장비 곱 보정치
 */
namespace OnTheRecord.Entity
{

	public class Breakable : Entity
	{
		private CalStats unchangeableStats;
		private CalStats sumStats;
		private CalStats secondMulStats;
		public CalStats finalStats;

		public TokenList tokenList;
		
		public readonly int camp;
		public float hp;

		public Breakable(CalStats origin, int camp) : base(false)
		{
			this.camp = camp;
			unchangeableStats = origin;
			sumStats = CalSumStats();
			secondMulStats = CalSecondMulStats();
			finalStats = CalFinalStats();
			this.hp = finalStats.hpMaxS;
			tokenList = new TokenList();
		}

		private void AddResistToken()
		{
			tokenList.Add(new Token((int)TokenCode.ResistPhysic, CalResistToken(finalStats.resPhysicS)));
			tokenList.Add(new Token((int)TokenCode.ResistFlame, CalResistToken(finalStats.resFlameS)));
			tokenList.Add(new Token((int)TokenCode.ResistFreeze, CalResistToken(finalStats.resFreezeS)));
			tokenList.Add(new Token((int)TokenCode.ResistElectric, CalResistToken(finalStats.resElectricS)));
			tokenList.Add(new Token((int)TokenCode.ResistPoison, CalResistToken(finalStats.resPoisonS)));
			tokenList.Add(new Token((int)TokenCode.ResistDisease, CalResistToken(finalStats.resDiseaseS)));
			tokenList.Add(new Token((int)TokenCode.ResistChemical, CalResistToken(finalStats.resChemicalS)));
		}

		private int CalResistToken(float resist)
		{
			switch (resist)
			{
				case < (float)DamageResistanceRange.m5:
					return (int)0;
				case >= (float)DamageResistanceRange.m5 and < (float)DamageResistanceRange.m4:
					return (int)1;
				case >= (float)DamageResistanceRange.m4 and < (float)DamageResistanceRange.m3:
					return (int)2;
				case >= (float)DamageResistanceRange.m3 and < (float)DamageResistanceRange.m2:
					return (int)3;
				case >= (float)DamageResistanceRange.m2 and < (float)DamageResistanceRange.m1:
					return (int)4;
				case >= (float)DamageResistanceRange.m1 and < (float)DamageResistanceRange.z:
					return (int)5;
				case >= (float)DamageResistanceRange.z and < (float)DamageResistanceRange.p1:
					return (int)6;
				case >= (float)DamageResistanceRange.p1 and < (float)DamageResistanceRange.p2:
					return (int)7;
				case >= (float)DamageResistanceRange.p2 and < (float)DamageResistanceRange.p3:
					return (int)8;
				case >= (float)DamageResistanceRange.p3 and < (float)DamageResistanceRange.p4:
					return (int)9;
				case >= (float)DamageResistanceRange.p4 and < (float)DamageResistanceRange.infinity:
					return (int)10;
				default:
					return (int)999;
			}
		}


		virtual protected CalStats CalUnchangeableStats()
		{
			return unchangeableStats;
		}

		protected CalStats CalSumStats()
		{
			return tokenList.sumTokenStats;
		}

		protected CalStats CalSecondMulStats()
		{
			return tokenList.secondMulTokenStats;
		}

		protected CalStats CalFinalStats()
		{
			return (unchangeableStats + sumStats) * secondMulStats;
		}

		public Breakable(StatsBase origin, bool penetrateable, int camp, TokenList tokenList) : base(penetrateable)
		{
			this.camp = camp;
			this.tokenList = tokenList;
			unchangeableStats = origin;
			sumStats = CalSumStats();
			secondMulStats = CalSecondMulStats();
			finalStats = CalFinalStats();
			this.hp = finalStats.hpMaxS;
		}

		public void SubHp(float damage)
		{
			hp -= damage;
		}

		public void AddHp(float heal)
		{
			hp += heal;
			if (hp > finalStats.hpMaxS)
				hp = finalStats.hpMaxS;
		}

		public float CalDamage(float damage, int damageType)
		{
			int resistStack;
			switch (damageType)
			{
				// DamageType 에 따른 처리 내용 정리되면 추가
				case (int)DamageType.impact:
				case (int)DamageType.slash:
				case (int)DamageType.pierce:
				case (int)DamageType.shot:
				case (int)DamageType.explo:
					resistStack = tokenList.GetTokenStack((int)TokenCode.ResistPhysic);
					if (resistStack > 11)
						resistStack = 11;
					damage *= DamageResistanceValue.common[resistStack];
					break;
				case (int)DamageType.flame:
					resistStack = tokenList.GetTokenStack((int)TokenCode.ResistFlame);
					if (resistStack > 11)
						resistStack = 11;
					damage *= DamageResistanceValue.common[resistStack];
					break;
				case (int)DamageType.freeze:
					resistStack = tokenList.GetTokenStack((int)TokenCode.ResistFreeze);
					if (resistStack > 11)
						resistStack = 11;
					damage *= DamageResistanceValue.common[resistStack] / 2F;
					break;
				case (int)DamageType.electric:
					resistStack = tokenList.GetTokenStack((int)TokenCode.ResistElectric);
					if (resistStack > 11)
						resistStack = 11;
					damage *= DamageResistanceValue.common[resistStack];
					break;
				case (int)DamageType.poison:
					resistStack = tokenList.GetTokenStack((int)TokenCode.ResistPoison);
					if (resistStack > 11)
						resistStack = 11;
					damage *= DamageResistanceValue.common[resistStack];
					break;
				case (int)DamageType.chemical:
					resistStack = tokenList.GetTokenStack((int)TokenCode.ResistChemical);
					if (resistStack > 11)
						resistStack = 11;
					damage *= DamageResistanceValue.common[resistStack];
					break;
				case (int)DamageType.disease:
					resistStack = tokenList.GetTokenStack((int)TokenCode.ResistDisease);
					if (resistStack > 11)
						resistStack = 11;
					damage *= DamageResistanceValue.common[resistStack];
					break;
				default:
					break;
			}
			return damage;
		}

		public void ChangeHp(float change)
		{
			if (change < 0)
				SubHp(change);
			else
				AddHp(change);
		}

		virtual public void AddToken(Token t)
		{
			tokenList.Add(t);
			sumStats = CalSumStats();
			secondMulStats = CalSecondMulStats();
			finalStats = CalFinalStats();
			if (hp > finalStats.hpMaxS)
				hp = finalStats.hpMaxS;
		}

		virtual public void Situation(int situation)
		{
			// Passive 스킬이 있는 개체는 Passive 처리 먼저 
			tokenList.Situation(situation, this);
		}

		virtual public void TurnEnd()
		{
			// 비활성 토큰 추가
			tokenList.AddWithoutCalStats((int)TokenCode.Inert);
			// 활성 토큰 제거
			tokenList.RemoveWithoutCalStats((int)TokenCode.NonInert);
			// 턴 종료 시츄에이션
			Situation((int)SituationCode.endTurn);
		}

		virtual public void TurnStart()
		{
			// 활성 토큰 추가
			tokenList.AddWithoutCalStats((int)TokenCode.NonInert);
			// 비활성 토큰 제거
			tokenList.RemoveWithoutCalStats((int)TokenCode.Inert);
			// 턴 시작 시츄에이션
			Situation((int)SituationCode.startTurn);
		}

	}
}
