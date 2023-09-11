using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yilmaz;
using UnityEngine.SceneManagement;

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

    Scene _Scene;

    public AudioSource OyunSesi;
    private void Awake()
    {
        OyunSesi.volume=_BellekYonetim.VeriOku_f("OyunSes");
        Destroy(GameObject.FindWithTag("MenuMusic"));
        ItemKontrol();
    }
    void Start()
    {

        DusmanlariOlustur();
        AnlikKarakterSayisi = 1;
        _Scene = SceneManager.GetActiveScene();

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

                    Debug.Log("Kaybettin");
                }
                else
                {
                    _BellekYonetim.VeriKaydet_float("Puan", _BellekYonetim.VeriOku_f("Puan") + (100f + AnlikKarakterSayisi * 25f));

                    if (_Scene.buildIndex == _BellekYonetim.VeriOku_i("SonLevel"))
                        _BellekYonetim.VeriKaydet_int("SonLevel", _BellekYonetim.VeriOku_i("SonLevel") + 1);
                    Debug.Log("Kazandin");
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
}
