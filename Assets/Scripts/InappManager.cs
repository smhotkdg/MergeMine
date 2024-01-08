using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;
public class InappManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject InappProcess;
    private static InappManager _instance = null;

    public static InappManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton InappManager == null");
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            //
            _instance = this;
        }
    }
    void Start()
    {
        InAppPurchasing.InitializePurchasing();
        InitData();
    }
    void InitData()
    {
        if(InAppPurchasing.IsInitialized() == true)
        {
            IAPProduct[] products = InAppPurchasing.GetAllIAPProducts();

            // Print all product names
            foreach (IAPProduct prod in products)
            {
                Debug.Log("Product name: " + prod.Name);                
            }
        }
        else
        {
            InAppPurchasing.InitializePurchasing();
        }
    }

    // Subscribe to IAP purchase events
    void OnEnable()
    {
        InAppPurchasing.PurchaseCompleted += PurchaseCompletedHandler;
        InAppPurchasing.PurchaseFailed += PurchaseFailedHandler;       
    }

    // Unsubscribe when the game object is disabled
    void OnDisable()
    {
        InAppPurchasing.PurchaseCompleted -= PurchaseCompletedHandler;
        InAppPurchasing.PurchaseFailed -= PurchaseFailedHandler;
    }

    // Purchase the sample product
    public void PurchaseSampleProduct()
    {
        // EM_IAPConstants.Sample_Product is the generated name constant of a product named "Sample Product"
        
    }    
    public void PurchaseGoldBar(int index)
    {        
        switch (index)
        {
            case 0:
                InAppPurchasing.Purchase(EM_IAPConstants.Product_GoldBar_1);
                break;
            case 1:
                InAppPurchasing.Purchase(EM_IAPConstants.Product_GoldBar_2);
                break;
            case 2:
                InAppPurchasing.Purchase(EM_IAPConstants.Product_GoldBar_3);
                break;
        }
        InappProcess.SetActive(true);
        
    }

    public void PurchaseNoAds()
    {
        if (GameManager.Instance.isNoads == true)
            return;
        InAppPurchasing.Purchase(EM_IAPConstants.Product_Noads);
    }
    // Successful purchase handler
    void PurchaseCompletedHandler(IAPProduct product)
    {
        // Compare product name to the generated name constants to determine which product was bought
        InappProcess.SetActive(false);
        switch (product.Name)
        {
            case EM_IAPConstants.Product_GoldBar_1:
                UIManager.Instance.SetBuyComplete("GoldBar1");
                break;
            case EM_IAPConstants.Product_GoldBar_2:
                UIManager.Instance.SetBuyComplete("GoldBar2");
                break;
            case EM_IAPConstants.Product_GoldBar_3:
                UIManager.Instance.SetBuyComplete("GoldBar3");
                break;
            case EM_IAPConstants.Product_Noads:
                UIManager.Instance.SetBuyComplete("Noads");
                GameManager.Instance.isNoads = true;
                break;

                // More products here...
        }
    }

    // Failed purchase handler
    void PurchaseFailedHandler(IAPProduct product)
    {
        InappProcess.SetActive(false);
        Debug.Log("The purchase of product " + product.Name + " has failed.");
    }
    public void RestorePurchases()
    {
#if UNITY_IOS
        InAppPurchasing.RestorePurchases();
#endif
    }
    void Update()
    {
        
    }
}
