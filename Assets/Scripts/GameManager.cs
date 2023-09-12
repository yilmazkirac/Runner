using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yilmaz;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("-----Sapkalar")]
    public GameObject[] Sapkalar;
    [Header("-----Sopalar")]
    public GameObject[] Sopalar;
    [Header("-----Materialler")]
    public Material[] Materialler;
    public SkinnedMeshRenderer _Renderer;
    public Material DefoultTema;

    public static int AnlikKarakterSayisi;
    public List<GameObject> Karakterler;
    public List<GameObject> BosKarakterler;
    public List<GameObject> OlusmaEfektleri;
    public List<GameObject> YokOlmaEfektleri;
    public List<GameObject> AdamLekesiEfektleri;


    [Header("Level Verileri")]
    public List<GameObject> Dusmanlar;
    public int KacDusmanOlsun;
    public GameObject AnaKarakter;
    public bool OyunBittimi;
    bool SonaGeldikmi = false;

    Matematiksel_islemler _Matematiksel_Islemler = new Matematiksel_islemler();
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetim _VeriYonetim = new VeriYonetim();
    Scene _Scene;

    public AudioSource[] OyunSesleri;

    public GameObject[] IslemPanalleri;
    public Slider Sesayar;
    bool cikisBool;

    List<DilVerileri> _DilOkunanVerileri = new List<DilVerileri>();
    public List<DilVerileri> _DilTercihi = new List<DilVerileri>();
    public TextMeshProUGUI[] Text;
    bool ayar = false;
    public Slider LoadSlider;
    public GameObject LoadingScene;
    private void Awake()
    {
        OyunSesleri[0].volume = _BellekYonetim.VeriOku_f("OyunSes");
        Sesayar.value = _BellekYonetim.VeriOku_f("OyunSes");
        OyunSesleri[1].volume = _BellekYonetim.VeriOku_f("MenuFx");
        Destroy(GameObject.FindWithTag("MenuMusic"));
        ItemKontrol();
    }
    void Start()
    {

        _VeriYonetim.Dil_Load();
        _DilOkunanVerileri = _VeriYonetim.DilVerileriListeyeAktar();
        _DilTercihi.Add(_DilOkunanVerileri[5]);
        DiltercihiYonetimi();
        DusmanlariOlustur();
        AnlikKarakterSayisi = 1;
        _Scene = SceneManager.GetActiveScene();

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
    public void CikisBtnIslem(string durum)
    {
        if (!ayar)
        {
            OyunSesleri[1].Play();
            Time.timeScale = 0;
            if (durum == "durdur")
            {
                IslemPanalleri[0].SetActive(true);
                cikisBool=true;
            }


            else if (durum == "devamet")
            {
                IslemPanalleri[0].SetActive(false);
                Time.timeScale = 1;
                cikisBool =false;
            }

            else if (durum == "tekrar")
            {
                SceneManager.LoadScene(_Scene.buildIndex);
                Time.timeScale = 1;
            }
            else if (durum == "anasayfa")
            {
                SceneManager.LoadScene(0);
                Time.timeScale = 1;
            }
        }
    }
    public void Sesiayarla()
    {
        _BellekYonetim.VeriKaydet_float("OyunSes", Sesayar.value);
        OyunSesleri[0].volume = Sesayar.value;
    }
    public void Ayarlar()
    {
        if (!cikisBool)
        {
            if (!ayar)
            {
                IslemPanalleri[1].SetActive(true);
                Time.timeScale = 0;
                ayar = true;

            }
            else
            {
                IslemPanalleri[1].SetActive(false);
                Time.timeScale = 1;
                ayar = false;
            }
        }
 

    }
    void SavasDurumu()
    {
        if (SonaGeldikmi)
        {
            if (AnlikKarakterSayisi == 1 || KacDusmanOlsun == 0)
            {
                OyunBittimi = true;
                foreach (GameObject Dusman in Dusmanlar)
                {
                    if (Dusman.activeInHierarchy)
                    {
                        Dusman.GetComponent<Animator>().SetBool("Saldir", false);
                    }
                }
                foreach (GameObject Dusman in Karakterler)
                {
                    if (Dusman.activeInHierarchy)
                    {
                        Dusman.GetComponent<Animator>().SetBool("Saldir", false);
                    }
                }
                foreach (GameObject bos in BosKarakterler)
                {
                    if (bos.activeInHierarchy)
                    {
                        bos.GetComponent<Animator>().SetBool("Saldir", false);
                    }
                }
                AnaKarakter.GetComponent<Animator>().SetBool("Saldir", false);

                if (AnlikKarakterSayisi < KacDusmanOlsun || AnlikKarakterSayisi == KacDusmanOlsun)
                {

                    IslemPanalleri[3].SetActive(true);
                }
                else
                {
                    _BellekYonetim.VeriKaydet_float("Puan", _BellekYonetim.VeriOku_f("Puan") + (100f + AnlikKarakterSayisi * 25f));

                    if (_Scene.buildIndex == _BellekYonetim.VeriOku_i("SonLevel"))
                        _BellekYonetim.VeriKaydet_int("SonLevel", _BellekYonetim.VeriOku_i("SonLevel") + 1);
                    IslemPanalleri[2].SetActive(true);
                }
            }
        }

    }

    public void DusmanlariTetikle()
    {

        foreach (GameObject Dusman in Dusmanlar)
        {
            if (Dusman.activeInHierarchy)
            {
                Dusman.GetComponent<Dusman>().AnimasyonTetikle();
            }
        }
        SonaGeldikmi = true;
        SavasDurumu();
    }
    void DusmanlariOlustur()
    {
        for (int i = 0; i < KacDusmanOlsun; i++)
        {
            Dusmanlar[i].SetActive(true);
        }
    }
    public void AdamYonetim(string IslemTuru, int GelenSayi, Transform Pozisyon)
    {
        switch (IslemTuru)
        {
            case "Carpma":
                _Matematiksel_Islemler.Carpma(GelenSayi, Karakterler, Pozisyon, OlusmaEfektleri);
                break;

            case "Toplama":
                _Matematiksel_Islemler.Toplama(GelenSayi, Karakterler, Pozisyon, OlusmaEfektleri);
                break;

            case "Cikarma":
                _Matematiksel_Islemler.Cikarma(GelenSayi, Karakterler, YokOlmaEfektleri);
                break;

            case "Bolme":
                _Matematiksel_Islemler.Bolme(GelenSayi, Karakterler, YokOlmaEfektleri);
                break;
        }

    }
    public void YokOlmaEfektiOlustur(Vector3 Pozisyon, bool Balyoz = false, bool Durum = false)
    {
        foreach (var efekt in YokOlmaEfektleri)
        {

            if (!efekt.activeInHierarchy)
            {
                efekt.SetActive(true);
                efekt.transform.position = Pozisyon;
                efekt.GetComponent<ParticleSystem>().Play();
                efekt.GetComponent<AudioSource>().Play();
                if (!Durum)
                    AnlikKarakterSayisi--;
                else
                    KacDusmanOlsun--;
                break;
            }
        }
        if (Balyoz)
        {
            foreach (var efekt in AdamLekesiEfektleri)
            {
                Vector3 yeniPoz = new Vector3(Pozisyon.x, 0.005f, Pozisyon.z);
                if (!efekt.activeInHierarchy)
                {
                    efekt.SetActive(true);
                    efekt.transform.position = yeniPoz;
                    break;
                }
            }
        }
        if (!OyunBittimi)
            SavasDurumu();
    }
    public void ItemKontrol()
    {
        if (_BellekYonetim.VeriOku_i("AktifSapka") != -1)
            Sapkalar[_BellekYonetim.VeriOku_i("AktifSapka")].SetActive(true);
        if (_BellekYonetim.VeriOku_i("AktifSopa") != -1)
            Sopalar[_BellekYonetim.VeriOku_i("AktifSopa")].SetActive(true);
        if (_BellekYonetim.VeriOku_i("AktifTema") != -1)
        {
            Material[] mats = _Renderer.materials;
            mats[0] = Materialler[_BellekYonetim.VeriOku_i("AktifTema")];
            _Renderer.materials = mats;
        }
        else
        {
            Material[] mats = _Renderer.materials;
            mats[0] = DefoultTema;
            _Renderer.materials = mats;
        }
    }
    public void SonrakiLevel()
    {
        StartCoroutine(LoadAsync(_Scene.buildIndex + 1));

    }
}
