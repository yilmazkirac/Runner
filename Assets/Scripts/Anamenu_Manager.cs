using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yilmaz;

public class Anamenu_Manager : MonoBehaviour
{
    public AudioSource ButonSes;
    public GameObject CikisPaneli;
  
    BellekYonetim _BellekYonetim = new BellekYonetim();
    ReklamYonetimi _ReklamYonetimi = new ReklamYonetimi();
    VeriYonetim _VeriYonetim = new VeriYonetim();
    public List<ItemBilgileri> _Varsayilan_ItemBilgileri = new List<ItemBilgileri>();


    public List<DilVerileri> _Varsayilan_DilVerileri = new List<DilVerileri>();
     List<DilVerileri> _DilOkunanVerileri = new List<DilVerileri>();
    public List<DilVerileri> _DilTercihi = new List<DilVerileri>();

    public Slider LoadSlider;
    public GameObject LoadingScene;
    public Text[] Text;
    void Start()
    {
        _BellekYonetim.KontrolEtTanimla();
        _VeriYonetim.IlkKurulum(_Varsayilan_ItemBilgileri,_Varsayilan_DilVerileri);
        ButonSes.volume = _BellekYonetim.VeriOku_f("MenuFx");
        //  _BellekYonetim.VeriKaydet_string("Dil", "EN");
        
        _VeriYonetim.Dil_Load();
        _DilOkunanVerileri = _VeriYonetim.DilVerileriListeyeAktar();
        _DilTercihi.Add(_DilOkunanVerileri[0]);
        DiltercihiYonetimi();

       

    }
    public void DiltercihiYonetimi()
    {
        if (_BellekYonetim.VeriOku_s("Dil")=="TR")
        {
            for (int i = 0; i < Text.Length; i++)
            {
                Text[i].text= _DilTercihi[0]._Dilverileri_TR[i].Metin;
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
    public void SahneYukle(int Index)
    {
        ButonSes.Play();
        SceneManager.LoadScene(Index);
    }
    IEnumerator LoadAsync(int SceneIndex)
    {
        AsyncOperation Op = SceneManager.LoadSceneAsync(SceneIndex);
        LoadingScene.SetActive(true);
        while (!Op.isDone)
        {
            float progress = Mathf.Clamp01(Op.progress / .9f);
            LoadSlider.value = progress;
            yield return null;  
        }
    }
    public void Oyna()
    {
        ButonSes.Play();   
        StartCoroutine(LoadAsync(_BellekYonetim.VeriOku_i("SonLevel")));
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
