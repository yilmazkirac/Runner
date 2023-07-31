using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AyarlarManager : MonoBehaviour
{
    public AudioSource ButonSes;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AnaMenuyeDon()
    {
        ButonSes.Play();
        SceneManager.LoadScene(0);
    }
    public void DilDegis()
    {
        ButonSes.Play();
    }
}
