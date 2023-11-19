using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnTheRecord.Entity;

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

		public void AddWithoutCalStats(Token t)
		{
			// 나중에 완전히 모든 조건식이 확정이 나면 그에 따라 Stats의 계산의 필요성을 계산할 수 있다.
			// 그런 게 가능해지면 return값을 bool로 변경
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
		}

		public void AddWithoutCalStats(TokenBase tb)
		{
			AddWithoutCalStats(new Token(tb));
		}

		public void AddWithoutCalStats(TokenBase tb, int stack)
		{
			AddWithoutCalStats(new Token(tb, stack));
		}

		public void AddWithoutCalStats(int tokenCode)
		{
			AddWithoutCalStats(new Token(tokenCode));
		}

		public void Add(Token t)
		{
			AddWithoutCalStats(t);
			CalculateStats();
		}

		public void Add(TokenBase tb)
		{
			Add(new Token(tb));
		}

		public void Add(TokenBase tb, int stack)
		{
			Add(new Token(tb, stack));
		}

		public void Add(int tokenCode)
		{
			Add(new Token(tokenCode));
		}

		public void Add(int tokenCode, int stack)
		{
			Add(new Token(tokenCode, stack));
		}

		public void RemoveWithoutCalStats(Token t)
		{
			// 나중에 완전히 모든 조건식이 확정이 나면 그에 따라 Stats의 계산의 필요성을 계산할 수 있다.
			// 그런 게 가능해지면 return값을 bool로 변경
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

		public void RemoveWithoutCalStats(TokenBase tb)
		{
			RemoveWithoutCalStats(new Token(tb));
		}

		public void RemoveWithoutCalStats(TokenBase tb, int stack)
		{
			RemoveWithoutCalStats(new Token(tb, stack));
		}

		public void RemoveWithoutCalStats(int tokenCode)
		{
			RemoveWithoutCalStats(new Token(tokenCode));
		}

		public void Remove(Token t)
		{
			RemoveWithoutCalStats(t);
			CalculateStats();
		}

		public void Remove(TokenBase tb)
		{
			Remove(new Token(tb));
		}

		public void Remove(TokenBase tb, int stack)
		{
			Remove(new Token(tb, stack));
		}

		public void Remove(int tokenCode)
		{
			Remove(new Token(tokenCode));
		}

		public void Remove(int tokenCode, int stack)
		{
			Remove(new Token(tokenCode, stack));
		}

		public void RemoveAtWithoutCalStats(int index)
		{
			_tokenList.RemoveAt(index);
		}

		public void RemoveAt(int index)
		{
			RemoveAtWithoutCalStats(index);
			CalculateStats();
		}


		public void CalculateStats()
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

		public void Situation(int situation, Breakable b)
		{
			float hpDamage = 0;
			float hpHeal = 0;
			//demotion 기능 미구현. 나중에 demotion 기능이 포함된 토큰이 추가된다면 구현
			for (int i = _tokenList.Count - 1; i >= 0; --i)
			{
				if (_tokenList[i].tBase.removeSituation1 == situation
				|| _tokenList[i].tBase.removeSituation2 == situation
				|| _tokenList[i].tBase.removeSituation3 == situation)
				{
					if (_tokenList[i].tBase.hpValueWhenRemove > 0)
						hpHeal += _tokenList[i].tBase.hpValueWhenRemove;
					else
						hpDamage -= _tokenList[i].tBase.hpValueWhenRemove;
					
					if (_tokenList[i].tBase.damageWhenRemove)
						hpDamage += b.CalDamage(_tokenList[i].tBase.damageValueWhenRemove, _tokenList[i].tBase.damageTypeWhenRemove);
					RemoveAtWithoutCalStats(i);
				}
			}
			b.SubHp(hpDamage);
			b.AddHp(hpHeal);
			CalculateStats();
		}

		public Token? GetToken(Token t)
		{
			int i = _tokenList.BinarySearch(t);
			if (i < 0)
				return null;
			else
				return _tokenList[i];
		}

		public Token? GetToken(TokenBase tb)
		{
			return GetToken(new Token(tb));
		}

		public Token? GetToken(int tokenCode)
		{
			return GetToken(new Token(tokenCode));
		}

		public int GetTokenStack(Token t)
		{
			int i = _tokenList.BinarySearch(t);
			if (i < 0)
				return 0;
			else
				return _tokenList[i].stack;
		}

		public int GetTokenStack(TokenBase tb)
		{
			return GetTokenStack(new Token(tb));
		}

		public int GetTokenStack(int tokenCode)
		{
			return GetTokenStack(new Token(tokenCode));
		}
		// 게임 로직에 따라 필요한 메소드들	추가할 것
		// 

	}
}
