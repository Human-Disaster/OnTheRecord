using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * demotion 기능은 미구현
 */
namespace OnTheRecord.BasicComponent
{
	public class TokenList
	{
		// 특정 토큰들을 찾아서 리턴, 특정 토큰 추가, 제거, 토큰의 적용 그리고 앞의 함수들이 복합적으로 일어나는 함수들
		private List<Token> _tokenList = new List<Token>();
		private CalStats _sumTokenStats = new CalStats();
		public ref readonly CalStats sumTokenStats => ref _sumTokenStats;
		private CalStats _mulTokenStats = new CalStats();
		public ref readonly CalStats mulTokenStats => ref _mulTokenStats;
		private CalStats _secondMulTokenStats = new CalStats();
		public ref readonly CalStats secondMulTokenStats => ref _secondMulTokenStats;

		public TokenList()
		{
		}

		public void Add(Token t)
		{
			int i = _tokenList.BinarySearch(t);
			if (i < 0)
				_tokenList.Insert(~i, t);
			else
				_tokenList[i].stack += t.stack;
			if (t.tBase.overlapMax < _tokenList[i].stack)
			{
				if (t.tBase.promotionToken.targetCode == 0)
					_tokenList[i].stack = t.tBase.overlapMax;
				else
				{
					this.Add(new Token(t.tBase.promotionToken.token, _tokenList[i].stack / (t.tBase.overlapMax + 1)));
					_tokenList[i].stack %= (t.tBase.overlapMax + 1);
					if (_tokenList[i].stack == 0)
						_tokenList.RemoveAt(i);
				}
			}
			_tokenList.Sort();
			CalculateStats();
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
			CalculateStats();
		}

		public void Remove(TokenBase tb)
		{
			this.Remove(new Token(tb));
		}

		public void Remove(TokenBase tb, int stack)
		{
			this.Remove(new Token(tb, stack));
		}

		private void CalculateStats()
		{
			_sumTokenStats = new CalStats();
			_mulTokenStats = new CalStats();
			_secondMulTokenStats = new CalStats();
			foreach (Token t in _tokenList)
			{
				_sumTokenStats += t.tBase.addStats;
				_mulTokenStats += t.tBase.mulStats * t.stack;
				_secondMulTokenStats += t.tBase.secondMulStats * t.stack;
			}
		}

		// 게임 로직에 따라 필요한 메소드들	추가할 것
		// 

	}
}
