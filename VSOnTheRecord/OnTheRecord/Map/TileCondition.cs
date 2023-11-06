using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnTheRecord.BasicComponent;

namespace OnTheRecord.Map
{
	public class TileCondition
	{
		public readonly TileConditionBase condition;
		public int remainingTurn;

		public TileCondition(int code, int turn)
		{

			remainingTurn = turn;
		}
	}
}
