using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
#if !SERVER
using UnityEngine;
#endif

/*
 * csv 파일로 된 테이블을 메모리에 올려서 처리하기 위한 싱글톤 클래스
 * 각종 csv로 부터 추출한 내용을 List에 집어넣어 준비
 * 클라이언트, 서버용 2가지 버전 구분은 SERVER 가 정의되어있는 가 를 기준으로 구분
 */
namespace OnTheRecord.BasicComponent
{
	class OnMemoryTable
	{
		private enum ParsNum
		{
			StatsBase = 0,
			TokenBase = 1,
			TileConditionBase = 2,
			ActiveSkillBase = 3,
			PassiveSkillBase = 4,
			ArmBase = 5,
			EquipmentBase = 6,
			ConsumableBase = 7,
			ItemBase = 8,
			SpecialNPCBase = 9
		}

		private enum ParsTokenNum
		{

		}

		private List<Base> _statsBaseList;
		private List<Base> _tokenBaseList;
		private List<Base> _tileConditionBaseList;
		private List<Base> _activeSkillBaseList;
		private List<Base> _passiveSkillBaseList;
		private List<Base> _armBaseList;
		private List<Base> _equipmentBaseList;
		private List<Base> _consumableBaseList;
		private List<Base> _itemBaseList;
		private List<Base> _specialNPCBaseList;


		private static OnMemoryTable? _instance = null;
		private static readonly object _lock = new object();
		private static string _csvWordSplit = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
		private static string _csvLineSplit = @"\r\n|\n\r|\n|\r";
		private OnMemoryTable()
		{
			string csvText = ReadFile("파일 경로.csv");
			string[] data = Regex.Split(csvText, _csvWordSplit);
			_statsBaseList = new List<Base>(int.Parse(data[(int)ParsNum.StatsBase]));
			_tokenBaseList = new List<Base>(int.Parse(data[(int)ParsNum.TokenBase]));
			_tileConditionBaseList = new List<Base>(int.Parse(data[(int)ParsNum.TileConditionBase]));
			_activeSkillBaseList = new List<Base>(int.Parse(data[(int)ParsNum.ActiveSkillBase]));
			_passiveSkillBaseList = new List<Base>(int.Parse(data[(int)ParsNum.PassiveSkillBase]));
			_armBaseList = new List<Base>(int.Parse(data[(int)ParsNum.ArmBase]));
			_equipmentBaseList = new List<Base>(int.Parse(data[(int)ParsNum.EquipmentBase]));
			_consumableBaseList = new List<Base>(int.Parse(data[(int)ParsNum.ConsumableBase]));
			_itemBaseList = new List<Base>(int.Parse(data[(int)ParsNum.ItemBase]));
			_specialNPCBaseList = new List<Base>(int.Parse(data[(int)ParsNum.SpecialNPCBase]));
			// 각각의 BaseList 도 마찬가지로 초기화
			MakeStatsBaseList();
			MakeTokenBaseList();
			MakeTileConditionBaseList();
			MakeActiveSkillBaseList();
			MakePassiveSkillBaseList();
			MakeArmBaseList();
			MakeEquipmentBaseList();
			MakeConsumableBaseList();
			MakeItemBaseList();
			// 각각의 BaseList 도 마찬가지로 구현
		}

		private void MakeStatsBaseList()
		{
			string csvText = ReadFile("파일 경로.csv");
			string[] lines = Regex.Split(csvText, _csvLineSplit);
			for (int i = 0; i < lines.Length; i++)
				_statsBaseList.Add(new StatsBase(lines[i]));
			_statsBaseList.Sort();
		}

		private void MakeTokenBaseList()
		{
			string csvText = ReadFile("파일 경로.csv");
			string[] lines = Regex.Split(csvText, _csvLineSplit);
			for (int i = 0; i < lines.Length; i++)
				_tokenBaseList.Add(new TokenBase(lines[i]));
			_tokenBaseList.Sort();
			foreach(TokenBase tb in _tokenBaseList)
			{
				if (tb.promotionToken.targetCode != 0)
					tb.promotionToken.SetToken(GetTokenBase(tb.promotionToken._tokenCode));
				if (tb.demotionToken.targetCode != 0)
					tb.demotionToken.SetToken(GetTokenBase(tb.demotionToken._tokenCode));
				if (tb.addStatsCode != 0)
					tb.SetAddStats(GetStatsBase(tb.addStatsCode));
				if (tb.mulStatsCode != 0)
					tb.SetMulStats(GetStatsBase(tb.mulStatsCode));
				if (tb.secondMulStatsCode != 0)
					tb.SetSecondMulStats(GetStatsBase(tb.secondMulStatsCode));
			}
		}

