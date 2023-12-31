using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSes : MonoBehaviour
{
    private static GameObject instance;
    AudioSource Ses;
    void Start()
    {
        Ses = GetComponent<AudioSource>();
        Ses.volume = PlayerPrefs.GetFloat("MenuSes");
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Ses.volume = PlayerPrefs.GetFloat("MenuSes");
    }
}
