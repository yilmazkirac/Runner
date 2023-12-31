using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yilmaz;

public interface IOzellestimeManager
{
    void AnaMenuyeDon();
    void GeriDon();
    void IslemPaneliCikar(int index);
    void Kaydet();
    void Material_Butonlari(string islem);
    void Sapka_Butonlari(string islem);
    void Satinal();
    void Sopa_Butonlari(string islem);
}

public class OzellestimeManager : MonoBehaviour, IOzellestimeManager
{

    [Header("-----Sapkalar")]
    public GameObject[] Sapkalar;
    public Button[] SapkaButonlari;
    public Text SapkaText;
    int SapkaIndex = -1;

    [Header("-----Sopalar")]
    public GameObject[] Sopalar;
    public Button[] SopaButonlari;
    public Text SopaText;
    int SopaIndex = -1;

    [Header("-----Materialler")]
    public Material[] Materialler;
    public Material DefoultTema;
    public Button[] MaterialButonlari;
    public Text MaterialText;
    int MaterialIndex = -1;
    public SkinnedMeshRenderer _Renderer;

    [Header("Digerleri")]
    public GameObject[] IslemPanalleri;
    public GameObject IslemCanvas;
    public GameObject[] GenelPanaller;
    public Button[] IslemButonlari;

    public Text PuanText;
    public List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri>();
    public Animator Kaydedildi_Animator;

    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetim _VeriYonetim = new VeriYonetim();
    public AudioSource[] ButonSes;
    int IslemPanelindex;

