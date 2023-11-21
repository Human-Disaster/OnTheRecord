using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalStaticReference
{
	public enum ActivableType
	{
		None,
		Player = 1,
		Hostile = 2

	}

	public enum PlayerType
	{
		None = 0,
		Noble = 1,
		Soldier = 2
	}

	public enum HostileType
	{
		None = 0,
		BC_Conscript = 101,
		BC_Rifleman = 102,
		BC_Veterance = 103,
		BC_Assult = 201,
		BC_Grenadier = 202,
		BC_Shocktroop = 203
	}

	/*
	 * 토큰의 타입
	 * 일종의 플래그
	 * 중복되는 타입을 가질 수 있음
	 * 수정 필요?
	 */
	public enum TokenType
	{
		None =		0b0000000,
		StatAdd =	0b0000001,
		StatMul =	0b0000010,
		DotDmg =	0b0000100,
		DotHeal =	0b0001000,
		CC =		0b0010000,
		Harmful =	0b0100000,
		Useful =	0b1000000
	}

	/*
	 * 데미지 타입
	 * 일종의 플래그 - 이후 아래의 Situation 처럼 넘버링을 통해 특정 값을 가질 수 있음(이럴 경우 List로 처리?)
	 * 여러 중복되는 타입을 가질 수 있음?
	 * 타입의 분류로써 여럿을 지정하는 타입 또한 존재(ex. 충격, 참격 등을 포함하는 물리)
	 * 수정 필요
	 */
	public enum DmgType
	{
		None = 0
	}

	/*
	 * 아이템 타입
	 * 일종의 플래그 - 이후 아래의 Situation 처럼 넘버링을 통해 특정 값을 가질 수 있음(이럴 경우 List로 처리?)
	 */
	public enum ItemType
	{
		None = 0,

		// 장비 - 무기
		EquipWeapon =	0b00000000000001,
		// 장비 - 특수
		EquipSpecial =	0b00000000000010,
		// 장비 - 머리
		EquipHead =		0b00000000000100,
		// 장비 - 상체
		EquipUpper =	0b00000000001000,
		// 장비 - 하체
		EquipLower =	0b00000000010000,
		// 장비 전반
		Equipment =		0b00000000011111,

		// 소모품 - 소형
		ConsumS =		0b00000000100000,
		// 소모품 - 중형
		ConsumM =		0b00000001000000,
		// 소모품 - 대형
		ConsumL =		0b00000010000000,
		// 소모품 전반
		Consumable =	0b00000011100000,

		// 재료 - 소형
		MaterS =		0b00000100000000,
		// 재료 - 중형
		MaterM =		0b00001000000000,
		// 재료 - 대형
		MaterL =		0b00010000000000,
		// 재료 전반
		Material =		0b00011100000000,

		// 교환품 - 소형
		ExchangeS =		0b00100000000000,
		// 교환품 - 중형
		ExchangeM =		0b01000000000000,
		// 교환품 - 대형
		ExchangeL =		0b10000000000000,
		// 교환품 전반
		Exchange =		0b11100000000000,

		// 일반 아이템 전반
		NormalItem =	0b11111111100000
	}


	public enum SituationCode//게임 탐험, 보스전 전체의 상황을 카테고리화 시켜놓음
	{
		None,

		// 01xx start
		startExpedition = 0101,
		startEngage = 0102,
		startFreerom = 0103,
		startRound = 0104,
		startTurn = 0105,

		// 02xx end
		endExpedition = 0201,
		endEngage = 0202,
		endFreerom = 0203,
		endRound = 0204,
		endTurn = 0205,

		// 03xx enter
		enterRoom = 0301,

		// 04xx exit
		exitRoom = 0401,
		exitTile = 0499,

		// 05xx interact
		interactLoot = 0501,

		// 06xx do
		doMove = 0601,
		doAttack = 0602,
		doDown = 0603,

		// 07xx take
		takeMove = 0701,
		takeAttack = 0702,
		takeDown = 0703,

		// 08xx sqaud
		sqaudDown = 0801,

		// 09xx hostile
		hostileDown = 0901
	}

	public enum DamageType
	{
		// 0000 예외
		None,
		// 1XXX 물리
		impact = 1000,
		slash = 1001,
		pierce = 1002,
		shot = 1003,
		explo = 1004,
		// 2XXX 속성
		flame = 2000,
		freeze = 2001,
		electric = 2002,
		poison = 2003,
		chemical = 2004,
		// 3XXX 생체
		disease = 3000,
		bleed = 3001,
		// 4XXX 보조
		hunt = 4000,
		harvest = 4001
	}

	public enum DamageResistanceRange
	{
		m5 = -400,
		m4 = -300,
		m3 = -200,
		m2 = -100,
		m1 = 0,
		z = 100,
		p1 = 200,
		p2 = 300,
		p3 = 400,
		p4 = 500,
		infinity = 9999
	}

	public readonly struct DamageResistanceValue
	{
		//	-5 -4 -3 -2 -1 0 1 2 3 4 5 6
		public static readonly float[] common = {
			1.6F, 1.45F, 1.33F, 1.25F, 1.15F, 0.98F, 0.87F, 0.75F, 0.67F, 0.5F, 0.33F, 0F };
	}

	public enum TokenCode
	{
		None,
		
		NonInert = 100001,		//활성 토큰
		Inert = 100002,			//비활성 토큰
		Down = 100003,			//전투불능 토큰
		

		Concealment = 300001,	//은폐 토큰
		Exposure = 400001,		//노출 토큰

		ResistPhysic = 700001,	//물리 저항
		ResistFlame = 700002,	//화염 저항
		ResistFreeze = 700003,	//빙결 저항
		ResistElectric = 700004,//전류 저항
		ResistPoison = 700005,	//독 저항
		ResistDisease = 700006,	//병해 저항
		ResistChemical = 700007,//화학 저항

	}

	public enum AimingCode
	{
		None,
		// 01 - 03 사각형 기준
		Circle,
		Square,
		Diamond,
		// 04 - 06 확산 기준
		Cross,
		XShape,
		Spread,
	}

	public enum EffectCode
	{
		None,
		// 01 - 03 사각형 기준
		Circle,
		Square,
		Diamond,
		// 04 - 06 확산 기준
		Cross,
		XShape,
		Spread,
	}

	public enum SkillTypePreCode
	{
		None,
		Direct,
		HighAngle,
		Travel,
		Charge,
		Jump,
		Penetrate,
		True,
		NonAttack
	}

	public enum SkillTypePostCode
	{
		None,
		Target,
		Nontarget,
		Area,
		Floor
	}
}
