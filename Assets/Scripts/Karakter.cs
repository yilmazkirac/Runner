using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Karakter : MonoBehaviour
{
    public GameManager _gameManager;
    public Kamera _Kamera;
    public GameObject GidecegiYer;
    public bool SonaGeldikmi;
    public Slider _Slider;
    public GameObject GecisNoktasi;

    private void Start()
    {
        float fark = Vector3.Distance(transform.position, GecisNoktasi.transform.position);
        _Slider.maxValue = fark;
    }
    private void FixedUpdate()
    {
        if (!SonaGeldikmi)
            transform.Translate(Vector3.forward * .5f * Time.deltaTime);
    }

    private void Update()
    {


        if (Time.timeScale != 0)
        {
            if (SonaGeldikmi)
            {
                transform.position = Vector3.Lerp(transform.position, GidecegiYer.transform.position, 1f * Time.deltaTime);
                if (_Slider.value != 0)
                    _Slider.value -= .005f;


            }
            else
            {
                float fark = Vector3.Distance(transform.position, GecisNoktasi.transform.position);
                _Slider.value = fark;

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (Input.GetAxis("Mouse X") < 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z), .3f);
                    }
                    if (Input.GetAxis("Mouse X") > 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z), .3f);
                    }
                }


            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carpma") || other.CompareTag("Toplama") || other.CompareTag("Cikarma") || other.CompareTag("Bolme"))
        {
            int sayi = int.Parse(other.name);
            _gameManager.AdamYonetim(other.tag, sayi, other.transform);
        }

        else if (other.CompareTag("LevelSonu"))
        {
            _Kamera.SonaGeldikmi = true;
            _gameManager.DusmanlariTetikle();
            SonaGeldikmi = true;

        }
        else if (other.CompareTag("BosKarakter"))
        {
            _gameManager.Karakterler.Add(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Direk")|| collision.gameObject.CompareTag("IgneliKutu")|| collision.gameObject.CompareTag("PervaneIgnesi"))
        {
            if (transform.position.x>0)
                transform.position = new Vector3(transform.position.x - .2f, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x + .2f, transform.position.y, transform.position.z);
        }       
    }
}
