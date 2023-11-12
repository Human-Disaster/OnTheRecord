using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRecord.BasicComponent
{
	public class Consumable : Item
	{
		public readonly ConsumableBase consumableBase;

		public Consumable(int itemCode) : base(itemCode)
		{
			if (base.itemBase.consumable == null)
				throw new Exception(string.Format("{0} 코드를 가진 ConsumableBase가 null입니다.", itemCode));
			consumableBase = base.itemBase.consumable;
		}

		public Consumable(int itemCode, int stack) : base(itemCode, stack)
		{
			if (base.itemBase.consumable == null)
				throw new Exception(string.Format("{0} 코드를 가진 ConsumableBase가 null입니다.", itemCode));
			consumableBase = base.itemBase.consumable;
		}

		public Consumable(ItemBase itemBase) : base(itemBase)
		{
			if (base.itemBase.consumable == null)
				throw new Exception("ItemBase가 가진 ConsumableBase가 null입니다.");
			consumableBase = base.itemBase.consumable;
		}

		public Consumable(ItemBase itemBase, int stack) : base(itemBase, stack)
		{
			if (base.itemBase.consumable == null)
				throw new Exception("ItemBase가 가진 ConsumableBase가 null입니다.");
			consumableBase = base.itemBase.consumable;
		}

		public Consumable(Consumable other) : base(other.itemBase)
		{
			consumableBase = other.consumableBase;
		}

		public Consumable(Consumable other, int stack) : base(other.itemBase, stack)
		{
			consumableBase = other.consumableBase;
		}
	}
}
