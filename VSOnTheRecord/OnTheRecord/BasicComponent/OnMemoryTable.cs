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
		private List<SkillBase> _skillBaseList;
		private List<ItemBase> _itemBaseList;

		private List<TileConditionBase> _tileConditionBaseList;

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

		public StatsBase GetStatsBase(int statsCode)
		{
			return _statsBaseList[_statsBaseList.BinarySearch(new StatsBase(statsCode))];
		}
		public TokenBase GetTokenBase(int tokenCode)
		{
			return _tokenBaseList[_tokenBaseList.BinarySearch(new TokenBase(tokenCode))];
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
