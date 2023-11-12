using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


/*
 * OnMemoryTable에 올려두고 필요할 때 _statsCode로 부터 참조하여 Stats를 만들기 위한 클래스
 * scv 파일의 한 줄을 이 StatsBase 인스턴스 하나로 변경할 것이니 생성자는 기본적으로 string 사용
 * OnMemoryTable에서 파싱해서 건내줄 수도 있...나??? 이 경우는 일단 제외
 * 스탯을 가지는 모든 클래스가 참조할 기본 스탯	클래스
 * StatsBase를 이용해서 계산을 하는 경우 CalStats 클래스로 변환된다.
 * 연산자 오버로딩을 통해 +, -, *에 대해서 StatsBase와 StatsBase, StatsBase와 CalStats, CalStats와 CalStats의 연산이 가능하다.
 */
namespace OnTheRecord.BasicComponent
{
	public class StatsBase : Base
	{
		public readonly float hpMaxS = 0;
		public readonly float hpRS = 0;
		public readonly float sanMaxS = 0;
		public readonly float sanRS = 0;
		public readonly float apMaxS = 0;
		public readonly float apRS = 0;
		public readonly float spdS = 0;
		public readonly float accS = 0;
		public readonly float dogS = 0;
		public readonly float defTS = 0;
		public readonly float defRS = 0;
		public readonly float atkS = 0;
		public readonly float critS = 0;
		public readonly float critDS = 0;
		public readonly float critRS = 0;
		public readonly float resPhysicS = 0;
		public readonly float resFireS = 0;
		public readonly float resIceS = 0;
		public readonly float resLightningS = 0;
		public readonly float resPoisonS = 0;
		public readonly float resDiseaseS = 0;
		public readonly float resAcidS = 0;


		public StatsBase(string str) : base(str)
		{
			string[] values = Parse(str);
			hpMaxS = base.BaseFloatParse(values[1]);
			hpRS = base.BaseFloatParse(values[2]);
			sanMaxS = base.BaseFloatParse(values[3]);
			sanRS = base.BaseFloatParse(values[4]);
			apMaxS = base.BaseFloatParse(values[5]);
			apRS = base.BaseFloatParse(values[6]);
			spdS = base.BaseFloatParse(values[7]);
			accS = base.BaseFloatParse(values[8]);
			dogS = base.BaseFloatParse(values[9]);
			defTS = base.BaseFloatParse(values[10]);
			defRS = base.BaseFloatParse(values[11]);
			atkS = base.BaseFloatParse(values[12]);
			critS = base.BaseFloatParse(values[13]);
			critDS = base.BaseFloatParse(values[14]);
			critRS = base.BaseFloatParse(values[15]);
			resPhysicS = base.BaseFloatParse(values[16]);
			resFireS = base.BaseFloatParse(values[17]);
			resIceS = base.BaseFloatParse(values[18]);
			resLightningS = base.BaseFloatParse(values[19]);
			resPoisonS = base.BaseFloatParse(values[20]);
			resDiseaseS = base.BaseFloatParse(values[21]);
			resAcidS = base.BaseFloatParse(values[22]);
		}

		public StatsBase(int code, int init) : base(code)
		{
			hpMaxS = init;
			hpRS = init;
			sanMaxS = init;
			sanRS = init;
			apMaxS = init;
			apRS = init;
			spdS = init;
			accS = init;
			dogS = init;
			defTS = init;
			defRS = init;
			atkS = init;
			critS = init;
			critDS = init;
			critRS = init;
			resPhysicS = init;
			resFireS = init;
			resIceS = init;
			resLightningS = init;
			resPoisonS = init;
			resDiseaseS = init;
			resAcidS = init;
		}

		public static CalStats operator +(StatsBase a, StatsBase b)
			=> new CalStats(
				b.hpMaxS + a.hpMaxS,
				b.hpRS + a.hpRS,
				b.sanMaxS + a.sanMaxS,
				b.sanRS + a.sanRS,
				b.apMaxS + a.apMaxS,
				b.apRS + a.apRS,
				b.spdS + a.spdS,
				b.accS + a.accS,
				b.dogS + a.dogS,
				b.defTS + a.defTS,
				b.defRS + a.defRS,
				b.atkS + a.atkS,
				b.critS + a.critS,
				b.critDS + a.critDS,
				b.critRS + a.critRS,
				b.resPhysicS + a.resPhysicS,
				b.resFireS + a.resFireS,
				b.resIceS + a.resIceS,
				b.resLightningS + a.resLightningS,
				b.resPoisonS + a.resPoisonS,
				b.resDiseaseS + a.resDiseaseS,
				b.resAcidS + a.resAcidS
				);

