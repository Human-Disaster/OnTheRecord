using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using machanism_test.Entity;
using machanism_test.NewFolder;

namespace machanism_test
{
    class Dmg_function
    {
        /*
         *  dmg로 취급할 수 있는 모든 것 처리
         *  내성 처리가 필요한 내용은 DR DT 적용
         *  이후 모든 dmg를 DmgFnalize에 적용
         *  방어력 감쇄, 관통 매커니즘 추가 시 Activable attcker 추가해서 스탯을 가져와 판정 할 것
         *  속성저항 있으면 DR에 추가해서 만들것
         */

        public void Apply_damage(float phys_d, int elem_type, float elem_d, float san_d, Activable defender)
        {
            DRRooll();
            DTRoll();
            DmgFinalize();
        }

        private void DRRoll(/*float defn_dr*/)
        {
            // dmg -= dmg * (defen_dr / 100);
        }

        private void DTRoll(/*float defn_dt*/)
        {
            // dmg -= dmg - defn_dt
        }

        private void DmgFinalize(int dmg)
        {
            getDmg();
        }
    }
}
