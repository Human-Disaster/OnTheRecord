using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalStaticReference;
using OnTheRecord.BasicComponent;

namespace OnTheRecord.Entity
{
    class Activable : Breakable
    {
        public ActiveSkill?[] activeSkills;
        public float sanity;
        public float ap;
        public Activable(StatsBase origin, int camp) : base(origin, camp)
        {
            activeSkills = Array.Empty<ActiveSkill?>();
            sanity = origin.sanMaxS;
            ap = origin.apMaxS;
        }


    }

}