using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yilmaz;

public class Anamenu_Manager : MonoBehaviour
{
    public GameObject CikisPaneli;
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetim _VeriYonetim = new VeriYonetim();   
    public List<ItemBilgileri> _ItemBilgileri= new List<ItemBilgileri>();
    void Start()
    {
        _BellekYonetim.KontrolEtTanimla();
      //  _VeriYonetim.IlkKurulum(_ItemBilgileri);
    }
    public void SahneYukle(int Index)
    {
        SceneManager.LoadScene(Index);
    }

    public void Oyna()
    {      
        SceneManager.LoadScene(_BellekYonetim.VeriOku_i("SonLevel"));
    }
    public void CikisBtnIslem(string durum)
    {
        if (durum == "Evet")
            Application.Quit();

       else if (durum == "Cikis")
            CikisPaneli.SetActive(true);

        else
            CikisPaneli.SetActive(false);
    }
}
