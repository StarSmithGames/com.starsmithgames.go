#if PURCHASING
using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

using Zenject;

namespace StarSmithGames.Go.InAppPurchaseService
{
	public class InAppPurchaseService : MonoBehaviour, IDetailedStoreListener, IInAppPurchaseService
	{
		public event Action<string> onPurchased;

		[Inject]
		private List<InAppProduct> products;
		
		private bool isInitialized = false;
		private IStoreController storeController;
		private IExtensionProvider storeExtensionProvider;

		public void Start()
		{
			var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

			foreach (var product in products)
			{
				builder.AddProduct(product.productId, ProductType.Consumable);
			}

			UnityPurchasing.Initialize(this, builder);
		}

		public void BuyProduct(string productId)
		{
			if (isInitialized)
			{
				Product product = storeController.products.WithID(productId);

				if (product != null && product.availableToPurchase)
				{
					Debug.Log(string.Format($"[InAppPurchaseService] Purchasing product {product.definition.id}"));
					storeController.InitiatePurchase(product);
				}
				else
				{
					Debug.LogError($"[InAppPurchaseService] Not purchasing product, either is not found or is not available for purchase. Product:{(product != null)} IsAvailable:{(product?.availableToPurchase)}");
				}
			}
			else
			{
				Debug.LogError("[InAppPurchaseService] Not initialized.");
			}
		}

		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
		{
			string id = args.purchasedProduct.definition.id;
			Debug.Log($"[InAppPurchaseService] ProcessPurchase: PASS. ProductId: {id} StoreId: {args.purchasedProduct.definition.storeSpecificId}");
			onPurchased?.Invoke(id);
			return PurchaseProcessingResult.Complete;
		}

		public void RestorePurchases()
		{
			if (!isInitialized) return;

			if (Application.platform == RuntimePlatform.IPhonePlayer ||
				Application.platform == RuntimePlatform.OSXPlayer)
			{
				var apple = storeExtensionProvider.GetExtension<IAppleExtensions>();
				apple.RestoreTransactions((result, msg) =>
				{
					Debug.Log($"[InAppPurchaseService] RestorePurchases continuing: {result}. If no further messages, no purchases available to restore. msg:{msg}");
				});
			}
			else
			{
				Debug.Log($"[InAppPurchaseService] RestorePurchases not supported on this platform {Application.platform}");
			}
		}

		public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
		{
			storeController = controller;
			storeExtensionProvider = extensions;

			isInitialized = true;

			Debug.Log("[InAppPurchaseService] Initialized.");
		}

		public void OnInitializeFailed(InitializationFailureReason error, string message)
		{
			isInitialized = false;

			Debug.LogError($"[InAppPurchaseService] Initialization failed with error:{error} message:{message}");
		}


		public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
		{
			Debug.LogError($"[InAppPurchaseService] Purchase failed: FAIL. Product: '{product.definition.storeSpecificId}' reason:{failureDescription.reason} description:{failureDescription.message}");
		}

		#region Obsolete
		public void OnInitializeFailed(InitializationFailureReason error) { }

		public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) { }
		#endregion
	}
}
#endif