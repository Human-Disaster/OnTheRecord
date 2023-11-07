using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheRecord.BasicComponent
{
	public class TokenInfo
	{
		public readonly int _tokenCode;
		private TokenBase? _token = null;
		public ref readonly TokenBase token => ref _token;
		public readonly int tokenAmount;
		public readonly int targetCode;

		public TokenInfo(int tokenCode, int tokenAmount, int targetCode)
		{
			_tokenCode = tokenCode;
			this.tokenAmount = tokenAmount;
			this.targetCode = targetCode;
		}

		public void SetToken(TokenBase token)
		{
			_token = token;
		}
	}
}