		private void MakeTileConditionBaseList()
		{
			string csvText = ReadFile("파일 경로.csv");
			string[] lines = Regex.Split(csvText, _csvLineSplit);
			for (int i = 0; i < lines.Length; i++)
				_tileConditionBaseList.Add(new TileConditionBase(lines[i]));
			_tileConditionBaseList.Sort();
			foreach(TileConditionBase tcb in _tileConditionBaseList)
			{
				//todo
			}
		}

		private void MakeActiveSkillBaseList()
		{
			string csvText = ReadFile("파일 경로.csv");
			string[] lines = Regex.Split(csvText, _csvLineSplit);
			for (int i = 0; i < lines.Length; i++)
				_activeSkillBaseList.Add(new ActiveSkillBase(lines[i]));
			_activeSkillBaseList.Sort();
			foreach(ActiveSkillBase asb in _activeSkillBaseList)
			{
				if (asb.grantToken1.targetCode != 0)
					asb.grantToken1.SetToken(GetTokenBase(asb.grantToken1._tokenCode));
				if (asb.grantToken2.targetCode != 0)
					asb.grantToken2.SetToken(GetTokenBase(asb.grantToken2._tokenCode));
				if (asb.grantToken3.targetCode != 0)
					asb.grantToken3.SetToken(GetTokenBase(asb.grantToken3._tokenCode));	
				if (asb.removeToken1.targetCode != 0)
					asb.removeToken1.SetToken(GetTokenBase(asb.removeToken1._tokenCode));
				if (asb.removeToken2.targetCode != 0)
					asb.removeToken2.SetToken(GetTokenBase(asb.removeToken2._tokenCode));
				if (asb.removeToken3.targetCode != 0)
					asb.removeToken3.SetToken(GetTokenBase(asb.removeToken3._tokenCode));
				if (asb.tileConditionCode != 0)
					asb.SetTileCondition(GetTileConditionBase(asb.tileConditionCode));
				if (asb.addStatsCode != 0)
					asb.SetAddStats(GetStatsBase(asb.addStatsCode));
				if (asb.mulStatsCode != 0)
					asb.SetMulStats(GetStatsBase(asb.mulStatsCode));
			}
		}

		private void MakePassiveSkillBaseList()
		{
			string csvText = ReadFile("파일 경로.csv");
			string[] lines = Regex.Split(csvText, _csvLineSplit);
			for (int i = 0; i < lines.Length; i++)
				_passiveSkillBaseList.Add(new PassiveSkillBase(lines[i]));
			_passiveSkillBaseList.Sort();
			foreach(PassiveSkillBase psb in _passiveSkillBaseList)
			{
				//todo
			}
		}

		private void MakeArmBaseList()
		{
			string csvText = ReadFile("파일 경로.csv");
			string[] lines = Regex.Split(csvText, _csvLineSplit);
			for (int i = 0; i < lines.Length; i++)
				_armBaseList.Add(new ArmBase(lines[i]));
			_armBaseList.Sort();
			foreach(ArmBase ab in _armBaseList)
			{
				if (ab.normalAttackCode != 0)
					ab.SetNormalAttack(GetActiveSkillBase(ab.normalAttackCode));
				if (ab.skillAttackCode != 0)
					ab.SetSkillAttack(GetActiveSkillBase(ab.skillAttackCode));
				if (ab.specialSkillCode != 0)
					ab.SetSpecialSkill(GetActiveSkillBase(ab.specialSkillCode));
			}
		}

		private void MakeEquipmentBaseList()
		{
			string csvText = ReadFile("파일 경로.csv");
			string[] lines = Regex.Split(csvText, _csvLineSplit);
			for (int i = 0; i < lines.Length; i++)
				_equipmentBaseList.Add(new EquipmentBase(lines[i]));
			_equipmentBaseList.Sort();
			foreach(EquipmentBase eb in _equipmentBaseList)
			{
				if (eb.addStatsCode != 0)
					eb.SetAddStats(GetStatsBase(eb.addStatsCode));
				if (eb.mulStatsCode != 0)
					eb.SetMulStats(GetStatsBase(eb.mulStatsCode));
				if (eb.armsCode != 0)
					eb.SetArms(GetArmBase(eb.armsCode));
			}
		}