		public static CalStats operator +(CalStats a, StatsBase b)
			=> new CalStats(
				b.hpMaxS + a.hpMaxS,
				b.hpRS + a.hpRS,
				b.sanMaxS + a.sanMaxS,
				b.sanRS + a.sanRS,
				b.apMaxS + a.apMaxS,
				b.apRS + a.apRS,
				b.spdS + a.spdS,
				b.accS + a.accS,
				b.dogS + a.dogS,
				b.defTS + a.defTS,
				b.defRS + a.defRS,
				b.atkS + a.atkS,
				b.critS + a.critS,
				b.critDS + a.critDS,
				b.critRS + a.critRS,
				b.resPhysicS + a.resPhysicS,
				b.resFireS + a.resFireS,
				b.resIceS + a.resIceS,
				b.resLightningS + a.resLightningS,
				b.resPoisonS + a.resPoisonS,
				b.resDiseaseS + a.resDiseaseS,
				b.resAcidS + a.resAcidS
				);
		public static CalStats operator +(StatsBase a, CalStats b)
			=> b + a;

		public static CalStats operator -(StatsBase a)
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
				-a.resFireS,
				-a.resIceS,
				-a.resLightningS,
				-a.resPoisonS,
				-a.resDiseaseS,
				-a.resAcidS
				);
		public static CalStats operator -(StatsBase a, StatsBase b)
			=> a + (-b);
		public static CalStats operator -(CalStats a, StatsBase b)
			=> a + (-b);
		public static CalStats operator -(StatsBase a, CalStats b)
			=> a + (-b);
		public static CalStats operator *(StatsBase a, StatsBase b)
			=> new CalStats(
				b.hpMaxS * a.hpMaxS,
				b.hpRS * a.hpRS,
				b.sanMaxS * a.sanMaxS,
				b.sanRS * a.sanRS,
				b.apMaxS * a.apMaxS,
				b.apRS * a.apRS,
				b.spdS * a.spdS,
				b.accS * a.accS,
				b.dogS * a.dogS,
				b.defTS * a.defTS,
				b.defRS * a.defRS,
				b.atkS * a.atkS,
				b.critS * a.critS,
				b.critDS * a.critDS,
				b.critRS * a.critRS,
				b.resPhysicS * a.resPhysicS,
				b.resFireS * a.resFireS,
				b.resIceS * a.resIceS,
				b.resLightningS * a.resLightningS,
				b.resPoisonS * a.resPoisonS,
				b.resDiseaseS * a.resDiseaseS,
				b.resAcidS * a.resAcidS
				);
		public static CalStats operator *(CalStats a, StatsBase b)
			=> new CalStats(
				b.hpMaxS * a.hpMaxS,
				b.hpRS * a.hpRS,
				b.sanMaxS * a.sanMaxS,
				b.sanRS * a.sanRS,
				b.apMaxS * a.apMaxS,
				b.apRS * a.apRS,
				b.spdS * a.spdS,
				b.accS * a.accS,
				b.dogS * a.dogS,
				b.defTS * a.defTS,
				b.defRS * a.defRS,
				b.atkS * a.atkS,
				b.critS * a.critS,
				b.critDS * a.critDS,
				b.critRS * a.critRS,
				b.resPhysicS * a.resPhysicS,
				b.resFireS * a.resFireS,
				b.resIceS * a.resIceS,
				b.resLightningS * a.resLightningS,
				b.resPoisonS * a.resPoisonS,
				b.resDiseaseS * a.resDiseaseS,
				b.resAcidS * a.resAcidS
				);
		public static CalStats operator *(StatsBase a, CalStats b)
			=> b * a;
		public static CalStats operator *(StatsBase a, int b)
			=> new CalStats(
				b * a.hpMaxS,
				b * a.hpRS,
				b * a.sanMaxS,
				b * a.sanRS,
				b * a.apMaxS,
				b * a.apRS,
				b * a.spdS,
				b * a.accS,
				b * a.dogS,
				b * a.defTS,
				b * a.defRS,
				b * a.atkS,
				b * a.critS,
				b * a.critDS,
				b * a.critRS,
				b * a.resPhysicS,
				b * a.resFireS,
				b * a.resIceS,
				b * a.resLightningS,
				b * a.resPoisonS,
				b * a.resDiseaseS,
				b * a.resAcidS
				);
		public static CalStats operator *(int a, StatsBase b)
			=> b * a;
	}
}
