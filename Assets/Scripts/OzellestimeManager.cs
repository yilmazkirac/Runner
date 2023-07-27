using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yilmaz;


public class OzellestimeManager : MonoBehaviour
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
    public TextMeshProUGUI SatinalmaText;
    public Text PuanText;
    public List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri>();


    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetim _VeriYonetim = new VeriYonetim();

    int IslemPanelindex;


  

    private void Start()
    {
        _BellekYonetim.VeriKaydet_int("AktifSapka", -1);
        _BellekYonetim.VeriKaydet_int("AktifSopa", -1);
        _BellekYonetim.VeriKaydet_int("AktifTema", -1);
       // _BellekYonetim.VeriKaydet_int("Puan", 1500);
        PuanText.text= _BellekYonetim.VeriOku_i("Puan").ToString();

        if (_BellekYonetim.VeriOku_i("AktifSapka")==-1)
        {
            foreach (var item in Sapkalar)
            {
                item.SetActive(false);
            }
            SapkaIndex = -1;
            SapkaText.text = "Sapka Yok";
        }
        else
        {
            SapkaIndex = _BellekYonetim.VeriOku_i("AktifSapka");
            Sapkalar[SapkaIndex].SetActive(true);
        }

  
        if (_BellekYonetim.VeriOku_i("AktifSopa") == -1)
        {
            foreach (var item in Sopalar)
            {
                item.SetActive(false);
            }
            SopaIndex = -1;
            SopaText.text = "Sapka Yok";
        }
        else
        {
            SopaIndex = _BellekYonetim.VeriOku_i("AktifSopa");
            Sopalar[SopaIndex].SetActive(true);
        }

        if (_BellekYonetim.VeriOku_i("AktifTema") == -1)
        {
    

            MaterialIndex = -1;
            MaterialText.text = "Tema Yok";
        }
        else
        {
            MaterialIndex = _BellekYonetim.VeriOku_i("AktifTema");

            Material[] mats = _Renderer.materials;
            mats[0] = Materialler[MaterialIndex];
            _Renderer.materials = mats;
        }


        //_VeriYonetim.Save(_ItemBilgileri);
        _VeriYonetim.Load();
        _ItemBilgileri = _VeriYonetim.ListeyeAktar();
    }
    public void Sapka_Butonlari(string islem)
    {
        if (islem=="ileri")
        {
            if (SapkaIndex==-1)
            {
                SapkaIndex = 0;
                Sapkalar[SapkaIndex].SetActive(true);
               SapkaText.text= _ItemBilgileri[SapkaIndex].ItemAd;
            }
            else
            {
                Sapkalar[SapkaIndex].SetActive(false);
                SapkaIndex++;
                Sapkalar[SapkaIndex].SetActive(true);
                SapkaText.text = _ItemBilgileri[SapkaIndex].ItemAd;
            }

            //-------------------------------------------

            if (SapkaIndex==Sapkalar.Length-1)
            {
                SapkaButonlari[1].interactable = false;
            }
            else 
            {
                SapkaButonlari[1].interactable = true;
            }
            if (SapkaIndex!=-1)
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
                if (SapkaIndex!=-1)
                {
                    Sapkalar[SapkaIndex].SetActive(true);
                    SapkaButonlari[0].interactable = true;
                    SapkaText.text = _ItemBilgileri[SapkaIndex].ItemAd;
                }
                else
                {
                    SapkaButonlari[0].interactable = false;
                    SapkaText.text = "Sapka Yok";
                }
      
            }
            else
            {
                SapkaButonlari[0].interactable = false;
                SapkaText.text = "Sapka Yok";
            }

            //-------------------------------------------
            if (SapkaIndex!=Sapkalar.Length-1)
            {
                SapkaButonlari[1].interactable = true;
            }
            
        }
    }
    public void Sopa_Butonlari(string islem)
    {
        if (islem == "ileri")
        {
            if (SopaIndex == -1)
            {
                SopaIndex = 0;
                Sopalar[SopaIndex].SetActive(true);
                SopaText.text = _ItemBilgileri[SopaIndex+3].ItemAd;
            }
            else
            {
                Sopalar[SopaIndex].SetActive(false);
                SopaIndex++;
                Sopalar[SopaIndex].SetActive(true);
                SopaText.text = _ItemBilgileri[SopaIndex + 3].ItemAd;
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
                }
                else
                {
                    SopaButonlari[0].interactable = false;
                    SopaText.text = "Sopa Yok";
                }

            }
            else
            {
                SopaButonlari[0].interactable = false;
                SopaText.text = "Sopa Yok";
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
        if (islem == "ileri")
        {
            if (MaterialIndex == -1)
            {
                MaterialIndex = 0;
                Material[] mats = _Renderer.materials;
                mats[0] = Materialler[MaterialIndex];
                _Renderer.materials = mats;
                MaterialText.text = _ItemBilgileri[MaterialIndex + 6].ItemAd;
            }
            else
            {
             
                MaterialIndex++;
                Material[] mats = _Renderer.materials;
                mats[0] = Materialler[MaterialIndex];
                _Renderer.materials = mats;
                MaterialText.text = _ItemBilgileri[MaterialIndex + 6].ItemAd;
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
                }
                else
                {
                    MaterialButonlari[0].interactable = false;
                    Material[] mats = _Renderer.materials;
                    mats[0] = DefoultTema;
                    _Renderer.materials = mats;
                    MaterialText.text = "Tema Yok";
                }

            }
            else
            {
                MaterialButonlari[0].interactable = false;
                Material[] mats = _Renderer.materials;
                mats[0] = Materialler[MaterialIndex];
                _Renderer.materials = mats;
                MaterialText.text = "Tema Yok";
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
        IslemPanelindex=index;
        GenelPanaller[0].SetActive(true);
        IslemPanalleri[index].SetActive(true);
        GenelPanaller[1].SetActive(true);
        IslemCanvas.SetActive(false);
    }
    public void GeriDon()
    {
        IslemPanalleri[IslemPanelindex].SetActive(false);
        GenelPanaller[0].SetActive(false);
        GenelPanaller[1].SetActive(false);
        IslemCanvas.SetActive(true);
        IslemPanelindex = -1;



    }
    public void AnaMenuyeDon()
    {
        SceneManager.LoadScene(0);

    }


    public void Satinal()
    {

        if (IslemPanelindex!=-1)
        {
            switch (IslemPanelindex)
            {
                case 0:
                    Debug.Log("Bölüm no : " + IslemPanelindex + " Item Index : " + SapkaIndex + " Item Name : " + _ItemBilgileri[SapkaIndex].ItemAd);
                    break;
                case 1:
                    Debug.Log("Bölüm no : " + IslemPanelindex + " Item Index : " + SopaIndex + " Item Name : " + _ItemBilgileri[SopaIndex+3].ItemAd);
                    break;
                case 2:
                    Debug.Log("Bölüm no : " + IslemPanelindex + " Item Index : " + MaterialIndex + " Item Name : " + _ItemBilgileri[MaterialIndex+6].ItemAd);
                    break;
            }
        }
    }
    public void Kaydet()
    {
        Debug.Log(IslemPanelindex);
    }
}