		private void MakeConsumableBaseList()
		{
			string csvText = ReadFile("파일 경로.csv");
			string[] lines = Regex.Split(csvText, _csvLineSplit);
			for (int i = 0; i < lines.Length; i++)
				_consumableBaseList.Add(new ConsumableBase(lines[i]));
			_consumableBaseList.Sort();
			foreach(ConsumableBase cb in _consumableBaseList)
			{
				if (cb.skillCode != 0)
					cb.SetSkill(GetActiveSkillBase(cb.skillCode));
			}
		}

		private void MakeItemBaseList()
		{
			string csvText = ReadFile("파일 경로.csv");
			string[] lines = Regex.Split(csvText, _csvLineSplit);
			for (int i = 0; i < lines.Length; i++)
				_itemBaseList.Add(new ItemBase(lines[i]));
			_itemBaseList.Sort();
			foreach(ItemBase ib in _itemBaseList)
			{
				if (ib.consumableCode != 0)
					ib.SetConsumable(GetConsumableBase(ib.consumableCode));
				if (ib.equipmentCode != 0)
					ib.SetEquipment(GetEquipmentBase(ib.equipmentCode));
			}
		}

		private void MakeSpecialNPCBaseList()
		{
			string csvText = ReadFile("파일 경로.csv");
			string[] lines = Regex.Split(csvText, _csvLineSplit);
			for (int i = 0; i < lines.Length; i++)
				_specialNPCBaseList.Add(new SpecialNPCBase(lines[i]));
			_specialNPCBaseList.Sort();
			foreach(SpecialNPCBase snb in _specialNPCBaseList)
			{
				for (int i = 0; i < SpecialNPCBase.activeSkillCodeNum; i++)
					if (snb.activeSkillCode[i] != 0)
						snb.SetActiveSkill(GetActiveSkillBase(snb.activeSkillCode[i]), i);
			}
		}

		public StatsBase? GetStatsBase(int statsCode)
		{
			int i = _statsBaseList.BinarySearch(new Base(statsCode));
			if (i < 0)
				return null;
			else
				return (StatsBase)_statsBaseList[i];
		}
		public TokenBase GetTokenBase(int tokenCode)
		{
			return (TokenBase)_tokenBaseList[_tokenBaseList.BinarySearch(new Base(tokenCode))];
		}
		public TileConditionBase GetTileConditionBase(int tileConditionCode)
		{
			return (TileConditionBase)_tileConditionBaseList[_tileConditionBaseList.BinarySearch(new Base(tileConditionCode))];
		}
		public ActiveSkillBase GetActiveSkillBase(int activeSkillCode)
		{
			return (ActiveSkillBase)_activeSkillBaseList[_activeSkillBaseList.BinarySearch(new Base(activeSkillCode))];
		}
		public PassiveSkillBase GetPassiveSkillBase(int passiveSkillCode)
		{
			return (PassiveSkillBase)_passiveSkillBaseList[_passiveSkillBaseList.BinarySearch(new Base(passiveSkillCode))];
		}
		public ArmBase GetArmBase(int armCode)
		{
			return (ArmBase)_armBaseList[_armBaseList.BinarySearch(new Base(armCode))];
		}
		public EquipmentBase GetEquipmentBase(int equipmentCode)
		{
			return (EquipmentBase)_equipmentBaseList[_equipmentBaseList.BinarySearch(new Base(equipmentCode))];
		}
		public ConsumableBase GetConsumableBase(int consumableCode)
		{
			return (ConsumableBase)_consumableBaseList[_consumableBaseList.BinarySearch(new Base(consumableCode))];
		}
		public ItemBase GetItemBase(int itemCode)
		{
			return (ItemBase)_itemBaseList[_itemBaseList.BinarySearch(new Base(itemCode))];
		}

		public SpecialNPCBase GetSpecialNPCBase(int specialNPCCode)
		{
			return (SpecialNPCBase)_specialNPCBaseList[_specialNPCBaseList.BinarySearch(new Base(specialNPCCode))];
		}

		private string ReadFile(string filePath)
		{
#if SERVER
			return System.IO.File.ReadAllText(filePath);
#else
			TextAsset textAsset = Resources.Load<TextAsset>(filePath);
			return textAsset.text;
#endif
		}
		public static OnMemoryTable Instance()
		{
			if (_instance == null)
			{
				lock (_lock)
				{
					if (_instance == null)
					{
						_instance = new OnMemoryTable();
					}
				}
			}
			return _instance;
		}
	}
}
