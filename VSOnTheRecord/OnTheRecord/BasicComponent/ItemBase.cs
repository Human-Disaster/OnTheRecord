using ExternalStaticReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OnTheRecord.BasicComponent
{
	public class ItemBase : IComparable
	{
		private static string _csvWordSplit = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";

		public readonly int itemCode;
		public readonly int itemType;
		public readonly int weight;


		public ItemBase(string str)
		{
			var values = Regex.Split(str, _csvWordSplit);
			itemCode = int.Parse(values[0]);
			itemType = int.Parse(values[1]);
			weight = int.Parse(values[2]);
		}

		public int CompareTo(object? obj)
		{
			return itemCode.CompareTo(((ItemBase)obj).itemCode);
		}
	}
}
