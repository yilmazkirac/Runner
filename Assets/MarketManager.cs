using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yilmaz;
using UnityEngine.Purchasing;
using Unity.VisualScripting;
using Product = UnityEngine.Purchasing.Product;
using System;
using UnityEngine.SceneManagement;

public class MarketManager : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_ExtensionProvider;
    private static string Puan_250 = "puan250";
    private static string Puan_500 = "puan500";
    private static string Puan_750 = "puan750";
    private static string Puan_1000 = "puan1000";

    VeriYonetim _VeriYonetim = new VeriYonetim();
    BellekYonetim _BellekYonetim = new BellekYonetim();
    List<DilVerileri> _DilOkunanVerileri = new List<DilVerileri>();
    public List<DilVerileri> _DilTercihi = new List<DilVerileri>();
    public Text Text;
    private void Start()
    {

        _VeriYonetim.Dil_Load();
        _DilOkunanVerileri = _VeriYonetim.DilVerileriListeyeAktar();
        _DilTercihi.Add(_DilOkunanVerileri[3]);
        DiltercihiYonetimi();

        if (m_StoreController == null)
        {
            IntitializePurchasing();
        }
    }

    public void UrunAl(string deger)
    {
        BuyProductID(deger);
    }
    public void BuyProductID(string Productid) {
        if (IsInitalized())
        {
            
            Product product = m_StoreController.products.WithID(Productid);
            if (product != null && product.availableToPurchase)
            {
                m_StoreController.InitiatePurchase(product);

            }
            else
            {
                Debug.Log("Satin alirken hata olustu");
            }
        }
        else
        {
            Debug.Log("urun cagirilamiyor");
        }
    }
    public void IntitializePurchasing()
    {
        if (IsInitalized())
        {
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            
            builder.AddProduct(Puan_250,ProductType.Consumable);
            builder.AddProduct(Puan_500, ProductType.Consumable);
            builder.AddProduct(Puan_750, ProductType.Consumable);
            builder.AddProduct(Puan_1000, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
            
            }
    private bool IsInitalized()
    {
        return m_StoreController != null&& m_ExtensionProvider != null;
    }
    public void DiltercihiYonetimi()
    {
        if (_BellekYonetim.VeriOku_s("Dil") == "TR")
            Text.text = _DilTercihi[0]._Dilverileri_TR[0].Metin;
        else
            Text.text = _DilTercihi[0]._Dilverileri_EN[0].Metin;


    }
     
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        if (String.Equals(purchaseEvent.purchasedProduct.definition.id,Puan_250,StringComparison.Ordinal))
        {
            _BellekYonetim.VeriKaydet_int("Puan",_BellekYonetim.VeriOku_i("Puan")+250);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Puan_500, StringComparison.Ordinal))
        {
            _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_i("Puan") + 500);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Puan_750, StringComparison.Ordinal))
        {
            _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_i("Puan") + 750);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Puan_1000, StringComparison.Ordinal))
        {
            _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_i("Puan") + 1000);
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
       m_StoreController = controller;
        m_ExtensionProvider = extensions;
    }
    public void AnaMenuyeDon()
    {
        
        SceneManager.LoadScene(0);
    }
}
