using OnTheRecord.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalStaticReference;
using OnTheRecord.Map;
using System.ComponentModel.Design;

namespace OnTheRecord.BasicComponent
{
	public class TokenInfo
	{
		public readonly int _tokenCode;
		private TokenBase? _token = null;
		public ref readonly TokenBase? token => ref _token;
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

		public bool TokenInfoCheckTarget(Activable user, Breakable target, Tile tile)
		{
			if (_token is null)
				return false;
			if (targetCode / 100 % 100 == (int)TargetPreCode.Target &&
				target != tile.GetEntity())
				return false;
			else if (targetCode / 100 % 100 == (int)TargetPreCode.Area)
				switch (targetCode % 100)
				{
					case (int)TargetPostCode.Self:
						if (user != target)
							return false;
						break;
					case (int)TargetPostCode.Squadmate:
						if (user == target || user.camp != target.camp)
							return false;
						break;
					case (int)TargetPostCode.Squad:
						if (user.camp != target.camp)
							return false;
						break;
					case (int)TargetPostCode.Enemy:
					case (int)TargetPostCode.AllEnemy:
						if (user.camp == target.camp)
							return false;
						break;
					default:
						break;
				}
			return true;
		}
	}
}
