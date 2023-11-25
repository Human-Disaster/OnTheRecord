using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnTheRecord.Entity;

namespace OnTheRecord.BasicComponent
{
	public class PassiveSkillList
	{
		private List<PassiveSkill> _passiveSkills;
		private CalStats passiveSkillStats;

		public PassiveSkillList()
		{
			_passiveSkills = new List<PassiveSkill>();
			CalculateStats();
		}

		public PassiveSkillList(List<PassiveSkill> passiveSkills)
		{
			_passiveSkills = passiveSkills;
			CalculateStats();
		}

		public PassiveSkillList(PassiveSkillList passiveSkillList)
		{
			_passiveSkills = passiveSkillList._passiveSkills;
			CalculateStats();
		}

		public PassiveSkillList(PassiveSkill[] passiveSkills)
		{
			_passiveSkills = passiveSkills.ToList();
			CalculateStats();
		}

		public PassiveSkillList(int[] passiveSkillCodes)
		{
			_passiveSkills = new List<PassiveSkill>();
			foreach (int passiveSkillCode in passiveSkillCodes)
			{
				_passiveSkills.Add(new PassiveSkill(passiveSkillCode));
			}
			CalculateStats();
		}

		public PassiveSkillList(int passiveSkillCode)
		{
			_passiveSkills = new List<PassiveSkill>();
			_passiveSkills.Add(new PassiveSkill(passiveSkillCode));
			CalculateStats();
		}

		public void AddPassiveSkill(PassiveSkill passiveSkill)
		{
			_passiveSkills.Add(passiveSkill);
			CalculateStats();
		}

		private void CalculateStats()
		{
			passiveSkillStats = new CalStats();
			foreach (PassiveSkill passiveSkill in _passiveSkills)
			{
				//passiveSkillStats += passiveSkill.skillBase.GetCalStats();
			}
		}

		public CalStats GetPassiveStats()
		{
			return passiveSkillStats;
		}

		public void Situation(int situation, Activable activable)
		{
			foreach (PassiveSkill passiveSkill in _passiveSkills)
			{
				//todo
				//passiveSkill.Passive_trggrcheck(situation);
			}
		}
	}
}
