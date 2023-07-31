using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatoricin : MonoBehaviour
{
    public Animator _Animator;

   public void Pasifles()
    {
        _Animator.SetBool("ok",false);
    }
}
