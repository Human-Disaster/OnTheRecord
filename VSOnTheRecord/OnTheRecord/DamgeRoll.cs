using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using machanism_test.Basic_Component;
using machanism_test.Entity;
using machanism_test.NewFolder;
using machanism_test.NewFolder1;

namespace machanism_test
{
    public class DamageRoll
    {
        public float dmg = 0;

        Active_Skill apply_skill = 0;

        public bool Aacc = false;
        public bool Acrit = false;
        public bool Adog = false;

        public bool getAacc() { return Aacc; }
        public bool getcrit() { return Acrit; }
        public bool getadog() { return Adog; }
        public float getDmg() { return dmg; }

        public void AtkRoll(/*스킬 오브젝트*/ Activable attacker, Activable defender, Active_Skill skill)
        {
            apply_skill = skill;
            if (/*flag 체크 ex (!skill.trueflight) &&*/ AccRoll(attacker))
                MissProcess(attacker, defender);
            else if (/*flag 체크 ex (!skill.trueflight) &&*/ DogRoll(defender))
                DogProcess(attacker, defender);
            else
                NormalProcess(attacker, defender);
        }

        private void MissProcess(Activable attacker, Activable defender)
        {
            // todo 명중판정 실패, 명중판정 실패시 활성화 되는것 활성화
            defender.GrantToken(apply_skill.miss_list);
            PassiveProcess(attacker, defender, Stituation.Do_miss);
        }

        private void DogProcess(Activable attacker, Activable defender)
        {
            // todo 회피판정 성공. 회피판정 실패시 활성화 되는것 활성화
            PassiveProcess(attacker, defender, Stituation.Do_doge);
        }

        private bool AccRoll(Activable attacker)
        {
            // todo 명중판정
            return false;
        }

        private bool DogRoll(Activable defender)
        {
            // todo 회피판정
            return false;
        }

        private void NormalProcess(Activable attacker, Activable defender)
        {
            // todo
            DmgRoll();
            PassiveProcess(attacker, defender, Stituation.Do_hit);
        }

        private void DmgRoll()
        {
            
            if (CritRoll())
                dmg *= 2  /* 치명타 가중치*/ ;
            DRRoll();
            DTRoll();
            DmgFinalize();
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

        private bool CritRoll(/* 공격자 스탯 넣을것, 상위에서 공격자 방어자 구분 넣을것*/)
        {
            
        }
        
        public void DRRoll(/*float defn_dr*/)
        {
           // dmg -= dmg * (defen_dr / 100);
        }

        public void DTRoll(/*float defn_dt*/)
        {
            // dmg -= dmg - defn_dt
        }

        public void DmgFinalize(int dmg)
        {
            getDmg();
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


}
