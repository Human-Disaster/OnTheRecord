using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalStaticReference;
using OnTheRecord.BasicComponent;

namespace OnTheRecord.Entity
{
	public class Activable : Breakable
	{
		public ActiveSkill?[] activeSkills;
		public PassiveSkillList passiveSkills;
		public float sanity;
		public float ap;
		public Activable(CalStats origin, int camp) : base(origin, camp)
		{
			activeSkills = Array.Empty<ActiveSkill?>();
			passiveSkills = new PassiveSkillList();
			sanity = origin.sanMaxS;
			ap = origin.apMaxS;
		}

		public Activable(CalStats origin, int camp, int a) : base(origin, camp, a)
		{
			activeSkills = Array.Empty<ActiveSkill?>();
			passiveSkills = new PassiveSkillList();
			sanity = origin.sanMaxS;
			ap = origin.apMaxS;
		}

		public void SetActiveSkills(ActiveSkill?[] activeSkills)
		{
			this.activeSkills = activeSkills;
		}

		public void SubSanity(float value)
		{
			sanity -= value;
		}

		public void AddSanity(float value)
		{
			sanity += value;
			if (sanity > finalStats.sanMaxS)
				sanity = finalStats.sanMaxS;
		}

		public void ChangeSanity(float value)
		{
			if (value < 0)
				SubSanity(-value);
			else
				AddSanity(value);
		}

		public void SubAp(float value)
		{
			ap -= value;
		}

		public void AddAp(float value)
		{
			ap += value;
			if (ap > finalStats.apMaxS)
				ap = finalStats.apMaxS;
		}

		public void ChangeAp(float value)
		{
			if (value < 0)
				SubAp(-value);
			else
				AddAp(value);
		}

		public override void ChangeFinalStats()
		{
			base.ChangeFinalStats();
			if (sanity > finalStats.sanMaxS)
				sanity = finalStats.sanMaxS;
			if (ap > finalStats.apMaxS)
				ap = finalStats.apMaxS;
		}

		public void AddConcealment(int value)
		{
			if (value < 0)
				AddExposure(-value);
			int stack = tokenList.GetTokenStack((int)TokenCode.Exposure);
			if (stack == 0)
				tokenList.Add(new Token((int)TokenCode.Concealment, value));
			else if (stack < value)
			{
				tokenList.Remove(new Token((int)TokenCode.Exposure, stack));
				tokenList.Add(new Token((int)TokenCode.Concealment, value - stack));
			}
			else
				tokenList.Remove(new Token((int)TokenCode.Exposure, value));
		}

		public void AddExposure(int value)
		{
			if (value < 0)
				AddConcealment(-value);
			int stack = tokenList.GetTokenStack((int)TokenCode.Concealment);
			if (stack == 0)
				tokenList.Add(new Token((int)TokenCode.Exposure, value));
			else if (stack < value)
			{
				tokenList.Remove(new Token((int)TokenCode.Concealment, stack));
				tokenList.Add(new Token((int)TokenCode.Exposure, value - stack));
			}
			else
				tokenList.Remove(new Token((int)TokenCode.Concealment, value));
		}

		public override void Situation(int situation)
		{
			switch (situation)
			{
				case (int)SituationCode.endTurn:
					foreach (ActiveSkill? activeSkill in activeSkills)
					{
						if (activeSkill is not null)
							activeSkill.TurnEnd();
					}
					TurnEnd();
					break;
				case (int)SituationCode.startTurn:
					TurnStart();
					break;
			}
			passiveSkills.Situation(situation, this);
			tokenList.Situation(situation, this);
		}

		public override void TurnEnd()
		{
			base.TurnEnd();
			AddAp(finalStats.apRS);
			foreach (ActiveSkill? activeSkill in activeSkills)
			{
				if (activeSkill is not null)
					activeSkill.TurnEnd();
			}
		}

		public override void TurnStart()
		{
			base.TurnStart();
			AddSanity(finalStats.sanRS);
		}

		public enum SkillUseResult
		{
			NotEnoughHp = -4,
			NotEnoughAp = -3,
			NotEnoughSan = -2,
			NullError = -1,
			Success = 0,
		}

		public int UseSkill(int skillIndex, Breakable[] targets)
		{
			if (activeSkills.Length > skillIndex && !(activeSkills[skillIndex] is null))
			{
				return UseSkill(activeSkills[skillIndex], targets);
			}
			else
				return (int)SkillUseResult.NullError;
		}

		public int UseSkill(ActiveSkill activeSkill, Breakable[] targets)
		{
			if (activeSkill is not null)
			{
				if (ap < -activeSkill.skillBase.apConsumption)
					return (int)SkillUseResult.NotEnoughAp;
				else if (sanity < -activeSkill.skillBase.sanConsumption)
					return (int)SkillUseResult.NotEnoughSan;
				else if (hp < -activeSkill.skillBase.hpConsumption)
					return (int)SkillUseResult.NotEnoughHp;
				ChangeAp(activeSkill.skillBase.apConsumption);
				ChangeSanity(activeSkill.skillBase.sanConsumption);
				ChangeHp(activeSkill.skillBase.hpConsumption);
				//확정
				foreach (Breakable target in targets)
				{
					if (activeSkill.skillBase.recoveryWhether)
					{
						target.AddHp(activeSkill.skillBase.hpRecoveryValue);
						if (!(target as Activable is null))
						{
							((Activable)target).AddAp(activeSkill.skillBase.apRecoveryValue);
							((Activable)target).AddSanity(activeSkill.skillBase.sanRecoveryValue);
						}
					}
					target.tokenList.Add(activeSkill.skillBase.grantToken1);
					target.tokenList.Add(activeSkill.skillBase.grantToken2);
					target.tokenList.Add(activeSkill.skillBase.grantToken3);
					target.tokenList.Remove(activeSkill.skillBase.removeToken1);
					target.tokenList.Remove(activeSkill.skillBase.removeToken2);
					target.tokenList.Remove(activeSkill.skillBase.removeToken3);
				}
				//불확정
				if (activeSkill.skillBase.damage1.damageWhether
					|| activeSkill.skillBase.damage2.damageWhether
					|| activeSkill.skillBase.damage3.damageWhether)
				{
					foreach (Breakable target in targets)
					{
						activeSkill.AtkRoll(this, target);
					}
				}
				return (int)SkillUseResult.Success;
			}
			else
				return (int)SkillUseResult.NullError;
		}

		public bool IsDead()
		{
			return hp <= 0;
		}
	}
}