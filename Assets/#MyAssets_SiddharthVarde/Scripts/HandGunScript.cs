using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandGunScript : MonoBehaviour
{
    [SerializeField]
    const int maxAmmoInMag = 10;
    [SerializeField]
    int currentAmmoInMag = 10;
    [SerializeField]
    int damageToZombie = 20;
    [SerializeField]
    float range = 20;
    [SerializeField]
    GameObject handGunMuzzle;
    [SerializeField]
    Image crossHairImage;
    [SerializeField]
    AudioClip aimIn, shoot, reload, outOfAmmoReload;

    Animator handGunAnimator;
    RaycastHit hitOfHandGun;
    Vector3 rayPosOnScreen = new Vector3(0.5f, 0.5f, 0);
    UiManager uiManagerRef;

    bool aiming = false;
    void Start()
    {
        handGunAnimator = GetComponent<Animator>();
        uiManagerRef = GameObject.FindGameObjectWithTag("UiManager").GetComponent<UiManager>();
        uiManagerRef.ChangeHandGunBulletCount(currentAmmoInMag);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(Camera.main.ViewportToWorldPoint(rayPosOnScreen), Camera.main.transform.forward, out hitOfHandGun, range))
        {
            if(hitOfHandGun.collider.CompareTag("Enemy"))
            {
                crossHairImage.color = Color.green;
            }
            else
            {
                crossHairImage.color = Color.red;
            }
        }
        else
        {
            crossHairImage.color = Color.red;
        }

        Shoot();

        if(Input.GetKeyDown(KeyCode.R))
        {
            handGunAnimator.SetTrigger("Reload");
            if(currentAmmoInMag <= 0)
            {
                AudioSource.PlayClipAtPoint(outOfAmmoReload, transform.position);
            }
            else
            {
                AudioSource.PlayClipAtPoint(reload, transform.position);
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            AimInOutHandGun();
        }
    }

    void AimInOutHandGun()
    {
        if (aiming)
        {
            handGunAnimator.SetBool("Aiming",false);
            aiming = false;
        }
        else
        {
            handGunAnimator.SetBool("Aiming", true);
            aiming = true;
        }
        AudioSource.PlayClipAtPoint(aimIn, transform.position);
    }

    void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(currentAmmoInMag > 0)
            {
                currentAmmoInMag--;
                handGunAnimator.SetTrigger("Shoot");
                AudioSource.PlayClipAtPoint(shoot, transform.position);
                uiManagerRef.ChangeHandGunBulletCount(currentAmmoInMag);
                if(Physics.Raycast(Camera.main.ViewportToWorldPoint(rayPosOnScreen), Camera.main.transform.forward, out hitOfHandGun, range))
                {
                    if (hitOfHandGun.collider.CompareTag("Enemy"))
                    {
                        hitOfHandGun.collider.GetComponent<Zombie>().GetDamage(damageToZombie);
                    }
                }
                
                handGunMuzzle.SetActive(true);
            }
            if(currentAmmoInMag <= 0)
            {
                handGunAnimator.SetBool("OutOfAmmo", true);
            }
        }
    }

    public void Reload()
    {
        handGunAnimator.SetBool("OutOfAmmo", false);
        currentAmmoInMag = maxAmmoInMag;
        uiManagerRef.ChangeHandGunBulletCount(currentAmmoInMag);
    }
}
