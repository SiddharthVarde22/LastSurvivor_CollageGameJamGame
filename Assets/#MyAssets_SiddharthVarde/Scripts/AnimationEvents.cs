using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField]
    GameObject handGun;
    [SerializeField]
    GameObject rifel;

    public void ActivateHandGun()
    {
        rifel.SetActive(false);
        handGun.SetActive(true);
    }

    public void ActivateRifel()
    {
        handGun.SetActive(false);
        rifel.SetActive(true);
    }

    public void ReloadingHandGunEvent()
    {
        handGun.GetComponent<HandGunScript>().Reload();
    }
}
