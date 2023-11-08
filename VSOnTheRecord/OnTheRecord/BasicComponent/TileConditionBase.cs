using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OnTheRecord.BasicComponent
{
	public class TileConditionBase : Base
	{
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
			String[] values = Parse(str);
			_baseCode = int.Parse(values[0]);
			//todo
		}
	}
}
