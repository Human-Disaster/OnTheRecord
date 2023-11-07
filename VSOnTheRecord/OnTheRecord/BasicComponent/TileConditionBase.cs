using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OnTheRecord.BasicComponent
{
	public class TileConditionBase : IComparable
	{
		private static string _csvWordSplit = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";

		private readonly int _tileConditionCode;
		public readonly int tileConditionType;
		public readonly int hpTurn;
		public readonly int apTurn;
		public readonly int sanTurn;
		public readonly TokenInfo grantToken;

		public TileConditionBase(int tileConditionCode)
		{
			_tileConditionCode = tileConditionCode;
			//todo
		}

		public TileConditionBase(string str)
		{
			var values = Regex.Split(str, _csvWordSplit);
			//todo
		}

		public CompareTo(object? obj)
		{
			if (obj == null)
				return 1;
			TileConditionBase? otherTileConditionBase = obj as TileConditionBase;
			if (otherTileConditionBase != null)
				return _tileConditionCode.CompareTo(otherTileConditionBase._tileConditionCode);
			else
				throw new ArgumentException("Object is not a TileConditionBase");
		}
	}
}
