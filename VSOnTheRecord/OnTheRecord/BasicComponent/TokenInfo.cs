using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRecord.BasicComponent
{
	public class TokenInfo
	{
		public readonly bool tokenWhether;
		private readonly int _tokenCode;
		private TokenBase? _token = null;
		public ref readonly TokenBase token => ref _token;
		public readonly int tokenAmount;

		public TokenInfo(bool tokenWhether, int tokenCode, int tokenAmount)
		{
			this.tokenWhether = true;
			_tokenCode = tokenCode;
			this.tokenAmount = tokenAmount;
		}

		public void SetToken(TokenBase token)
		{
			_token = token;
		}
	}
}