    List<DilVerileri> _DilOkunanVerileri = new List<DilVerileri>();
    public List<DilVerileri> _DilTercihi = new List<DilVerileri>();
    public Text[] Text;
    string ItemText;
    string SatinalmaText;
    private void Start()
    {
        /*_BellekYonetim.VeriKaydet_int("AktifSapka", -1);
         _BellekYonetim.VeriKaydet_int("AktifSopa", -1);
         _BellekYonetim.VeriKaydet_int("AktifTema", -1);*/
        // _VeriYonetim.Save(_ItemBilgileri);
        _VeriYonetim.Load();
        _ItemBilgileri = _VeriYonetim.ListeyeAktar();
        DurumuKontrolEt(0, true);
        DurumuKontrolEt(1, true);
        DurumuKontrolEt(2, true);  
        foreach (var item in ButonSes)
        {
            item.volume = _BellekYonetim.VeriOku_f("MenuFx");
        }
  

        //    _BellekYonetim.VeriKaydet_int("Puan", 1500);
        PuanText.text = _BellekYonetim.VeriOku_i("Puan").ToString();

        _VeriYonetim.Dil_Load();
        _DilOkunanVerileri = _VeriYonetim.DilVerileriListeyeAktar();
        _DilTercihi.Add(_DilOkunanVerileri[1]);
        DiltercihiYonetimi();

    }
    public void DiltercihiYonetimi()
    {
        if (_BellekYonetim.VeriOku_s("Dil") == "TR")
        {
            for (int i = 0; i < Text.Length; i++)
            {
                Text[i].text = _DilTercihi[0]._Dilverileri_TR[i].Metin;
            }
            ItemText= _DilTercihi[0]._Dilverileri_TR[4].Metin;
            SatinalmaText = _DilTercihi[0]._Dilverileri_TR[5].Metin;
        }
        else
        {
            for (int i = 0; i < Text.Length; i++)
            {
                Text[i].text = _DilTercihi[0]._Dilverileri_EN[i].Metin;
            }
            ItemText = _DilTercihi[0]._Dilverileri_EN[4].Metin;
            SatinalmaText = _DilTercihi[0]._Dilverileri_EN[5].Metin;
        }
    }
    public void Sapka_Butonlari(string islem)
    {
        ButonSes[0].Play();
        if (islem == "ileri")
        {
            if (SapkaIndex == -1)
            {
                SapkaIndex = 0;
                Sapkalar[SapkaIndex].SetActive(true);
                SapkaText.text = _ItemBilgileri[SapkaIndex].ItemAd;

                if (!_ItemBilgileri[SapkaIndex].SatinAlmaDurumu)
                {
                    Text[5].text = _ItemBilgileri[SapkaIndex].Puan + "-"+SatinalmaText;
                    IslemButonlari[1].interactable = false;
                    if (_BellekYonetim.VeriOku_i("Puan") < _ItemBilgileri[SapkaIndex].Puan)
                        IslemButonlari[0].interactable = false;
                    else
                        IslemButonlari[0].interactable = true;


                }
                else
                {
                    Text[5].text = SatinalmaText;
                    IslemButonlari[0].interactable = false;
                    IslemButonlari[1].interactable = true;
                }
            }
            else
            {
                Sapkalar[SapkaIndex].SetActive(false);
                SapkaIndex++;
                Sapkalar[SapkaIndex].SetActive(true);
                SapkaText.text = _ItemBilgileri[SapkaIndex].ItemAd;
                if (!_ItemBilgileri[SapkaIndex].SatinAlmaDurumu)
                {
                    Text[5].text = _ItemBilgileri[SapkaIndex].Puan + "-"+SatinalmaText;
                    IslemButonlari[1].interactable = false;
                    if (_BellekYonetim.VeriOku_i("Puan") < _ItemBilgileri[SapkaIndex].Puan)
                        IslemButonlari[0].interactable = false;
                    else
                        IslemButonlari[0].interactable = true;
                }
                else
                {
                    Text[5].text = SatinalmaText;
                    IslemButonlari[0].interactable = false;
                    IslemButonlari[1].interactable = true;
                }
            }

            //-------------------------------------------

            if (SapkaIndex == Sapkalar.Length - 1)
            {
                SapkaButonlari[1].interactable = false;
            }
            else
            {
                SapkaButonlari[1].interactable = true;
            }
            if (SapkaIndex != -1)
            {
                SapkaButonlari[0].interactable = true;
            }
        }
        else
        {
            if (SapkaIndex != -1)
            {
                Sapkalar[SapkaIndex].SetActive(false);
                SapkaIndex--;
                if (SapkaIndex != -1)
                {
                    Sapkalar[SapkaIndex].SetActive(true);
                    SapkaButonlari[0].interactable = true;
                    SapkaText.text = _ItemBilgileri[SapkaIndex].ItemAd;
                    if (!_ItemBilgileri[SapkaIndex].SatinAlmaDurumu)
                    {
                        Text[5].text = _ItemBilgileri[SapkaIndex].Puan + "-"+SatinalmaText;
                        IslemButonlari[1].interactable = false;
                        if (_BellekYonetim.VeriOku_i("Puan") < _ItemBilgileri[SapkaIndex].Puan)
                            IslemButonlari[0].interactable = false;
                        else
                            IslemButonlari[0].interactable = true;
                    }
                    else
                    {
                        Text[5].text = SatinalmaText;
                        IslemButonlari[0].interactable = false;
                        IslemButonlari[1].interactable = true;
                    }
                }
                else
                {
                    SapkaButonlari[0].interactable = false;
                    SapkaText.text = ItemText;
                    Text[5].text = SatinalmaText;
                    IslemButonlari[0].interactable = false;

                }

            }
            else
            {
                SapkaButonlari[0].interactable = false;
                SapkaText.text = ItemText;
                Text[5].text = SatinalmaText;
                IslemButonlari[0].interactable = false;

            }

            //-------------------------------------------
            if (SapkaIndex != Sapkalar.Length - 1)
            {
                SapkaButonlari[1].interactable = true;
            }

        }
    }
    public void Sopa_Butonlari(string islem)
    {
        ButonSes[0].Play();
        if (islem == "ileri")
        {
            if (SopaIndex == -1)
            {
                SopaIndex = 0;
                Sopalar[SopaIndex].SetActive(true);
                SopaText.text = _ItemBilgileri[SopaIndex + 3].ItemAd;

                if (!_ItemBilgileri[SopaIndex + 3].SatinAlmaDurumu)
                {
                    Text[5].text = _ItemBilgileri[SopaIndex + 3].Puan + "-"+SatinalmaText;
                    IslemButonlari[1].interactable = false;
                    if (_BellekYonetim.VeriOku_i("Puan") < _ItemBilgileri[SopaIndex + 3].Puan)
                        IslemButonlari[0].interactable = false;
                    else
                        IslemButonlari[0].interactable = true;
                }
                else
                {
                    Text[5].text = SatinalmaText;
                    IslemButonlari[0].interactable = false;
                    IslemButonlari[1].interactable = true;
                }
            }
            else
            {
                Sopalar[SopaIndex].SetActive(false);
                SopaIndex++;
                Sopalar[SopaIndex].SetActive(true);
                SopaText.text = _ItemBilgileri[SopaIndex + 3].ItemAd;

                if (!_ItemBilgileri[SopaIndex + 3].SatinAlmaDurumu)
                {
                    Text[5].text = _ItemBilgileri[SopaIndex + 3].Puan + "-"+SatinalmaText;
                    IslemButonlari[1].interactable = false;
                    if (_BellekYonetim.VeriOku_i("Puan") < _ItemBilgileri[SopaIndex + 3].Puan)
                        IslemButonlari[0].interactable = false;
                    else
                        IslemButonlari[0].interactable = true;
                }
                else
                {
                    Text[5].text = SatinalmaText;
                    IslemButonlari[0].interactable = false;
                    IslemButonlari[1].interactable = true;
                }
            }

            //-------------------------------------------

            if (SopaIndex == Sopalar.Length - 1)
            {
                SopaButonlari[1].interactable = false;
            }
            else
            {
                SopaButonlari[1].interactable = true;
            }
            if (SopaIndex != -1)
            {
                SopaButonlari[0].interactable = true;
            }
        }
        else
        {
            if (SopaIndex != -1)
            {
                Sopalar[SopaIndex].SetActive(false);
                SopaIndex--;
                if (SopaIndex != -1)
                {
                    Sopalar[SopaIndex].SetActive(true);
                    SopaButonlari[0].interactable = true;
                    SopaText.text = _ItemBilgileri[SopaIndex + 3].ItemAd;
                    if (!_ItemBilgileri[SopaIndex + 3].SatinAlmaDurumu)
                    {
                        Text[5].text = _ItemBilgileri[SopaIndex + 3].Puan + "-"+SatinalmaText;
                        IslemButonlari[1].interactable = false;
                        if (_BellekYonetim.VeriOku_i("Puan") < _ItemBilgileri[SopaIndex + 3].Puan)
                            IslemButonlari[0].interactable = false;
                        else
                            IslemButonlari[0].interactable = true;
                    }
                    else
                    {
                        Text[5].text = SatinalmaText;
                        IslemButonlari[0].interactable = false;
                        IslemButonlari[1].interactable = true;
                    }
                }
                else
                {
                    SopaButonlari[0].interactable = false;
                    SopaText.text = ItemText;
                    Text[5].text = SatinalmaText;
                    IslemButonlari[0].interactable = false;

                }

            }
            else
            {
                SopaButonlari[0].interactable = false;
                SopaText.text = ItemText;
                Text[5].text = SatinalmaText;
                IslemButonlari[0].interactable = false;

            }

            //-------------------------------------------
            if (SopaIndex != Sopalar.Length - 1)
            {
                SopaButonlari[1].interactable = true;
            }

        }
    }
    public void Material_Butonlari(string islem)
    {
        ButonSes[0].Play();
        if (islem == "ileri")
        {
            if (MaterialIndex == -1)
            {
                MaterialIndex = 0;
                Material[] mats = _Renderer.materials;
                mats[0] = Materialler[MaterialIndex];
                _Renderer.materials = mats;
                MaterialText.text = _ItemBilgileri[MaterialIndex + 6].ItemAd;

                if (!_ItemBilgileri[MaterialIndex + 6].SatinAlmaDurumu)
                {
                    Text[5].text = _ItemBilgileri[MaterialIndex + 6].Puan + "-"+SatinalmaText;
                    IslemButonlari[1].interactable = false;
                    if (_BellekYonetim.VeriOku_i("Puan") < _ItemBilgileri[MaterialIndex + 6].Puan)
                        IslemButonlari[0].interactable = false;
                    else
                        IslemButonlari[0].interactable = true;
                }
                else
                {
                    Text[5].text = SatinalmaText;
                    IslemButonlari[0].interactable = false;
                    IslemButonlari[1].interactable = true;
                }
            }
            else
            {

                MaterialIndex++;
                Material[] mats = _Renderer.materials;
                mats[0] = Materialler[MaterialIndex];
                _Renderer.materials = mats;
                MaterialText.text = _ItemBilgileri[MaterialIndex + 6].ItemAd;

                if (!_ItemBilgileri[MaterialIndex + 6].SatinAlmaDurumu)
                {
                    Text[5].text = _ItemBilgileri[MaterialIndex + 6].Puan + "-"+SatinalmaText;
                    IslemButonlari[1].interactable = false;
                    if (_BellekYonetim.VeriOku_i("Puan") < _ItemBilgileri[MaterialIndex + 6].Puan)
                        IslemButonlari[0].interactable = false;
                    else
                        IslemButonlari[0].interactable = true;
                }
                else
                {
                    Text[5].text = SatinalmaText;
                    IslemButonlari[0].interactable = false;
                    IslemButonlari[1].interactable = true;
                }
            }

            //-------------------------------------------

            if (MaterialIndex == Materialler.Length - 1)
            {
                MaterialButonlari[1].interactable = false;
            }
            else
            {
                MaterialButonlari[1].interactable = true;
            }
            if (MaterialIndex != -1)
            {
                MaterialButonlari[0].interactable = true;
            }
        }
        else
        {
            if (MaterialIndex != -1)
            {

                MaterialIndex--;
                if (MaterialIndex != -1)
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = Materialler[MaterialIndex];
                    _Renderer.materials = mats;
                    MaterialButonlari[0].interactable = true;
                    MaterialText.text = _ItemBilgileri[MaterialIndex + 6].ItemAd;

                    if (!_ItemBilgileri[MaterialIndex + 6].SatinAlmaDurumu)
                    {
                        Text[5].text = _ItemBilgileri[MaterialIndex + 6].Puan + "-"+SatinalmaText;
                        IslemButonlari[1].interactable = false;
                        if (_BellekYonetim.VeriOku_i("Puan") < _ItemBilgileri[MaterialIndex + 6].Puan)
                            IslemButonlari[0].interactable = false;
                        else
                            IslemButonlari[0].interactable = true;
                    }
                    else
                    {
                        Text[5].text = SatinalmaText;
                        IslemButonlari[0].interactable = false;
                        IslemButonlari[1].interactable = true;
                    }
                }
                else
                {
                    MaterialButonlari[0].interactable = false;
                    Material[] mats = _Renderer.materials;
                    mats[0] = DefoultTema;
                    _Renderer.materials = mats;
                    MaterialText.text = ItemText;

                    Text[5].text = SatinalmaText;
                    IslemButonlari[0].interactable = false;
                }

            }
            else
            {
                MaterialButonlari[0].interactable = false;
                Material[] mats = _Renderer.materials;
                mats[0] = Materialler[MaterialIndex];
                _Renderer.materials = mats;
                MaterialText.text = ItemText;

                Text[5].text = SatinalmaText;
                IslemButonlari[0].interactable = false;
            }

            //-------------------------------------------
            if (MaterialIndex != Materialler.Length - 1)
            {
                MaterialButonlari[1].interactable = true;
            }

        }
    }
    public void IslemPaneliCikar(int index)
    {
        ButonSes[0].Play();
        DurumuKontrolEt(index, false);
        GenelPanaller[0].SetActive(true);
        IslemPanelindex = index;
        IslemPanalleri[index].SetActive(true);
        GenelPanaller[1].SetActive(true);
        IslemCanvas.SetActive(false);
    }
    public void GeriDon()
    {
        ButonSes[0].Play();
        GenelPanaller[0].SetActive(false);
        GenelPanaller[1].SetActive(false);
        IslemCanvas.SetActive(true);
        IslemPanalleri[IslemPanelindex].SetActive(false);
        DurumuKontrolEt(IslemPanelindex, true);
        IslemPanelindex = -1;
        Text[5].text = SatinalmaText;
        _VeriYonetim.Save(_ItemBilgileri);
    }
    void DurumuKontrolEt(int Bolum, bool islem = false)
    {
        if (Bolum == 0)
        {
            if (_BellekYonetim.VeriOku_i("AktifSapka") == -1)
            {
                foreach (var item in Sapkalar)
                {
                    item.SetActive(false);
                }

                IslemButonlari[0].interactable = false;
                IslemButonlari[1].interactable = false;

                if (!islem)
                {
                    SapkaIndex = -1;
                    SapkaText.text = ItemText;
                }

            }
            else
            {
                foreach (var item in Sapkalar)
                {
                    item.SetActive(false);
                }

                SapkaIndex = _BellekYonetim.VeriOku_i("AktifSapka");
                Sapkalar[SapkaIndex].SetActive(true);

                SapkaText.text = _ItemBilgileri[SapkaIndex].ItemAd;
                Text[5].text = SatinalmaText;
                IslemButonlari[0].interactable = false;
                IslemButonlari[1].interactable = true;
            }

        }

        else if (Bolum == 1)
        {
            if (_BellekYonetim.VeriOku_i("AktifSopa") == -1)
            {
                foreach (var item in Sopalar)
                {
                    item.SetActive(false);
                }
                IslemButonlari[0].interactable = false;
                IslemButonlari[1].interactable = false;
                if (!islem)
                {
                    SopaIndex = -1;
                    SopaText.text = ItemText;
                }
            }
            else
            {
                foreach (var item in Sopalar)
                {
                    item.SetActive(false);
                }

                SopaIndex = _BellekYonetim.VeriOku_i("AktifSopa");
                Sopalar[SopaIndex].SetActive(true);
                SopaText.text = _ItemBilgileri[SopaIndex + 3].ItemAd;
                Text[5].text = SatinalmaText;
                IslemButonlari[0].interactable = false;
                IslemButonlari[1].interactable = true;
            }

        }

        else
        {
            if (_BellekYonetim.VeriOku_i("AktifTema") == -1)
            {

                if (!islem)
                {
                    MaterialIndex = -1;
                    MaterialText.text = ItemText;
                    IslemButonlari[0].interactable = false;
                    IslemButonlari[1].interactable = false;
                }

                else
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = DefoultTema;
                    _Renderer.materials = mats;
                }
            }
            else
            {
                MaterialIndex = _BellekYonetim.VeriOku_i("AktifTema");

                Material[] mats = _Renderer.materials;
                mats[0] = Materialler[MaterialIndex];
                _Renderer.materials = mats;

                MaterialText.text = _ItemBilgileri[MaterialIndex + 6].ItemAd;
                Text[5].text = SatinalmaText;
                IslemButonlari[0].interactable = false;
                IslemButonlari[1].interactable = true;
            }
        }


    }
    public void AnaMenuyeDon()
    {
        ButonSes[0].Play();
        Text[5].text = SatinalmaText;
        _VeriYonetim.Save(_ItemBilgileri);


        SceneManager.LoadScene(0);
    }


    public void Satinal()
    {
        ButonSes[1].Play();
        if (IslemPanelindex != -1)
        {
            switch (IslemPanelindex)
            {
                case 0:
                    SatinAlmaSonuc(SapkaIndex);
                    break;
                case 1:
                    int index = SopaIndex + 3;
                    SatinAlmaSonuc(index);
                    break;
                case 2:
                    int index2 = MaterialIndex + 6;
                    SatinAlmaSonuc(index2);
                    break;
            }
        }
    }
    public void Kaydet()
    {
        ButonSes[2].Play();
        if (IslemPanelindex != -1)
        {
            switch (IslemPanelindex)
            {
                case 0:
                    KaydetmeSonuc("AktifSapka", SapkaIndex);
                    break;
                case 1:
                    KaydetmeSonuc("AktifSopa", SopaIndex);
                    break;
                case 2:
                    KaydetmeSonuc("AktifTema", MaterialIndex);
                    break;
            }
        }

    }
    void SatinAlmaSonuc(int index)
    {
        _ItemBilgileri[index].SatinAlmaDurumu = true;
        _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_i("Puan") - _ItemBilgileri[index].Puan);
        Text[5].text = SatinalmaText;
        IslemButonlari[0].interactable = false;
        IslemButonlari[1].interactable = true;
        PuanText.text = _BellekYonetim.VeriOku_i("Puan").ToString();
    }

    void KaydetmeSonuc(string key, int index)
    {
        _BellekYonetim.VeriKaydet_int(key, index);
        IslemButonlari[1].interactable = false;
        if (!Kaydedildi_Animator.GetBool("ok"))
        {
            Kaydedildi_Animator.SetBool("ok", true);
        }
    }

}