using ExternalStaticReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OnTheRecord.BasicComponent
{
	public class ItemBase : Base
	{
		public readonly int nameCode;
		public readonly int itemType => _baseCode / 100000 % 10;
		public readonly int stackMax;
		public readonly int weight;
		private readonly int _consumableCode;
		private ConsumableBase? _consumable = null;
		public ref readonly ConsumableBase? consumable => ref _consumable;
		private readonly int _equipmentCode;
		private EquipmentBase? _equipment = null;
		public ref readonly EquipmentBase? equipment => ref _equipment;


		public ItemBase(string str) : base(str)
		{
			var values = base.Parse(str);
			stackMax = int.Parse(values[1]);
			weight = int.Parse(values[2]);
		}
	}
}
