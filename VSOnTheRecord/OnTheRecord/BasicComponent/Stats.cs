using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  미완 이런 느낌이다 참고만 할 것
*/
namespace OnTheRecord.BasicComponent
{
	struct Stats
	{
		public float hpMax = 0;
		//개체가 가질 수 있는 체력이 최대치. 개체는 이 수치 이상의 체력을 보유할 수 없다.
		public float hpR = 0;
		//개체가 자기 차례를 끝냈을 때 회복되는 체력
		public float apMax = 0;
		//개체가 가질 수 있는 행동력의 최대치. 개체는 이 수치 이상의 행동력을 보유할 수 없다.
		public float apR = 0;
		//개체가 자기 차례를 끝냈을 때 회복되는 행동력
		public float spd = 0;
		//속도, 턴이 시작되고 개체들의 차례를 정할 때 사용 속도가 높은 개체부터 낮은 개체 순으로 차례가 진행된다

		public float acc = 0;
		//명중률, 명중-회피 계산식에서 명중 수식에 사용된다
		public float dog = 0;
		//회피, 명중-회피 계산식에서 공격을 회피하는 수식에 사용된다
		public float defT = 0;
		//피해 감쇄, 해당 수치만큼 받은 피해를 감소시킨다
		public float defR = 0;
		//피해 저항, 해당 백분율만큼 받은 피해를 감소시킨다
		public float atk = 0;
		//공격력, 개체의 피해량 수식에 사용된다.공격력, 개체의 피해량 수식에 사용된다.
		public float crit = 0;
		//치명타, 공격이 명중했을 때, 치명타로 적중될 확률
		public float critD = 0;
		//치명타 피해, 치명타로 적중했을 때, 피해량 증폭에 영향을 주는 수치
		public float critR = 0;
		//치명타 저항, 해당 수치는 치명타 피해 추산에서 받을 피해를 감소시키는 수식에 사용된다.

		
		Stats()
		{
		}
	}
}
