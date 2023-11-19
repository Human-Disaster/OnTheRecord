﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OnTheRecord.Entity;

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
        // need PassiveSkillBase

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
            if (skillBase.availableCount != 0)
                availableCount = skillBase.availableCount;
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
            defender.GrantToken(miss_list);
            PassiveProcess(attacker, defender, Stituation.Do_miss);
        }

        private void DogProcess(Activable attacker, Breakable defender)
        {
            // todo 회피판정 성공. 회피판정 실패시 활성화 되는것 활성화
            PassiveProcess(attacker, defender, Stituation.Do_doge);
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

        private void NormalProcess(Activable attacker, Activable defender)
        {
            // todo
            DmgRoll(defender);
            PassiveProcess(attacker, defender, Stituation.Do_hit);
        }

        private void DmgRoll(Activable attacker, Activable defender)
        {
            float phys_temp_dmg = phys_dmg;
            float elem_temp_dmg = elements_dmg;
            float san_temp_dmg = sanity_dmg;
            if (CritRoll())

                Dmg_function.Apply_damage();
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

        }

        // 받는 Stituation은 Do_ 형식일것
        public void PassiveProcess(Activable activist, Activable passivist, Stituation stituation)
        {

            activist.PassiveCheck(stituation, passivist);
            passivist.PassiveCheck(stituation - 100, activist);

            /*
            
            Run_safety(); */
        }
    }

    class SkillArea
    {
        int active_type; // 스마이트, 위치지정,
        List<List<int>> alive_area; // 스킬 발동가능 범위
        List<List<int>> act_effect_area; // 발동된 스킬의 효과 범위
    }

    class SkillEffect
    {
        int movex;
        int movey;
    }

    public class SkillList
    {
        List<Skill> act_list = new List<Skill>();
        List<Skill> pas_list = new List<Skill>();

        public void Sort()
        {
            token_list.Sort(delegate (Skill x, Skill y)
            {
                if (x.TokenType == 0 && y.TokenType == 0) return 0; // to do https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.list-1?view=net-6.0 참고 
                else if (x.TokenType == 0) return -1;
                else if (y.TokenType == 0) return 1;
                else return x.TokenType.CompareTo(y.TokenType);
            });
        }

        public void PasiveCheck(Stituation stituation, Activable self, Activable other)
        {
            // to do 리스트에 트리거 체크로 패시브를 확인한 뒤 self에게 토큰을 부여하는지, other에게 부여하는지, self가 피격됬을때 other가 가지는지 확인함
        }

        /* parts.Sort(delegate (Part x, Part y)
       {
           if (x.PartName == null && y.PartName == null) return 0;
           else if (x.PartName == null) return -1;
           else if (y.PartName == null) return 1;
           else return x.PartName.CompareTo(y.PartName);
       }*/

        public void Find_harm()
        {

        }

    }
}
