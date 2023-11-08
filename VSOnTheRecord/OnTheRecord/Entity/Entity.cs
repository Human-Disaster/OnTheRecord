using BasicComponent;

namespace OnTheRecord.Entity
{
	public class Entity
	{
		public CalStat baseStats;
		public CalStat tokenStats;
		public CalStat itemStats;
		public CalStat skillStats;
		//현 체력, 정신력, 행동력 등을 표기하기 위한 클래스 필요

		public TokenList tokenList;
		public ItemList itemList;
		public SkillList activeList;
		public SkillList passiveList;


	}
}