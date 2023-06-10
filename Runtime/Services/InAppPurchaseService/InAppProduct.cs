#if PURCHASING
using UnityEngine.Purchasing;

namespace StarSmithGames.Go.InAppPurchaseService
{
	[System.Serializable]
	public class InAppProduct
	{
		public string productId;

		public ProductType productType;
	}
}
#endif