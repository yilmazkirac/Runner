using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yilmaz;

public class Anamenu_Manager : MonoBehaviour
{
    public AudioSource ButonSes;
    public GameObject CikisPaneli;
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetim _VeriYonetim = new VeriYonetim();
    public List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri>();
    void Start()
    {
        _BellekYonetim.KontrolEtTanimla();
        _VeriYonetim.IlkKurulum(_ItemBilgileri);
        ButonSes.volume = _BellekYonetim.VeriOku_f("MenuFx");
    }

    public void SahneYukle(int Index)
    {
        ButonSes.Play();
        SceneManager.LoadScene(Index);
    }

    public void Oyna()
    {
        ButonSes.Play();
        SceneManager.LoadScene(_BellekYonetim.VeriOku_i("SonLevel"));
    }
    public void CikisBtnIslem(string durum)
    {
        ButonSes.Play();
        if (durum == "Evet")
            Application.Quit();

        else if (durum == "Cikis")
            CikisPaneli.SetActive(true);

        else
            CikisPaneli.SetActive(false);
    }
}
