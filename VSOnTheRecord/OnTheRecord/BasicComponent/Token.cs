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
		TokenBase tokenBase;
		public int stack = 0; // 토큰의 수량

		Token(int tokenCode)
		{
			tokenBase = OnMemoryTable.Instance().GetTokenBase(tokenCode);
			stack = 1;
		}

		public int CompareTo(object? obj)
		{
			if (obj == null) return 1;
			Token? otherToken = obj as Token;
			if (otherToken is not null)
				return this.tokenBase.CompareTo(otherToken.tokenBase);
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
			{
				_tokenList.Insert(~i, t);
				_tokenList.Sort();
			}
			else
			{
			//존재하면, t의 리스트에 존재하는 토큰스택에 더해준다.
			//todo
				_tokenList[i].stack += t.stack;
			}
		}

		public void Add(int token_type, int stack_weight)
		{
			//Token_type을 기준으로 토큰리스트를 검색
			//존재하면, stack_weight의 리스트에 존재하는 토큰스택에 더해준다.
			//존재하지 않으면, 토큰리스트에 토큰을 생성해서 add해 주고, sort.
		}

		public void Remove(T item);

		private void Sort()
		{
			token_list.Sort(delegate (Token x, Token y)
			{
				if (x.TokenType == 0 && y.TokenType == 0) return 0; // to do https://learn.microsoft.com/ko-kr/dotnet/api/system.collections.generic.list-1?view=net-6.0 참고 
				else if (x.TokenType == 0) return -1;
				else if (y.TokenType == 0) return 1;
				else return x.TokenType.CompareTo(y.TokenType);
			});
		}

		/* parts.Sort(delegate (Part x, Part y)
	   {
		   if (x.PartName == null && y.PartName == null) return 0;
		   else if (x.PartName == null) return -1;
		   else if (y.PartName == null) return 1;
		   else return x.PartName.CompareTo(y.PartName);
	   }*/

		public void Find_harm()
		{

		}

		public void Find_buff()
		{

		}


	}
}
