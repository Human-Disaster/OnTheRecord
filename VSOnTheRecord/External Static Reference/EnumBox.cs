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


	public enum Stituation//게임 탐험, 보스전 전체의 상황을 카테고리화 시켜놓음
	{
		None,
		Eneter_Dungeon = 001,
		Enter_Room = 002,
		Enter_Incounter = 003,

		Take_dmg = 101,
		Take_sanitydmg = 102,
		Take_elementdmg = 103,
		Take_buff = 104,
		Take_heal = 105,
		Take_stun = 106,
		Take_confuse = 107,
		Take_doge = 108,
		Take_miss = 109,
		Take_dowm = 110,

		/*
		Take_useact = 1??,
		Take_notuseact = 1??,
		*/

		Do_dmg = 201,
		Do_sanitydmg = 202,
		Do_elementdmg = 203,
		Do_buff = 204,
		Do_heal = 205,
		Do_stun = 206,
		Do_confuse = 207,
		Do_doge = 208,
		Do_miss = 209,
		Do_dowm = 210,

		/*
		Do_useact = 2??,
		Do_notuseact = 2??,
		*/

		Sqaud_down = 301,
		Sqaud_notuseact = 302,
		Sqaud_stun = 303,

		Hostile_down = 401

	}




}
