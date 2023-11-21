using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OnTheRecord.BasicComponent;
using OnTheRecord.Map;
using OnTheRecord.Entity;

namespace OnTheRecord.Control
{
    public class CombatControl
    {
        public Map map;
        public Room currentRoom;
        public List<Player> playerList;
        public List<Activable> ActivableList;

        public CombatControl(Map map, int row, int col, List<Player> playerList)
        {
            this.map = map;
            this.currentRoom = map.GetRoom(row, col);
            this.playerList = playerList;
            this.ActivableList = MakeActivableList();
        }
    }
}