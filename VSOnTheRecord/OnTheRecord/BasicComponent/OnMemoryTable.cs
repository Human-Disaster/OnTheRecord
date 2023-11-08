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
		private List<StatsBase> _statsBaseList;
		private List<TokenBase> _tokenBaseList;
		private List<TileConditionBase> _tileConditionBaseList;
		private List<ActiveSkillBase> _activeSkillBaseList;
		private List<ItemBase> _itemBaseList;


		private static OnMemoryTable? _instance = null;
		private static readonly object _lock = new object();
		private static string _csvWordSplit = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
		private static string _csvLineSplit = @"\r\n|\n\r|\n|\r";
		private OnMemoryTable()
		{
			string csvText = ReadFile("파일 경로.csv");
			string[] data = Regex.Split(csvText, _csvWordSplit);
			_statsBaseList = new List<StatsBase>(int.Parse(data[0]));
			_tokenBaseList = new List<TokenBase>(int.Parse(data[1]));
			_tileConditionBaseList = new List<TileConditionBase>(int.Parse(data[2]));
			_activeSkillBaseList = new List<ActiveSkillBase>(int.Parse(data[3]));
			// 각각의 BaseList 도 마찬가지로 초기화
			MakeStatsBaseList();
			MakeTokenBaseList();
			// 각각의 BaseList 도 마찬가지로 구현
		}

		private void MakeStatsBaseList()
		{
			string csvText = ReadFile("파일 경로.csv");
			var lines = Regex.Split(csvText, _csvLineSplit);
			for (int i = 0; i < lines.Length; i++)
				_statsBaseList.Add(new StatsBase(lines[i]));
			_statsBaseList.Sort();
		}

		private void MakeTokenBaseList()
		{
			string csvText = ReadFile("파일 경로.csv");
			var lines = Regex.Split(csvText, _csvLineSplit);
			for (int i = 0; i < lines.Length; i++)
				_tokenBaseList.Add(new TokenBase(lines[i]));
			_tokenBaseList.Sort();
			foreach(TokenBase tb in _tokenBaseList)
			{
				if (tb.promotionToken.targetCode != 0)
					tb.promotionToken.SetToken(GetTokenBase(tb.promotionToken._tokenCode));
				if (tb.demotionToken.targetCode != 0)
					tb.demotionToken.SetToken(GetTokenBase(tb.demotionToken._tokenCode));
				tb.SetAddStats(GetStatsBase(tb.addStatsCode));
				tb.SetMulStats(GetStatsBase(tb.mulStatsCode));
			}
		}

		private void MakeTileConditionBaseList()
		{
			string csvText = ReadFile("파일 경로.csv");
			var lines = Regex.Split(csvText, _csvLineSplit);
			for (int i = 0; i < lines.Length; i++)
				_tileConditionBaseList.Add(new TileConditionBase(lines[i]));
			_tileConditionBaseList.Sort();
			foreach(TileConditionBase tcb in _tileConditionBaseList)
			{
				//todo
			}
		}

		private void MakeActiveSkillList()
		{
			string csvText = ReadFile("파일 경로.csv");
			var lines = Regex.Split(csvText, _csvLineSplit);
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
				asb.SetAddStats(GetStatsBase(asb.addStatsCode));
				asb.SetMulStats(GetStatsBase(asb.mulStatsCode));
			}
		}

		public StatsBase GetStatsBase(int statsCode)
		{
			return _statsBaseList[_statsBaseList.BinarySearch(new StatsBase(statsCode))];
		}
		public TokenBase GetTokenBase(int tokenCode)
		{
			return _tokenBaseList[_tokenBaseList.BinarySearch(new TokenBase(tokenCode))];
		}
		public TileConditionBase GetTileConditionBase(int tileConditionCode)
		{
			return _tileConditionBaseList[_tileConditionBaseList.BinarySearch(new TileConditionBase(tileConditionCode))];
		}
		public ActiveSkillBase GetActiveSkillBase(int activeSkillCode)
		{
			return _activeSkillBaseList[_activeSkillBaseList.BinarySearch(new ActiveSkillBase(activeSkillCode))];
		}

#if SERVER
		private string ReadFile(string filePath)
		{
			return System.IO.File.ReadAllText(filePath);
		}
#endif
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
