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

		public Token(TokenInfo otherInfo)
		{
			tBase = otherInfo.token;
			stack = otherInfo.tokenAmount;
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
}
