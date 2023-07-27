using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class BosKarakter : MonoBehaviour
{
    public SkinnedMeshRenderer _Renderer;
    public Material AtanacakMaterial;
    public NavMeshAgent _Navmesh;
    public Animator _Animator;
    public GameObject Target;
    public GameManager _GameManager;
    bool TemasVar;

    private void LateUpdate()
    {
        if (TemasVar)
            _Navmesh.SetDestination(Target.transform.position);
    }
    Vector3 PozisyonVer()
    {

        return new Vector3(transform.position.x, .23f, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("AltKarakter") || other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("BosKarakter"))
            {
                MaterialDegis();
                TemasVar = true;
                GetComponent<AudioSource>().Play();
            }
        }


        else if (other.CompareTag("IgneliKutu"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("Testere"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("PervaneIgnesi"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Balyoz"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer(), true);
            gameObject.SetActive(false);
        }

        else if (other.CompareTag("Dusman"))
        { 
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer(), false, false);
            gameObject.SetActive(false);
        }
    }
    void MaterialDegis()
    {
        Material[] mats = _Renderer.materials;
        mats[0] = AtanacakMaterial;
        _Renderer.materials = mats;
        _Animator.SetBool("Saldir", true);
        gameObject.tag = "AltKarakter";
        GameManager.AnlikKarakterSayisi++;

    }
}
