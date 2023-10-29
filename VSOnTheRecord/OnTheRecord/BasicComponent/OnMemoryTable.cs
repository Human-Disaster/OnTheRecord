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
		private List<StatsBase> _StatsBaseList;
		private static OnMemoryTable? _Instance = null;
		private static readonly object _Lock = new object();
		private static string _csvWordSplit = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
		private static string _csvLineSplit = @"\r\n|\n\r|\n|\r";
		private OnMemoryTable()
		{
			string csvText = ReadFile("파일 경로.csv");
			string[] data = Regex.Split(csvText, _csvWordSplit);
			_StatsBaseList = new List<StatsBase>(int.Parse(data[0]));
			// 각각의 BaseList 도 마찬가지로 초기화
			MakeStatsBaseList();
			// 각각의 BaseList 도 마찬가지로 구현
		}

		private void MakeStatsBaseList()
		{
			string csvText = ReadFile("파일 경로.csv");
			var lines = Regex.Split(csvText, _csvLineSplit);
			for (int i = 0; i < lines.Length; i++)
			{
				_StatsBaseList.Add(new StatsBase(lines[i]));
			}
		}

#if SERVER
		private string ReadFile(string filePath)
		{
			return System.IO.File.ReadAllText(filePath);
		}
#endif
		public static OnMemoryTable Instance()
		{
			if (_Instance == null)
			{
				lock (_Lock)
				{
					if (_Instance == null)
					{
						_Instance = new OnMemoryTable();
					}
				}
			}
			return _Instance;
		}
	}
}
