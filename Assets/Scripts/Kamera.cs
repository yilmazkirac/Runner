using UnityEngine;

public class Kamera : MonoBehaviour
{
    public Transform Target;
    Vector3 Target_Offset;
    public bool SonaGeldikmi;
    public GameObject GidecegiYer;
    private void Start()
    {
        Target_Offset = transform.position - Target.position;
    }

    private void LateUpdate()
    {
        if (!SonaGeldikmi)
            transform.position = Vector3.Lerp(transform.position, Target.position + Target_Offset, .5f * Time.deltaTime);
        else
            transform.position = Vector3.Lerp(transform.position, GidecegiYer.transform.position, .5f * Time.deltaTime);
    }
}