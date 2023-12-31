﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

/*
* 각 Entity가 가지게 될 계산용 스탯
* 연산자 +, -, *, = 에 대해서 작동할 수 있을것
* 모든 속성의 끝에 stat의 S를 붙여 구분을 가능한 쉽게 할 것
* struct로 쓰고싶었는데 유니티 오류 -> class로 변경
*/
namespace OnTheRecord.BasicComponent
{
	public class CalStats
	{
		public float hpMaxS = 0;
		//개체가 가질 수 있는 체력이 최대치. 개체는 이 수치 이상의 체력을 보유할 수 없다.
		public float hpRS = 0;
		//개체가 자기 차례를 끝냈을 때 회복되는 체력
		public float sanMaxS = 0;
		//개체가 가질 수 있는 정신력의 최대치. 개체는 이 수치 이상의 정신력을 보유할 수 없다.
		public float sanRS = 0;
		//개체가 자기 차례를 끝냈을 때 회복되는 정신력
		public float apMaxS = 0;
		//개체가 가질 수 있는 행동력의 최대치. 개체는 이 수치 이상의 행동력을 보유할 수 없다.
		public float apRS = 0;
		//개체가 자기 차례를 끝냈을 때 회복되는 행동력
		public float spdS = 0;
		//속도, 턴이 시작되고 개체들의 차례를 정할 때 사용 속도가 높은 개체부터 낮은 개체 순으로 차례가 진행된다

		public float accS = 0;
		//명중률, 명중-회피 계산식에서 명중 수식에 사용된다
		public float dogS = 0;
		//회피, 명중-회피 계산식에서 공격을 회피하는 수식에 사용된다
		public float defTS = 0;
		//피해 감쇄, 해당 수치만큼 받은 피해를 감소시킨다
		public float defRS = 0;
		//피해 저항, 해당 백분율만큼 받은 피해를 감소시킨다
		public float atkS = 0;
		//공격력, 개체의 피해량 수식에 사용된다. 공격력, 개체의 피해량 수식에 사용된다.
		public float critS = 0;
		//치명타, 공격이 명중했을 때, 치명타로 적중될 확률
		public float critDS = 0;
		//치명타 피해, 치명타로 적중했을 때, 피해량 증폭에 영향을 주는 수치
		public float critRS = 0;
		//치명타 저항, 해당 수치는 치명타 피해 추산에서 받을 피해를 감소시키는 수식에 사용된다.

		public float resPhysicS = 0;
		public float resFlameS = 0;
		public float resFreezeS = 0;
		public float resElectricS = 0;
		public float resPoisonS = 0;
		public float resDiseaseS = 0;
		public float resChemicalS = 0;
		/*
		 * 저항치를 위한 스탯
		 * 전투지역으로 이동시 저 스탯을 기반으로 저항토큰 부여
		 */


		public CalStats()
		{
		}

		public CalStats(StatsBase? statsBase)
		{
			if (statsBase is not null)
			{
				this.hpMaxS = statsBase.hpMaxS;
				this.hpRS = statsBase.hpRS;
				this.sanMaxS = statsBase.sanMaxS;
				this.sanRS = statsBase.sanRS;
				this.apMaxS = statsBase.apMaxS;
				this.apRS = statsBase.apRS;
				this.spdS = statsBase.spdS;
				this.accS = statsBase.accS;
				this.dogS = statsBase.dogS;
				this.defTS = statsBase.defTS;
				this.defRS = statsBase.defRS;
				this.atkS = statsBase.atkS;
				this.critS = statsBase.critS;
				this.critDS = statsBase.critDS;
				this.critRS = statsBase.critRS;
				this.resPhysicS = statsBase.resPhysicS;
				this.resFlameS = statsBase.resFlameS;
				this.resFreezeS = statsBase.resFreezeS;
				this.resElectricS = statsBase.resElectricS;
				this.resPoisonS = statsBase.resPoisonS;
				this.resDiseaseS = statsBase.resDiseaseS;
				this.resChemicalS = statsBase.resChemicalS;
			}
		}

		public CalStats(int statsBaseCode)
		{
			StatsBase? statsBase = OnMemoryTable.Instance().GetStatsBase(statsBaseCode);
			if (statsBase is not null)
			{
				this.hpMaxS = statsBase.hpMaxS;
				this.hpRS = statsBase.hpRS;
				this.sanMaxS = statsBase.sanMaxS;
				this.sanRS = statsBase.sanRS;
				this.apMaxS = statsBase.apMaxS;
				this.apRS = statsBase.apRS;
				this.spdS = statsBase.spdS;
				this.accS = statsBase.accS;
				this.dogS = statsBase.dogS;
				this.defTS = statsBase.defTS;
				this.defRS = statsBase.defRS;
				this.atkS = statsBase.atkS;
				this.critS = statsBase.critS;
				this.critDS = statsBase.critDS;
				this.critRS = statsBase.critRS;
				this.resPhysicS = statsBase.resPhysicS;
				this.resFlameS = statsBase.resFlameS;
				this.resFreezeS = statsBase.resFreezeS;
				this.resElectricS = statsBase.resElectricS;
				this.resPoisonS = statsBase.resPoisonS;
				this.resDiseaseS = statsBase.resDiseaseS;
				this.resChemicalS = statsBase.resChemicalS;
			}
		}

