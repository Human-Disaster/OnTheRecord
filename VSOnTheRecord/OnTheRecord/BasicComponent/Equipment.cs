using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  미완 이런 느낌이다 참고만 할 것
*/
namespace OnTheRecord.BasicComponent
{
	class Equipment : Item
	{
		public readonly EquipmentBase equipmentBase;

		public Equipment(int itemCode) : base(itemCode)
		{
			if (base.itemBase.equipment == null)
				throw new Exception(string.Format("{0} 코드를 가진 EquipmentBase가 null입니다.", itemCode));
			equipmentBase = base.itemBase.equipment;
		}

		public Equipment(ItemBase itemBase) : base(itemBase)
		{
			if (base.itemBase.equipment == null)
				throw new Exception("ItemBase가 가진 EquipmentBase가 null입니다.");
			equipmentBase = base.itemBase.equipment;
		}

		public Equipment(Equipment other) : base(other.itemBase)
		{
			equipmentBase = other.equipmentBase;
		}
	}
}
