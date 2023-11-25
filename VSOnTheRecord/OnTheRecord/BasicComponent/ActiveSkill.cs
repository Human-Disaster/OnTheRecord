using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OnTheRecord.Entity;
using OnTheRecord.BasicComponent;
using ExternalStaticReference;

/*
  미완 이런 느낌이다 참고만 할 것
*/

namespace OnTheRecord.BasicComponent
{
	public class ActiveSkill
	{
		// 스킬 자체가 가지고 있는 것들 (토큰X)
		// 대부분 ActiveSkillBase에 들어가게 돼었으며 여기서 실질적으로 저장되야할 내용은 스킬 쿨타임, 이번턴의 남은 가용횟수 같은 것들
		public readonly ActiveSkillBase skillBase;
		public int cooltime;
		public int availableCount;

		public ActiveSkill(ActiveSkillBase skillBase)
		{
			this.skillBase = skillBase;
			cooltime = 0;
			if (skillBase.GetAvailableCount() != 0)
				availableCount = skillBase.GetAvailableCount();
			else
				availableCount = 1;
		}

		public ActiveSkill(int skillCode)
		{
			skillBase = OnMemoryTable.Instance().GetActiveSkillBase(skillCode);
			cooltime = 0;
			if (skillBase.GetAvailableCount() != 0)
				availableCount = skillBase.GetAvailableCount();
			else
				availableCount = 1;
		}

		public void TurnEnd()
		{
			if (cooltime != 0)
				cooltime--;
			if (skillBase.GetAvailableCount() != 0)
				availableCount = skillBase.GetAvailableCount();
			else
				availableCount = 1;
		}

		// -1 : 미스
		// -2 : 회피
		// -3 : 불가
		public int AtkRoll(/*스킬 오브젝트*/ Activable attacker, Breakable defender)
		{
			if (cooltime != 0 || availableCount == 0)
				return -3;
			else if (skillBase.GetAvailableCount() != 0)
				availableCount--;
			else if (skillBase.GetCooltime() != 0)
				cooltime = skillBase.GetCooltime();
			// todo 스킬 사용
			int skillType = skillBase.skillType;
			if (!(skillType / 100 % 100 == (int)SkillTypePreCode.True
				|| skillType % 100 == (int)SkillTypePostCode.Nontarget
				|| skillType % 100 == (int)SkillTypePostCode.Area
				|| skillType % 100 == (int)SkillTypePostCode.Floor))
			{
				if (!AccRoll(attacker))
				{
					//MissProcess(attacker, defender);
					return -1;
				}
				else if (DogRoll(defender))
				{
					//DogProcess(attacker, defender);
					return -2;
				}
			}
			return NormalProcess(attacker, defender);
		}

		private void MissProcess(Activable attacker, Breakable defender)
		{
			// todo 명중판정 실패, 명중판정 실패시 활성화 되는것 활성화
			//defender.AddToken(miss_list);
			//PassiveProcess(attacker, defender, SituationCode.Miss);
		}

		private void DogProcess(Activable attacker, Breakable defender)
		{

		}

		private bool AccRoll(Activable attacker)
		{
			float acc = attacker.finalStats.accS;
			if (acc > 94F)
				acc = 94F;
			else if (acc < 3F)
				acc = 3F;
			acc /= 100F;
			return acc > Checkrand.Checkrand_float(0F, 1F);
		}

		private bool DogRoll(Breakable defender)
		{
			float dog = defender.finalStats.dogS;
			if (dog > 75F)
				dog = 75F;
			else if (dog < 1.7F)
				dog = 1.7F;
			dog /= 100F;
			return dog > Checkrand.Checkrand_float(0F, 1F);
		}


		private int NormalProcess(Activable attacker, Breakable defender)
		{
			int result = 1;
			float dmg = attacker.finalStats.atkS;
			dmg *= (55F + 2.5F * Checkrand.Checkrand_float(15F, 20F)) / 10F;
			if (CritRoll(attacker, defender))
			{
				dmg += CritDamage(attacker);
				result = 2;
			}
			dmg = defender.CalDamage(dmg, skillBase.damage1.damageType);
			defender.ChangeHp(-dmg);
			return result;
		}

		private void DmgRoll(Activable attacker, Activable defender)
		{
		}


		/* 원 버전 데미지 판정 함수
		 private void DmgRoll()
		{
			if (CritRoll())
				;
			DRRoll();
			DTRoll();
			DmgFinalize();
		}*/

		private bool CritRoll(Activable attacker, Breakable defender)
		{
			float crit = attacker.finalStats.critS * defender.finalStats.critRS;
			return crit > Checkrand.Checkrand_float(0F, 1F);
		}

		private float CritDamage(Activable attacker)
		{
			return attacker.finalStats.atkS * attacker.finalStats.critDS / 100F;
		}
	}

}
