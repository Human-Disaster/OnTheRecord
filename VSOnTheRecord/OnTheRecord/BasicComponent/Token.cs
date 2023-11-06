using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  미완 이런 느낌이다 참고만 할 것
*/
namespace OnTheRecord.BasicComponent
{
	public class Token : IComparable
	{
		public readonly TokenBase tBase;
		public int stack = 0; // 토큰의 수량

		public Token(int tokenCode)
		{
			tBase = OnMemoryTable.Instance().GetTokenBase(tokenCode);
			stack = 1;
		}

		public Token(int tokenCode, int stack)
		{
			tBase = OnMemoryTable.Instance().GetTokenBase(tokenCode);
			this.stack = stack;
		}

		public Token(TokenBase otherBase)
		{
			tBase = otherBase;
			stack = 1;
		}

		public Token(TokenBase otherBase, int stack)
		{
			tBase = otherBase;
			this.stack = stack;
		}

		public Token(Token other)
		{
			tBase = other.tBase;
			stack = other.stack;
		}
		
		public Token(Token other, int stack)
		{
			tBase = other.tBase;
			this.stack = stack;
		}

		public int CompareTo(object? obj)
		{
			if (obj == null) return 1;
			Token? otherToken = obj as Token;
			if (otherToken is not null)
				return this.tBase.CompareTo(otherToken.tBase);
			else
				throw new ArgumentException("Object is not a Token");
		}

		public void Token_situation_check()
		{
			//todo
		}

		public void Token_out_check()
		{
			//todo
		}
	}

	public class TokenList // 토큰 관련 함수 전부 여기 넣을것
	{
		// 특정 토큰들을 찾아서 리턴, 특정 토큰 추가, 제거, 토큰의 적용 그리고 앞의 함수들이 복합적으로 일어나는 함수들
		private List<Token> _tokenList = new List<Token>();

		public void Add(Token t)
		{
			int i = _tokenList.BinarySearch(t);
			if (i < 0)
				_tokenList.Insert(~i, t);
			else
				_tokenList[i].stack += t.stack;
			if (t.tBase.overlapMax < _tokenList[i].stack)
			{
				_tokenList[i].stack %= (t.tBase.overlapMax + 1);
				if (_tokenList[i].stack == 0)
					_tokenList.RemoveAt(i);
				this.Add(new Token(t.tBase.promotionToken, _tokenList[i].stack / (t.tBase.overlapMax + 1)));
			}
			_tokenList.Sort();
		}

		public void Add(TokenBase tb)
		{
			this.Add(new Token(tb));
		}

		public void Remove(Token t)
		{
			int i = _tokenList.BinarySearch(t);
			if (i < 0)
				return;
			else
			{
				_tokenList[i].stack -= t.stack;
				if (_tokenList[i].stack <= 0)
					_tokenList.RemoveAt(i);
			}
		}

		// 게임 로직에 따라 필요한 메소드들	추가할 것
		// 
	}
}
