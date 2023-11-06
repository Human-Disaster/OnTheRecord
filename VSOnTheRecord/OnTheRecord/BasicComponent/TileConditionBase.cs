using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRecord.BasicComponent
{
	public class TileConditionBase
	{
		private static string _csvWordSplit = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";

		private readonly int _tileConditionCode;
		private readonly

		public TileConditionBase(int tileConditionCode)
		{
			_tileConditionCode = tileConditionCode;
		}
	}
}
