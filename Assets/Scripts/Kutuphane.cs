using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GoogleMobileAds.Api;
namespace Yilmaz
{
    public class Matematiksel_islemler
    {
        public void Carpma(int GelenSayi, List<GameObject> Karakterler, Transform Pozisyon, List<GameObject> OlusturmaEfektleri)
        {
            int DonguSayisi = (GameManager.AnlikKarakterSayisi * GelenSayi) - GameManager.AnlikKarakterSayisi;
            int sayi = 0;
            foreach (GameObject karakter in Karakterler)
            {
                if (sayi < DonguSayisi)
                {
                    if (!karakter.activeInHierarchy)
                    {
                        foreach (var efekt in OlusturmaEfektleri)
                        {
                            if (!efekt.activeInHierarchy)
                            {
                                efekt.SetActive(true);
                                efekt.transform.position = Pozisyon.position;
                                efekt.GetComponent<ParticleSystem>().Play();
                                efekt.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        karakter.transform.position = Pozisyon.position;
                        karakter.SetActive(true);
                        sayi++;
                    }
                }
                else
                {
                    sayi = 0;
                    break;
                }
            }
            GameManager.AnlikKarakterSayisi *= GelenSayi;
        }

        public void Toplama(int GelenSayi, List<GameObject> Karakterler, Transform Pozisyon, List<GameObject> OlusturmaEfektleri)
        {
            int sayi2 = 0;
            foreach (GameObject karakter in Karakterler)
            {
                if (sayi2 < GelenSayi)
                {
                    if (!karakter.activeInHierarchy)
                    {
                        foreach (var efekt in OlusturmaEfektleri)
                        {
                            if (!efekt.activeInHierarchy)
                            {
                                efekt.SetActive(true);
                                efekt.transform.position = Pozisyon.position;
                                efekt.GetComponent<ParticleSystem>().Play();
                                efekt.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        karakter.transform.position = Pozisyon.position;
                        karakter.SetActive(true);
                        sayi2++;
                    }
                }
                else
                {
                    sayi2 = 0;
                    break;
                }
            }
            GameManager.AnlikKarakterSayisi += GelenSayi;

        }

        public void Cikarma(int GelenSayi, List<GameObject> Karakterler, List<GameObject> YokOlmaEfektleri)
        {
            if (GameManager.AnlikKarakterSayisi < GelenSayi)
            {
                foreach (GameObject karakter in Karakterler)
                {
                    foreach (var efekt in YokOlmaEfektleri)
                    {
                        if (!efekt.activeInHierarchy)
                        {
                            Vector3 yeniPoz = new Vector3(karakter.transform.position.x, .23f, karakter.transform.position.z);
                            efekt.SetActive(true);
                            efekt.transform.position = yeniPoz;
                            efekt.GetComponent<ParticleSystem>().Play();
                            break;
                        }
                    }
                    karakter.transform.position = Vector3.zero;
                    karakter.SetActive(false);
                }
                GameManager.AnlikKarakterSayisi = 1;
            }

            else
            {
                int sayi3 = 0;
                foreach (GameObject karakter in Karakterler)
                {
                    if (sayi3 != GelenSayi)
                    {

                        if (karakter.activeInHierarchy)
                        {
                            foreach (var efekt in YokOlmaEfektleri)
                            {
                                if (!efekt.activeInHierarchy)
                                {
                                    Vector3 yeniPoz = new Vector3(karakter.transform.position.x, .23f, karakter.transform.position.z);
                                    efekt.SetActive(true);
                                    efekt.transform.position = yeniPoz;
                                    efekt.GetComponent<ParticleSystem>().Play();
                                    break;
                                }
                            }
                            karakter.transform.position = Vector3.zero;
                            karakter.SetActive(false);
                            sayi3++;
                        }
                    }
                    else
                    {
                        sayi3 = 0;
                        break;
                    }
                }
                GameManager.AnlikKarakterSayisi -= GelenSayi;
            }
        }

        public void Bolme(int GelenSayi, List<GameObject> Karakterler, List<GameObject> YokOlmaEfektleri)
        {
            if (GameManager.AnlikKarakterSayisi <= GelenSayi)
            {
                foreach (GameObject karakter in Karakterler)
                {
                    foreach (var efekt in YokOlmaEfektleri)
                    {
                        if (!efekt.activeInHierarchy)
                        {
                            Vector3 yeniPoz = new Vector3(karakter.transform.position.x, .23f, karakter.transform.position.z);

                            efekt.SetActive(true);
                            efekt.transform.position = yeniPoz;
                            efekt.GetComponent<ParticleSystem>().Play();
                            break;
                        }
                    }
                    karakter.transform.position = Vector3.zero;
                    karakter.SetActive(false);
                }
                GameManager.AnlikKarakterSayisi = 1;
            }

            else
            {
                int bolen = GameManager.AnlikKarakterSayisi / GelenSayi;

                int sayi4 = 0;
                foreach (GameObject karakter in Karakterler)
                {
                    if (sayi4 < bolen)
                    {

                        if (karakter.activeInHierarchy)
                        {
                            foreach (var efekt in YokOlmaEfektleri)
                            {
                                if (!efekt.activeInHierarchy)
                                {
                                    Vector3 yeniPoz = new Vector3(karakter.transform.position.x, .23f, karakter.transform.position.z);

                                    efekt.SetActive(true);
                                    efekt.transform.position = yeniPoz;
                                    efekt.GetComponent<ParticleSystem>().Play();
                                    break;
                                }
                            }
                            karakter.transform.position = Vector3.zero;
                            karakter.SetActive(false);
                            sayi4++;
                        }
                    }
                    else
                    {
                        sayi4 = 0;
                        break;
                    }
                }

                if (GameManager.AnlikKarakterSayisi % GelenSayi == 0)
                {
                    GameManager.AnlikKarakterSayisi /= GelenSayi;
                }
                else if (GameManager.AnlikKarakterSayisi % GelenSayi == 1)
                {
                    GameManager.AnlikKarakterSayisi /= GelenSayi;
                    GameManager.AnlikKarakterSayisi++;
                }
                else if (GameManager.AnlikKarakterSayisi % GelenSayi == 2)
                {
                    GameManager.AnlikKarakterSayisi /= GelenSayi;
                    GameManager.AnlikKarakterSayisi += 2;
                }

            }

        }
    }
    public class BellekYonetim
    {
        public void VeriKaydet_string(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }
        public void VeriKaydet_int(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }
        public void VeriKaydet_float(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }

        public string VeriOku_s(string key)
        {
            return PlayerPrefs.GetString(key);
        }
        public int VeriOku_i(string key)
        {
            return PlayerPrefs.GetInt(key);
        }
        public float VeriOku_f(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public void KontrolEtTanimla()
        {
            if (!PlayerPrefs.HasKey("SonLevel"))
            {
                PlayerPrefs.SetInt("SonLevel", 5);
                PlayerPrefs.SetInt("Puan", 100);
                PlayerPrefs.SetInt("AktifSapka", -1);
                PlayerPrefs.SetInt("AktifSopa", -1);
                PlayerPrefs.SetInt("AktifTema", -1);

                PlayerPrefs.SetFloat("MenuFx", 1);
                PlayerPrefs.SetFloat("OyunSes", 1);
                PlayerPrefs.SetFloat("MenuSes", 1);

                PlayerPrefs.SetString("Dil", "TR");
                PlayerPrefs.SetInt("GecisReklamiSayisi", 0);
            }
        }

    }

    [Serializable]
    public class ItemBilgileri
    {
        public int GrupIndenx;
        public int ItemIndex;
        public int Puan;
        public bool SatinAlmaDurumu;
        public string ItemAd;
    }

    public class VeriYonetim
    {
        public void Save(List<ItemBilgileri> _ItemBilgileri)
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemVerileri.gd");
            bf.Serialize(file, _ItemBilgileri);
            file.Close();


        }
        public void IlkKurulum(List<ItemBilgileri> _ItemBilgileri, List<DilVerileri> _DilVerileri)
        {
            if (!File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd");
                bf.Serialize(file, _ItemBilgileri);
                file.Close();

            }
            if (!File.Exists(Application.persistentDataPath + "/DilVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/DilVerileri.gd");
                bf.Serialize(file, _DilVerileri);
                file.Close();

            }
        }

        List<ItemBilgileri> _Itemiclist;
        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemVerileri.gd", FileMode.Open);
                _Itemiclist = (List<ItemBilgileri>)bf.Deserialize(file);
                file.Close();

            }
        }

        List<DilVerileri> _DilVerileriListe;
        public void Dil_Load()
        {
            if (File.Exists(Application.persistentDataPath + "/DilVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/DilVerileri.gd", FileMode.Open);
                _DilVerileriListe = (List<DilVerileri>)bf.Deserialize(file);
                file.Close();

            }
        }
        public List<DilVerileri> DilVerileriListeyeAktar()
        {
            return _DilVerileriListe;
        }

        public List<ItemBilgileri> ListeyeAktar()
        {
            return _Itemiclist;

        }
    }
    [Serializable]
    public class DilVerileri
    {
        public List<DilVerileri_TR> _Dilverileri_TR = new List<DilVerileri_TR>();
        public List<DilVerileri_TR> _Dilverileri_EN = new List<DilVerileri_TR>();
    }
    [Serializable]
    public class DilVerileri_TR
    {
        public string Metin;
    }

    public class ReklamYonetimi
    {

#if UNITY_ANDROID
        private string _adUnitId = "ca-app-pub-3940256899942544/1833173712";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
  private string _adUnitId = "unused";
#endif


#if UNITY_ANDROID
        private string _adRwerdId = "ca-app-pub-3940256899942544/5224354917";
#elif UNITY_IPHONE
  private string _adRwerdId = "ca-app-pub-3940256099942544/4411468910";
#else
  private string _adRwerdId = "unused";
#endif

        private InterstitialAd interstitialAd;
        private RewardedAd rewardedAd;
        public delegate void OnReward();
        public static event OnReward OnGaveReward;

        //geçis

        public void LoadInterstitialAd()
        {
          
       
     
            if (interstitialAd != null)
            {
                interstitialAd.Destroy();
                interstitialAd = null;
            }
            var adRequest = new AdRequest();
            InterstitialAd.Load(_adUnitId, adRequest,
                (InterstitialAd ad, LoadAdError error) =>
                {
                    if (error != null || ad == null)
                    {
                        Debug.LogError("interstitial ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("Interstitial ad loaded with response : "
                              + ad.GetResponseInfo());

                    interstitialAd = ad;
                });
            RegisterEventHandlers(interstitialAd);
        }
        public void ShowAd()
        {

            if (PlayerPrefs.GetInt("GecisReklamiSayisi")==2)
            {
                if (interstitialAd != null && interstitialAd.CanShowAd())
                {
                    PlayerPrefs.SetInt("GecisReklamiSayisi", 0);
                    interstitialAd.Show();
                }
                else
                {
                }
            }
            else
            {
                PlayerPrefs.SetInt("GecisReklamiSayisi", PlayerPrefs.GetInt("GecisReklamiSayisi")+1);
            }

        
        }
        private void RegisterEventHandlers(InterstitialAd ad)
        {
            ad.OnAdFullScreenContentClosed += () =>
            {
                LoadInterstitialAd();
            };
        }



        //ödüllü

        public void LoadRewardedAd()
        {
            if (rewardedAd != null)
            {
                rewardedAd.Destroy();
                rewardedAd = null;
            }
            var adRequest = new AdRequest();

            RewardedAd.Load(_adRwerdId, adRequest,
                (RewardedAd ad, LoadAdError error) =>
                {
                    if (error != null || ad == null)
                    {
                        Debug.LogError("Rewarded ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("Rewarded ad loaded with response : "
                              + ad.GetResponseInfo());

                    rewardedAd = ad;
                });
            RegisterEventHandlers(rewardedAd);



        }
  
            public void ShowRewardedAd()
        {
            if (rewardedAd != null && rewardedAd.CanShowAd())
            {
                rewardedAd.Show((Reward reward) =>
                {
                    OnGaveReward?.Invoke();
                });
            }
        }
        private void RegisterEventHandlers(RewardedAd ad)
        {
            ad.OnAdFullScreenContentClosed += () =>
            {
                LoadRewardedAd();
            };
          
        }

   

        }
    }





