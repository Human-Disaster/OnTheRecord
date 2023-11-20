using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRecord.BasicComponent
{
	class ItemList
	{
		List<Item> itemList = new List<Item>();

		void AddItem(Item newItem)
		{
			int findItemIndex = itemList.IndexOf(newItem);
			if (findItemIndex == -1)
			{
				itemList.Add(new Item(newItem, 0));
				findItemIndex = itemList.Count - 1;
			}
			while (newItem.Getstack() + itemList[findItemIndex].Getstack() > newItem.itemBase.stackMax)
			{
				itemList[findItemIndex].MoveStack(newItem);
				itemList.Add(new Item(newItem, 0));
				findItemIndex = itemList.Count - 1;
			}
			itemList[findItemIndex].MoveStack(newItem);
		}
	}
}
