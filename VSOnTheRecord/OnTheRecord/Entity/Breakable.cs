using OnTheRecord.BasicComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		
		float hp;

		public Breakable(StatsBase origin, bool penetrateable)
		{
			this.penetrateable = penetrateable;
			this.origin = origin;
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

		public Breakable(StatsBase origin, bool penetrateable, TokenList tokenList)
		{
			this.penetrateable = penetrateable;
			this.origin = origin;
			this.tokenList = tokenList;
			unchangeableStats = CalUnchangeableStats();
			sumStats = CalSumStats();
			secondMulStats = CalSecondMulStats();
			finalStats = CalFinalStats();
			this.hp = finalStats.hpMaxS;
		}

		public void DamageHp(float damage)
		{
			hp -= damage;
			if (hp <= 0)
				hp = 0;
		}

		public void HealHp(float heal)
		{
			hp += heal;
			if (hp > finalStats.hpMaxS)
				hp = finalStats.hpMaxS;
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
			//
		}
	}
}
