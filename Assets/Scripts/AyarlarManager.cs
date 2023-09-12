using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yilmaz;
public class AyarlarManager : MonoBehaviour
{
    public AudioSource ButonSes;
    public Slider MenuSes;
    public Slider MenuFx;
    public Slider OyunSes;
    VeriYonetim _VeriYonetim = new VeriYonetim();
    List<DilVerileri> _DilOkunanVerileri = new List<DilVerileri>();
    public List<DilVerileri> _DilTercihi = new List<DilVerileri>();
    public TextMeshProUGUI[] Text;
    BellekYonetim _BellekYonetim = new BellekYonetim();
    public TextMeshProUGUI DilText;
    void Start()
    {
        _VeriYonetim.Dil_Load();
        _DilOkunanVerileri = _VeriYonetim.DilVerileriListeyeAktar();
        _DilTercihi.Add(_DilOkunanVerileri[4]);
        DiltercihiYonetimi();

        if (_BellekYonetim.VeriOku_s("Dil") == "TR")
        {
            DilText.text = "TÜRKÇE";
        
        }
        else
        {
            DilText.text = "ÝNGÝLÝZCE";
        
        }
        MenuSes.value = _BellekYonetim.VeriOku_f("MenuSes");
        MenuFx.value = _BellekYonetim.VeriOku_f("MenuFx");
        OyunSes.value = _BellekYonetim.VeriOku_f("OyunSes");
          ButonSes.volume=_BellekYonetim.VeriOku_f("MenuFx");

    }
    public void DilDegis1()
    {
        ButonSes.Play();

        if (_BellekYonetim.VeriOku_s("Dil") == "TR")
        {
            DilText.text = "ÝNGÝLÝZCE";
            _BellekYonetim.VeriKaydet_string("Dil", "EN");
            DiltercihiYonetimi();
        }
        else
        {
            DilText.text = "TÜRKÇE";
            _BellekYonetim.VeriKaydet_string("Dil", "TR");
            DiltercihiYonetimi();
        }
    }
    public void DiltercihiYonetimi()
    {
        if (_BellekYonetim.VeriOku_s("Dil") == "TR")
        {
            for (int i = 0; i < Text.Length; i++)
            {
                Text[i].text = _DilTercihi[0]._Dilverileri_TR[i].Metin;
            }
        }
        else
        {
            for (int i = 0; i < Text.Length; i++)
            {
                Text[i].text = _DilTercihi[0]._Dilverileri_EN[i].Metin;
            }
        }
    }
   

    public void AnaMenuyeDon()
    {
        ButonSes.Play();
        SceneManager.LoadScene(0);
    }
    
    public void SesAyarla(string HangiAyar)
    {
        switch (HangiAyar)
        {
            case "menuses":
                _BellekYonetim.VeriKaydet_float("MenuSes", MenuSes.value);
                break;

            case "menufx":
                _BellekYonetim.VeriKaydet_float("MenuFx", MenuFx.value);
                break;
            case "oyunses":
                _BellekYonetim.VeriKaydet_float("OyunSes", OyunSes.value);
                break;
        }
    }
}
