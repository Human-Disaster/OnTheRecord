using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalStaticReference;
using OnTheRecord.BasicComponent;
using OnTheRecord.Map;

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
			NotAvailable = -2,
			NullError = -1,
			Success = 0,
		}

		public int UseSkill(int skillIndex, Breakable[] targets, Tile targetTile)
		{
			if (activeSkills.Length > skillIndex && !(activeSkills[skillIndex] is null))
			{
				return UseSkill(activeSkills[skillIndex], targets, targetTile);
			}
			else
				return (int)SkillUseResult.NullError;
		}

		public int UseSkill(ActiveSkill activeSkill, Breakable[] targets, Tile targetTile)
		{
			if (activeSkill is null)
				return (int)SkillUseResult.NullError;
			if (!activeSkill.IsAvailable(this))
				return (int)SkillUseResult.NotAvailable;
			activeSkill.AtkRoll(this, targets, targetTile);
			return (int)SkillUseResult.Success;
		}

		public bool IsDead()
		{
			return hp <= 0;
		}
	}
}