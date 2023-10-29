using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalStaticReference;

/*
 * Item의 기반
 * 장비, 소모품, 재료, 교환품, 재화 전부 이 클래스의 파생 클래스로 처리?
 * 
 * 
 * 속성
 * itemBase
 *	itemType		EnumBox의 ItemType에 포함되는 녀석으로만 처리할 수 있어야할 것
 *	itemCode		아이템 종류별 고유 코드. 같은 코드를 가지면 같은 아이템. 이 코드로 아이템 구분
 *	weight			아이템의 무게.
 * stack		같은 아이템이 얼마나 쌓였는가.
 * 
 * 생성자
 * Item(int itemType, int itemCode, int weight)				stack은 1로 처리
 * Item(int itemType, int itemCode, int weight, int stack)	위와 같고 stack은 입력받은 거로 처리
 * Item(Item source)										stack을 제외한 source 정보 복사. stack은 1로 처리
 * Item(Item source, int stack)								위와 같고 stack은 입력받은 거로 처리
 * 
 * 메소드
 * Equals, CompareTo
 *		다른 Item과의 비교는 itemCode를 이용
 * void AddStack(int stack)
 *		입력받은만큼 스택 증가
 * bool SubStack(int stack)
 *		입력받은만큼 스택 감소
 * bool MoveStack(Item source)
 *		source의 모든 스택을 이동
 *		아이템 코드가 다르면 false 반환
 *		성공 후 true 반환
 * bool MoveStack(Item source, int moveValue)
 *		위와 같으나 moveValue만큼 이동
 *		source의 stack이 moveValue 보다 적은 경우에도 false 반환
 * bool IsEmpty()
 *		stack 이 없으면(== 0) true
 * long TotalWeight()
 *		weight * stack
 * 
 * 2023.10.01
 */

namespace OnTheRecord.BasicComponent
{
	class Item : IComparable<Item>
	{
		readonly ItemType itemType = ItemType.None;
		readonly int itemCode = 0;
		readonly int weight = 0;
		private int stack = 0;

		public bool Equals(Item? other)
		{
			if (other == null)
				return false;
			return (this.itemCode == other.itemCode);
		}

		public int CompareTo(Item? other)
		{
			if (other == null)
				return 0;
			return (this->itemCode - other.itemCode);
		}

		public static bool operator ==(Item a, Item b)
		{
			if (((object)a) == null || ((object)b) == null)
				return Object.Equals(a, b);
			return a.Equals(b);
		}
		public static bool operator != (Item a, Item b)
		{
			if (((object)a) == null || ((object)b) == null)
				return !(Object.Equals(a, b));
			return !(a.Equals(b));
		}

		Item(int itemType, int itemCode, int weight)
		{
			this->itemType = itemType;
			this->itemCode = itemCode;
			this->weight = weight;
			this->stack = 1;
		}

		Item(int itemType, int itemCode, int weight, int stack)
		{
			this->itemType = itemType;
			this->itemCode = itemCode;
			this->weight = weight;
			this->stack = stack;
		}

		Item(Item source)
		{
			this->itemType = source.itemType;
			this->itemCode = source.itemCode;
			this->weight = source.weight;
			this->stack = 1;
		}

		Item(Item source, int stack)
		{
			this->itemType = source.itemType;
			this->itemCode = source.itemCode;
			this->weight = source.weight;
			this->stack = stack;
		}

		void AddStack(int stack)
		{
			this->stack += stack;
		}

		bool SubStack(int stack)
		{
			if (this->stack < stack)
				return false;
			this->stack -= stack;
			return true;
		}

		bool MoveStack(Item source)
		{
			if (this != source)
				return false;
			this->stack += source.stack;
			source.stack = 0;
			return true;
		}

		bool MoveStack(Item source, int moveValue)
		{
			if (this != source || source.stack < moveValue)
				return false;
			this->stack += source.stack;
			source.stack = 0;
			return true;
		}

		bool IsEmpty()
		{
			return (this->stack == 0);
		}

		long TotalWeight()
		{
			return ((long)this->weight * this->stack);
		}
	}
}
