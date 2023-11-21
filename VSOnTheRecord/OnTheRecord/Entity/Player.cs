using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnTheRecord.BasicComponent;

namespace OnTheRecord.Entity
{

    public class Player : Activable
    {
        private int level;

        public Player(
            CalStats basicClassBaseStats,
            CalStats basicClassGrowthStats,
            /*
            심화 클래스 나온 이후 처리
            StatsBase advancedClassBaseStats,
            StatsBase advancedClassGrowthStats,
            CalStats traitsStats,
            */
            int level,
            int camp) : base(basicClassBaseStats + (basicClassGrowthStats * (level - 1)), camp)
        {
            this.level = level;
        }

        public Player(
            CalStats basicClassBaseStats,
            CalStats basicClassGrowthStats,
            /*
            심화 클래스 나온 이후 처리
            StatsBase advancedClassBaseStats,
            StatsBase advancedClassGrowthStats,
            CalStats traitsStats,
            */
            int level,
            int camp,
            int a) : base(basicClassBaseStats + (basicClassGrowthStats * (level - 1)), camp, a)
        {
            this.level = level;
        }
    }
}