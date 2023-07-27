using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yilmaz;

public class GameManager : MonoBehaviour
{

    public static int AnlikKarakterSayisi;
    public List<GameObject> Karakterler;
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
    void Start()
    {

        DusmanlariOlustur();
        AnlikKarakterSayisi = 1;
        /*_BellekYonetim.VeriKaydet_string("Ad", "Yilmaz");
        _BellekYonetim.VeriKaydet_int("Yas", 25);
        _BellekYonetim.VeriKaydet_float("Puan", 75);*/

        //  Debug.Log(_BellekYonetim.VeriOku_s("Ad"));
        // Debug.Log(_BellekYonetim.VeriOku_i("Yas"));
        Debug.Log(_BellekYonetim.VeriOku_f("Puan"));
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
                AnaKarakter.GetComponent<Animator>().SetBool("Saldir", false);

                if (AnlikKarakterSayisi < KacDusmanOlsun || AnlikKarakterSayisi == KacDusmanOlsun)
                {
                    Debug.Log("Kaybettin");
                }
                else
                {
                    _BellekYonetim.VeriKaydet_float("Puan", _BellekYonetim.VeriOku_f("Puan") + (100f + AnlikKarakterSayisi * 25f));
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
}
