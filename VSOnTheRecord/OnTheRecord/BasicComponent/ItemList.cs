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
				itemList.Add(newItem);
			else
				itemList[findItemIndex].MoveStack(newItem);
		}
	}
}
