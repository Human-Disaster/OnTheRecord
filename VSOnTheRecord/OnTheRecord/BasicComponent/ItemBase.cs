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

		public int GetItemType()
		{
			return _baseCode / 100000 % 10;
		}

		public readonly int stackMax;
		public readonly int weight;
		public readonly int consumableCode;
		private ConsumableBase? _consumable = null;
		public ref readonly ConsumableBase? consumable => ref _consumable;
		public readonly int equipmentCode;
		private EquipmentBase? _equipment = null;
		public ref readonly EquipmentBase? equipment => ref _equipment;


		public ItemBase(string str) : base(str)
		{
			string[] values = base.Parse(str);
			stackMax = base.BaseIntParse(values[1]);
			weight = base.BaseIntParse(values[2]);
			consumableCode = base.BaseIntParse(values[3]);
			equipmentCode = base.BaseIntParse(values[4]);
		}

		public void SetConsumable(ConsumableBase consumable)
		{
			_consumable = consumable;
		}

		public void SetEquipment(EquipmentBase equipment)
		{
			_equipment = equipment;
		}
	}
}
