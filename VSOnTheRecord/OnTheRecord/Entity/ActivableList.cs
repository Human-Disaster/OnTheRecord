using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnTheRecord.BasicComponent;
using OnTheRecord.Map;
using OnTheRecord.Entity;

namespace OnTheRecord.Entity
{
	public class ActivableList
	{
		public List<Activable> activableList;

		public ActivableList()
		{
			activableList = new List<Activable>();
		}

		public ActivableList(List<Activable> activableList)
		{
			this.activableList = activableList;
		}

		public ActivableList(Activable activable)
		{
			activableList = new List<Activable>();
			activableList.Add(activable);
		}

		public ActivableList(ActivableList activableList)
		{
			this.activableList = activableList.activableList;
		}

		public ActivableList(List<Player> playerList)
		{
			activableList = new List<Activable>();
			foreach (Player player in playerList)
			{
				activableList.Add(player);
			}
		}

		public AddActivable(Activable activable)
		{
			activableList.Add(activable);
		}

		public AddActivable(ActivableList activableList)
		{
			foreach (Activable activable in activableList)
			{
				activableList.Add(activable);
			}
		}

		public AddActivable(List<Player> playerList)
		{
			foreach (Player player in playerList)
			{
				activableList.Add(player);
			}
		}
	}
}