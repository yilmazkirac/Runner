using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AdamLekesi : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(Pasiflestir());
    }
    IEnumerator Pasiflestir()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
