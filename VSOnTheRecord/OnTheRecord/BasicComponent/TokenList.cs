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

		public void Add(Token t)
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
			CalculateStats();
		}

		public void Add(TokenBase tb)
		{
			this.Add(new Token(tb));
		}

		public void Add(TokenBase tb, int stack)
		{
			this.Add(new Token(tb, stack));
		}

		public void Add(int tokenCode)
		{
			this.Add(new Token(tokenCode));
		}

		public void Add(int tokenCode, int stack)
		{
			this.Add(new Token(tokenCode, stack));
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

		public void Remove(Token t)
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

		public void Remove(TokenBase tb)
		{
			this.Remove(new Token(tb));
		}

		public void Remove(TokenBase tb, int stack)
		{
			this.Remove(new Token(tb, stack));
		}

		public void Remove(int tokenCode)
		{
			this.Remove(new Token(tokenCode));
		}

		public void Remove(int tokenCode, int stack)
		{
			this.Remove(new Token(tokenCode, stack));
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

		public void Stituation(int situation, Breakable b)
		{
			float hpDamage = 0;
			float hpHeal = 0;
			//demotion 기능 미구현. 나중에 demotion 기능이 포함된 토큰이 추가된다면 구현
			foreach (Token t in _tokenList)
			{
				if (t.tBase.removeSituation1 == situation
				|| t.tBase.removeSituation2 == situation
				|| t.tBase.removeSituation3 == situation)
				{
					if (t.tBase.hpValueWhenRemove > 0)
						hpHeal += t.tBase.hpValueWhenRemove;
					else
						hpDamage -= t.tBase.hpValueWhenRemove;
					
					if (t.tBase.damageWhenRemove)
						hpDamage += b.CalDamage(t.tBase.damageValueWhenRemove, t.tBase.damageTypeWhenRemove);
					
				}
			}
		}

		public Token? GetTokenBase(Token t)
		{
			int i = _tokenList.BinarySearch(t);
			if (i < 0)
				return null;
			else
				return _tokenList[i];
		}

		public Token? GetTokenBase(TokenBase tb)
		{
			return GetTokenBase(new Token(tb));
		}

		public Token? GetTokenBase(int tokenCode)
		{
			return GetTokenBase(new Token(tokenCode));
		}
		// 게임 로직에 따라 필요한 메소드들	추가할 것
		// 

	}
}
