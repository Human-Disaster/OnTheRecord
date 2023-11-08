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

		protected string[] Parse(String str)
		{
			return Regex.Split(str, _csvWordSplit);
		}

		public CompareTo(object? obj)
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