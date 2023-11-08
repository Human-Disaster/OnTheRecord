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

		private readonly int _itemCode;
		public readonly int nameCode;
		public readonly int itemType => _itemCode / 100000 % 10;
		public readonly int stackMax;
		public readonly int weight;
		private readonly int _consumableCode;
		private ConsumableBase? _consumable = null;
		public ref readonly ConsumableBase? consumable => ref _consumable;
		private readonly int _equipmentCode;
		private EquipmentBase? _equipment = null;
		public ref readonly EquipmentBase? equipment => ref _equipment;


		public ItemBase(string str)
		{
			var values = Regex.Split(str, _csvWordSplit);
			_itemCode = int.Parse(values[0]);
			stackMax = int.Parse(values[1]);
			weight = int.Parse(values[2]);
		}

		public int CompareTo(object? obj)
		{
			if (obj == null)
				return 1;
			ItemBase? otherItemBase = obj as ItemBase;
			if (otherItemBase != null)
				return _itemCode.CompareTo(otherItemBase._itemCode);
			else
				throw new ArgumentException("Object is not a ItemBase");
		}
	}
}
