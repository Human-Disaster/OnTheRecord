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
        public int sanity;
        public int ap;
        public Activable(StatsBase origin, )
        {

        }


    }

}