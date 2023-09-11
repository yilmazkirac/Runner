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
    private void Start()
    {
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

    public void SahneYukle(int Index)
    {
        ButonSes.Play();
        SceneManager.LoadScene(Index);
    }
    public void GeriDon()
    {
        ButonSes.Play();
        SceneManager.LoadScene(0);
    }
}
