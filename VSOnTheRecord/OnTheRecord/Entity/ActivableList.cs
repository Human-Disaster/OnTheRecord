using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnTheRecord.BasicComponent;
using OnTheRecord.Map;
using OnTheRecord.Entity;
using ExternalStaticReference;

namespace OnTheRecord.Entity
{
	public class ActivableList
	{
		public List<Activable> activableList;
		public int currentTurnActivable;

		public void Situation(int situation)
		{
			switch (situation)
			{
				case (int)SituationCode.endTurn:
					TurnEnd();
					break;
				case (int)SituationCode.endRound:
					RoundEnd();
					break;
			}
		}

		private void TurnEnd()
		{
			currentTurnActivable++;
		}

		private void RoundEnd()
		{
			currentTurnActivable = 0;
		}

		public ActivableList()
		{
			activableList = new List<Activable>();
			currentTurnActivable = 0;
		}

		public ActivableList(List<Activable> activableList)
		{
			this.activableList = activableList;
			currentTurnActivable = 0;
		}

		public ActivableList(Activable activable)
		{
			activableList = new List<Activable>();
			activableList.Add(activable);
			currentTurnActivable = 0;
		}

		public ActivableList(ActivableList activableList)
		{
			this.activableList = activableList.activableList;
			currentTurnActivable = 0;
		}

		public ActivableList(List<Player> playerList)
		{
			activableList = new List<Activable>();
			foreach (Player player in playerList)
			{
				activableList.Add(player);
			}
			currentTurnActivable = 0;
		}

		public void AddActivable(Activable activable)
		{
			activableList.Add(activable);
		}

		public void AddActivable(ActivableList activableList)
		{
			foreach (Activable activable in activableList.activableList)
			{
				this.activableList.Add(activable);
			}
		}

		public void AddActivable(List<Player> playerList)
		{
			foreach (Player player in playerList)
			{
				activableList.Add(player);
			}
		}

		public void RemoveActivable(Activable activable)
		{
			activableList.Remove(activable);
		}

		public void RemoveAtActivable(int index)
		{
			activableList.RemoveAt(index);
		}

		public void SortTurn()
		{
			activableList.Sort()
		}
	}
}