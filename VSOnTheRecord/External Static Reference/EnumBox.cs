using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalStaticReference
{
    public enum ActivableType
    {
        None,
        Player = 1,
        Hostile = 2

    }

    public enum PlayerType
    {
        None = 0,
        Noble = 1,
        Soldier = 2
    }

    public enum HostileType
    {
        None = 0,
        BC_Conscript = 101,
        BC_Rifleman = 102,
        BC_Veterance = 103,
        BC_Assult = 201,
        BC_Grenadier = 202,
        BC_Shocktroop = 203
    }

    public enum Stituation//게임 탐험, 보스전 전체의 상황을 카테고리화 시켜놓음
    {
        None,
        Eneter_Dungeon = 001,
        Enter_Room = 002,
        Enter_Incounter = 003,

        Take_dmg = 101,
        Take_sanitydmg = 102,
        Take_elementdmg = 103,
        Take_buff = 104,
        Take_heal = 105,
        Take_stun = 106,
        Take_confuse = 107,
        Take_doge = 108,
        Take_miss = 109,
        Take_dowm = 110,

        /*
        Take_useact = 1??,
        Take_notuseact = 1??,
        */

        Do_dmg = 201,
        Do_sanitydmg = 202,
        Do_elementdmg = 203,
        Do_buff = 204,
        Do_heal = 205,
        Do_stun = 206,
        Do_confuse = 207,
        Do_doge = 208,
        Do_miss = 209,
        Do_dowm = 210,

        /*
        Do_useact = 2??,
        Do_notuseact = 2??,
        */

        Sqaud_down = 301,
        Sqaud_notuseact = 302,
        Sqaud_stun = 303,

        Hostile_down = 401

    }




}
