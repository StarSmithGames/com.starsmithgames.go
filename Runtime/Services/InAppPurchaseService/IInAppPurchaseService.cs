#if PURCHASING
using System;

namespace StarSmithGames.Go.InAppPurchaseService
{
	public interface IInAppPurchaseService
	{
		event Action<string> onPurchased;

		void BuyProduct(string productId);

		void RestorePurchases();
	}
}
#endif