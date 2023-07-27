using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


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


            }
        }

    }

    public class Verilerimiz
    {
        public static List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri>();

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
        public void IlkKurulum(List<ItemBilgileri> _ItemBilgileri)
        {
            if (!File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd");
                bf.Serialize(file, _ItemBilgileri);
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

        public List<ItemBilgileri> ListeyeAktar()
        {
            return _Itemiclist;

        }
    }
}

