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
    class Skill
    {
        //이건 필요한가?
    }

    class PassiveSkill : Skill
    {
        private PassiveSkillBase skillBase;

        public PassiveSkill(int skillCode)
        {
            skillBase = OnMemoryTable.Instance.GetPassiveSkillBase(skillCode);
        }

        // 스킬 자체가 가지고 있는 것들 (토큰X)
        /*
        public void Passive_trggrcheck(Stituation stituation)
        {
            // stituation을 체크해서 패시브를 on/off 동작 결정
        }
        */
        private void Passive_on() // to do 패시브 동작
        {

        }

        private void Passive_off()
        {

        }
    }

    class ActiveSkill : Skill
    {
        // 스킬 자체가 가지고 있는 것들 (토큰X)
        // 대부분 ActiveSkillBase에 들어가게 돼었으며 여기서 실질적으로 저장되야할 내용은 스킬 쿨타임, 이번턴의 남은 가용횟수 같은 것들
        ActiveSkillBase skillBase;
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

        public void AtkRoll(/*스킬 오브젝트*/ Activable attacker, Breakable defender)
        {
            if (/*flag 체크 ex (!skill.trueflight) &&*/ AccRoll(attacker))
                MissProcess(attacker, defender);
            else if (/*flag 체크 ex (!skill.trueflight) &&*/ DogRoll(defender))
                DogProcess(attacker, defender);
            else
                NormalProcess(attacker, defender);
        }

        private void MissProcess(Activable attacker, Breakable defender)
        {
            // todo 명중판정 실패, 명중판정 실패시 활성화 되는것 활성화
            //defender.AddToken(miss_list);
            //PassiveProcess(attacker, defender, SituationCode.Miss);
        }

        private void DogProcess(Activable attacker, Breakable defender)
        {
            // todo 회피판정 성공. 회피판정 실패시 활성화 되는것 활성화
            //PassiveProcess(attacker, defender, SituationCode.DoDoge);
        }

        private bool AccRoll(Activable attacker)
        {
            // todo 명중판정
            return false;
        }

        private bool DogRoll(Breakable defender)
        {
            // todo 회피판정
            return false;
        }

        private void NormalProcess(Activable attacker, Breakable defender)
        {
            // todo
            //DmgRoll(attacker, defender);
            //PassiveProcess(attacker, defender, SituationCode.DoHit);
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

        private bool CritRoll(Activable attacker, Activable defender/* 증폭시켜줄 temp_dmg 들을 매개변수로 받아올 것 */)
        {
            return false;
        }

        // 받는 Stituation은 Do_ 형식일것
        public void PassiveProcess(Activable activist, Activable passivist, int situation)
        {

            //activist.PassiveCheck(stituation, passivist);
            //passivist.PassiveCheck(stituation - 100, activist);

            /*
            
            Run_safety(); */
        }
    }

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

        public void AddPassiveSkill(PassiveSkill passiveSkill)
        {
            _passiveSkills.Add(passiveSkill);
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
    }
}
