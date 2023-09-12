using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yilmaz;

public class LevelManager : MonoBehaviour
{
    public AudioSource ButonSes;
    public Button[] Butonlar;
    //  public int Level;
    public Sprite Kilit;
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetim _VeriYonetim = new VeriYonetim();
    List<DilVerileri> _DilOkunanVerileri = new List<DilVerileri>();
    public List<DilVerileri> _DilTercihi = new List<DilVerileri>();
    public Text[] Text;
    public Slider LoadSlider;
    public GameObject LoadingScene;
    private void Start()
    {

        _VeriYonetim.Dil_Load();
        _DilOkunanVerileri = _VeriYonetim.DilVerileriListeyeAktar();
        _DilTercihi.Add(_DilOkunanVerileri[2]);
        DiltercihiYonetimi();




        ButonSes.volume = _BellekYonetim.VeriOku_f("MenuFx");
    int mevcudLevel = _BellekYonetim.VeriOku_i("SonLevel") - 4;
        int Index = 1;
        for (int i = 0; i < Butonlar.Length; i++)
        {
            if (Index <= mevcudLevel)
            {
                Butonlar[i].GetComponentInChildren<Text>().text = (Index).ToString();
                int SahneIndex = Index + 4;
                Butonlar[i].onClick.AddListener(delegate { SahneYukle(SahneIndex); });
            }
            else
            {
                Butonlar[i].GetComponent<Image>().sprite = Kilit;
                Butonlar[i].enabled = false;
            }
            Index++;
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
    public void SahneYukle(int Index)
    {
        ButonSes.Play();
        StartCoroutine(LoadAsync(Index));
    }
    public void GeriDon()
    {
        ButonSes.Play();
        SceneManager.LoadScene(0);
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
}
