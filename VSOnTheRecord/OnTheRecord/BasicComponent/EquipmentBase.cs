using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRecord.BasicComponent
{
	public class EquipmentBase : Base
	{
		public readonly int specialCode;	// 특수 매커니즘 코드
		public readonly int equipmentType => _baseCode / 100000 % 10;
		public readonly int addStatsCode;
		private StatsBase? _addStats = null;
		public ref readonly StatsBase? addStats => ref _addStats;
		public readonly int mulStatsCode;
		private StatsBase? _mulStats = null;
		public ref readonly StatsBase? mulStats => ref _mulStats;
		public readonly int armsCode;
		private ArmBase? _arm = null;
		public ref readonly ArmBase? arm => ref _arm;

		public EquipmentBase(string str) : base(str)
		{
			string[] values = base.Parse(str);
			specialCode = base.BaseIntParse(values[1]);
			addStatsCode = base.BaseIntParse(values[2]);
			mulStatsCode = base.BaseIntParse(values[3]);
			armsCode = base.BaseIntParse(values[4]);
		}

		public void SetAddStats(StatsBase stats)
		{
			_addStats = stats;
		}

		public void SetMulStats(StatsBase stats)
		{
			_mulStats = stats;
		}

		public void SetArms(ArmBase arm)
		{
			_arm = arm;
		}
	}
}
