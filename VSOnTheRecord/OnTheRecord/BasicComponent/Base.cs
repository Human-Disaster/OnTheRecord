using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OnTheRecord.BasicComponent
{
	public class Base : IComparable
	{
		private static string _csvWordSplit = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";

		protected readonly int _baseCode;

		public Base(int baseCode)
		{
			_baseCode = baseCode;
		}

		protected Base(string str)
		{
			int commaIndex = str.IndexOf(',');
			_baseCode = int.Parse(str.Substring(0, commaIndex));
		}

		protected int BaseIntParse(string str)
		{
			int result;
			if (int.TryParse(str, out result))
				return result;
			else
				return 0;
		}

		protected float BaseFloatParse(string str)
		{
			float result;
			if (float.TryParse(str, out result))
				return result;
			else
				return 0;
		}

		protected string[] Parse(string str)
		{
			return Regex.Split(str, _csvWordSplit);
		}

		public int CompareTo(object? obj)
		{
			if (obj == null)
				return 1;
			Base? otherBase = obj as Base;
			if (otherBase != null)
				return _baseCode.CompareTo(otherBase._baseCode);
			else
				throw new ArgumentException("Object is not a Base");
		}
	}
}