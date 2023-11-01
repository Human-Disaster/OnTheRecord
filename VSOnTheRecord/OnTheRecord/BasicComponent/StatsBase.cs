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
	public class StatsBase : IComparable
	{
		private static string _csvWordSplit = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";

		private readonly int _statsCode;
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


		public StatsBase(string str)
		{
			var values = Regex.Split(str, _csvWordSplit);
			_statsCode = int.Parse(values[0]);
			hpMaxS = float.Parse(values[1]);
			hpRS = float.Parse(values[2]);
			sanMaxS = float.Parse(values[3]);
			sanRS = float.Parse(values[4]);
			apMaxS = float.Parse(values[5]);
			apRS = float.Parse(values[6]);
			spdS = float.Parse(values[7]);
			accS = float.Parse(values[8]);
			dogS = float.Parse(values[9]);
			defTS = float.Parse(values[10]);
			defRS = float.Parse(values[11]);
			atkS = float.Parse(values[12]);
			critS = float.Parse(values[13]);
			critDS = float.Parse(values[14]);
			critRS = float.Parse(values[15]);
			resPhysicS = float.Parse(values[16]);
			resFireS = float.Parse(values[17]);
			resIceS = float.Parse(values[18]);
			resLightningS = float.Parse(values[19]);
			resPoisonS = float.Parse(values[20]);
			resDiseaseS = float.Parse(values[21]);
			resAcidS = float.Parse(values[22]);
		}

		public StatsBase(int code)
		{
			_statsCode = code;
		}

		public int CompareTo(object? obj)
		{
			if (obj == null)
				return 1;
			StatsBase? other = obj as StatsBase;
			if (other != null)
				return this._statsCode.CompareTo(other._statsCode);
			else
				throw new ArgumentException("Object is not a StatsBase");
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
	}
}
