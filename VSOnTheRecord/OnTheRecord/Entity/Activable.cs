using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalStaticReference;
using OnTheRecord.BasicComponent;

namespace OnTheRecord.Entity
{
    class Activable : Breakable
    {
        public ActiveSkill?[] activeSkills;
        public PassiveSkillList passiveSkills;
        public float sanity;
        public float ap;
        public Activable(CalStats origin, int camp) : base(origin, camp)
        {
            activeSkills = Array.Empty<ActiveSkill?>();
            sanity = origin.sanMaxS;
            ap = origin.apMaxS;
        }

        public Activable(CalStats origin, int camp, int a) : base(origin, camp, a)
        {
            activeSkills = Array.Empty<ActiveSkill?>();
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
        }

        public void SubAp(float value)
        {
            ap -= value;
        }

        public void AddAp(float value)
        {
            ap += value;
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

        public void Situation(int situation)
        {
            switch (situation)
            {
            case (int)SituationCode.TurnEnd:

            }
        }
    }

}