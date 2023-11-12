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
 *		다른 Item과의 비교는 itemBase를 이용
 * int AddStack(int stack)
 *		입력받은만큼 스택 증가
 *		스택이 maxStack을 넘어가면 maxStack까지만 증가하고 나머지는 반환
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
 * 2023.11.12
 */

namespace OnTheRecord.BasicComponent
{
	public class Item : IComparable
	{
		readonly public ItemBase itemBase;
		private int _stack;
		public readonly int stack => _stack;

		public Item(ItemBase ib)
		{
			this.itemBase = ib;
			_stack = 1;
		}

		public Item(ItemBase ib, int stack)
		{
			this.itemBase = ib;
			this._stack = stack;
		}

		public Item(Item source)
		{
			this.itemBase = source.itemBase;
			_stack = source._stack;
		}

		public Item(Item source, int stack)
		{
			this.itemBase = source.itemBase;
			this._stack = stack;
		}

		public Item(int itemCode)
		{
			this.itemBase = OnMemoryTable.Instance().GetItemBase(itemCode);
			_stack = 1;
		}

		public Item(int itemCode, int stack)
		{
			this.itemBase = OnMemoryTable.Instance().GetItemBase(itemCode);
			this._stack = stack;
		}

		override public bool Equals(object? obj)
		{
			if (obj == null)
				return false;
			Item? otherItem = obj as Item;
			if (otherItem is not null)
				return this.itemBase.Equals(otherItem.itemBase);
			ItemBase? otherItemBase = obj as ItemBase;
			if (otherItemBase is not null)
				return this.itemBase.Equals(otherItemBase);
			else
				return false;
		}

		public int CompareTo(object? obj)
		{
			if (obj == null) return 1;
			Item? otherItem = obj as Item;
			if (otherItem is not null)
				return this.itemBase.CompareTo(otherItem.itemBase);
			else
				throw new ArgumentException("Object is not a Item");
		}

		public static bool operator ==(Item a, Item b)
		{
			if (((object)a) == null || ((object)b) == null)
				return Object.Equals(a, b);
			return a.Equals(b);
		}
		public static bool operator !=(Item a, Item b)
		{
			if (((object)a) == null || ((object)b) == null)
				return !(Object.Equals(a, b));
			return !(a.Equals(b));
		}

		public int AddStack(int stack)
		{
			this._stack += stack;
			if (this._stack > itemBase.stackMax)
			{
				stack = this._stack - itemBase.stackMax;
				this._stack = itemBase.stackMax;
				return stack;
			}
			return 0;
		}

		public bool SubStack(int stack)
		{
			if (this._stack < stack)
				return false;
			this._stack -= stack;
			return true;
		}

		public bool MoveStack(Item source)
		{
			if (this != source)
				return false;
			this._stack += source._stack;
			if (this._stack > itemBase.stackMax)
			{
				source._stack = this._stack - itemBase.stackMax;
				this._stack = itemBase.stackMax;
				return true;
			}
			source._stack = 0;
			return true;
		}

		public bool MoveStack(Item source, int moveValue)
		{
			if (this != source || source._stack < moveValue)
				return false;
			this._stack += moveValue;
			source._stack -= moveValue;
			if (this._stack > itemBase.stackMax)
			{
				source._stack += this._stack - itemBase.stackMax;
				this._stack = itemBase.stackMax;
				return true;
			}
			return true;
		}

		public bool IsEmpty()
		{
			return (this._stack == 0);
		}

		public long TotalWeight()
		{
			return ((long)this.itemBase.weight * this._stack);
		}
	}
}