		public CalStats(
			float hpMaxS,
			float hpRS,
			float sanMaxS,
			float sanRS,
			float apMaxS,
			float apRS,
			float spdS,
			float accS,
			float dogS,
			float defTS,
			float defRS,
			float atkS,
			float critS,
			float critDS,
			float critRS,
			float resPhysicS,
			float resFireS,
			float resIceS,
			float resLightningS,
			float resPoisonS,
			float resDiseaseS,
			float resAcidS
			)
		{
			this.hpMaxS = hpMaxS;
			this.hpRS = hpRS;
			this.sanMaxS = sanMaxS;
			this.sanRS = sanRS;
			this.apMaxS = apMaxS;
			this.apRS = apRS;
			this.spdS = spdS;
			this.accS = accS;
			this.dogS = dogS;
			this.defTS = defTS;
			this.defRS = defRS;
			this.atkS = atkS;
			this.critS = critS;
			this.critDS = critDS;
			this.critRS = critRS;
			this.resPhysicS = resPhysicS;
			this.resFlameS = resFireS;
			this.resFreezeS = resIceS;
			this.resElectricS = resLightningS;
			this.resPoisonS = resPoisonS;
			this.resDiseaseS = resDiseaseS;
			this.resChemicalS = resAcidS;
		}

		public static CalStats operator -(CalStats a)
			=> new CalStats(
				-a.hpMaxS,
				-a.hpRS,
				-a.sanMaxS,
				-a.sanRS,
				-a.apMaxS,
				-a.apRS,
				-a.spdS,
				-a.accS,
				-a.dogS,
				-a.defTS,
				-a.defRS,
				-a.atkS,
				-a.critS,
				-a.critDS,
				-a.critRS,
				-a.resPhysicS,
				-a.resFlameS,
				-a.resFreezeS,
				-a.resElectricS,
				-a.resPoisonS,
				-a.resDiseaseS,
				-a.resChemicalS
				);
		public static CalStats operator +(CalStats a, CalStats b)
			=> new CalStats(
				a.hpMaxS + b.hpMaxS,
				a.hpRS + b.hpRS,
				a.sanMaxS + b.sanMaxS,
				a.sanRS + b.sanRS,
				a.apMaxS + b.apMaxS,
				a.apRS + b.apRS,
				a.spdS + b.spdS,
				a.accS + b.accS,
				a.dogS + b.dogS,
				a.defTS + b.defTS,
				a.defRS + b.defRS,
				a.atkS + b.atkS,
				a.critS + b.critS,
				a.critDS + b.critDS,
				a.critRS + b.critRS,
				a.resPhysicS + b.resPhysicS,
				a.resFlameS + b.resFlameS,
				a.resFreezeS + b.resFreezeS,
				a.resElectricS + b.resElectricS,
				a.resPoisonS + b.resPoisonS,
				a.resDiseaseS + b.resDiseaseS,
				a.resChemicalS + b.resChemicalS
				);
		public static CalStats operator -(CalStats a, CalStats b) => a + (-b);
		public static CalStats operator *(CalStats a, CalStats b)
			=> new CalStats(
				a.hpMaxS * b.hpMaxS,
				a.hpRS * b.hpRS,
				a.sanMaxS * b.sanMaxS,
				a.sanRS * b.sanRS,
				a.apMaxS * b.apMaxS,
				a.apRS * b.apRS,
				a.spdS * b.spdS,
				a.accS * b.accS,
				a.dogS * b.dogS,
				a.defTS * b.defTS,
				a.defRS * b.defRS,
				a.atkS * b.atkS,
				a.critS * b.critS,
				a.critDS * b.critDS,
				a.critRS * b.critRS,
				a.resPhysicS * b.resPhysicS,
				a.resFlameS * b.resFlameS,
				a.resFreezeS * b.resFreezeS,
				a.resElectricS * b.resElectricS,
				a.resPoisonS * b.resPoisonS,
				a.resDiseaseS * b.resDiseaseS,
				a.resChemicalS * b.resChemicalS
				);
		public static CalStats operator *(CalStats a, int b)
			=> new CalStats(
				a.hpMaxS * b,
				a.hpRS * b,
				a.sanMaxS * b,
				a.sanRS * b,
				a.apMaxS * b,
				a.apRS * b,
				a.spdS * b,
				a.accS * b,
				a.dogS * b,
				a.defTS * b,
				a.defRS * b,
				a.atkS * b,
				a.critS * b,
				a.critDS * b,
				a.critRS * b,
				a.resPhysicS * b,
				a.resFlameS * b,
				a.resFreezeS * b,
				a.resElectricS * b,
				a.resPoisonS * b,
				a.resDiseaseS * b,
				a.resChemicalS * b
				);
		public static CalStats operator *(int a, CalStats b) => b * a;
		public static implicit operator CalStats(StatsBase a)
			=> new CalStats(a);
	}
}
