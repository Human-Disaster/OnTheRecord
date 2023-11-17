using OnTheRecord.BasicComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnTheRecord.ExternalStaticReference;

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
		private StatsBase origin;
		private CalStats unchangeableStats;
		private CalStats sumStats;
		private CalStats secondMulStats;
		public CalStats finalStats;

		TokenList tokenList;
		
		public readonly int camp;
		public float hp;

		public Breakable(StatsBase origin, bool penetrateable, int camp)
		{
			this.penetrateable = penetrateable;
			this.origin = origin;
			this.camp = camp;
			unchangeableStats = CalUnchangeableStats();
			sumStats = CalSumStats();
			secondMulStats = CalSecondMulStats();
			finalStats = CalFinalStats();
			this.hp = finalStats.hpMaxS;
		}

		virtual protected CalStats CalUnchangeableStats()
		{
			return origin;
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

		public Breakable(StatsBase origin, bool penetrateable, int camp, TokenList tokenList)
		{
			this.penetrateable = penetrateable;
			this.origin = origin;
			this.camp = camp;
			this.tokenList = tokenList;
			unchangeableStats = CalUnchangeableStats();
			sumStats = CalSumStats();
			secondMulStats = CalSecondMulStats();
			finalStats = CalFinalStats();
			this.hp = finalStats.hpMaxS;
		}

		public void SubHp(float damage)
		{
			hp -= damage;
			if (hp <= 0)
				hp = 0;
		}

		public void AddHp(float heal)
		{
			hp += heal;
			if (hp > finalStats.hpMaxS)
				hp = finalStats.hpMaxS;
		}

		public float CalDamage(float damage, int damageType)
		{
			switch (damageType)
			{
				// DamageType 에 따른 처리 내용 정리되면 추가
				default:
					break;
			}
			return damage;
		}

		public void ChangeHp(float change)
		{
			if (change < 0)
				DamageHp(change);
			else
				HealHp(change);
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

		virtual public void TurnEnd()
		{
			// 비활성 토큰 추가
			tokenList.Add();
			// 활성 토큰 제거
			tokenList.Remove();
			// 턴 종료 시츄에이션
			// Passive 스킬이 있는 개체는 먼저 
			tokenList.Situation(, this);
		}
	}
}
