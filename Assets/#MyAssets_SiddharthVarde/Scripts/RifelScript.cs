using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RifelScript : MonoBehaviour
{
    [SerializeField]
    const int maxAmmoInMag = 30;
    [SerializeField]
    int currentAmmoInMag = 30;
    [SerializeField]
    float rifelRange = 40f;
    [SerializeField]
    int damageToZombie = 30;
    [SerializeField]
    int numberOfBulletsToShoot = 4;
    [SerializeField]
    GameObject muzzleFlashRifel;
    [SerializeField]
    Image crossHairImage;
    [SerializeField]
    AudioClip aimIn;
    [SerializeField]
    AudioClip shoot;
    [SerializeField]
    AudioClip reload;
    [SerializeField]
    AudioClip outOfAmmoReload;

    Animator rifelAnimator;
    RaycastHit rifelHit;
    Vector3 rayPosOnScreen = new Vector3(0.5f, 0.5f, 0);
    UiManager uiManagerRef;

    bool isAiming = false;
    void Start()
    {
        rifelAnimator = GetComponent<Animator>();
        uiManagerRef = GameObject.FindGameObjectWithTag("UiManager").GetComponent<UiManager>();
        uiManagerRef.ChangeRifelBulletCount(currentAmmoInMag);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(Camera.main.ViewportToWorldPoint(rayPosOnScreen), Camera.main.transform.forward, out rifelHit, rifelRange))
        {
            if(rifelHit.collider.CompareTag("Enemy"))
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

        if(Input.GetMouseButtonDown(1))
        {
            AimInOutRifel();
        }

        if(Input.GetMouseButtonDown(0))
        {
            if (currentAmmoInMag > 0)
            {
                rifelAnimator.SetBool("Shooting", true);
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            rifelAnimator.SetBool("Shooting", false);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            rifelAnimator.SetTrigger("Reload");
            if(currentAmmoInMag <= 0)
            {
                AudioSource.PlayClipAtPoint(outOfAmmoReload, transform.position);
            }
            else
            {
                AudioSource.PlayClipAtPoint(reload, transform.position);
            }
        }
    }

    public void ShootRifel()
    {
        if(currentAmmoInMag > 0)
        {
            currentAmmoInMag--;
            AudioSource.PlayClipAtPoint(shoot, transform.position);
            uiManagerRef.ChangeRifelBulletCount(currentAmmoInMag);
            muzzleFlashRifel.SetActive(true);
            if(Physics.Raycast(Camera.main.ViewportToWorldPoint(rayPosOnScreen),Camera.main.transform.forward,out rifelHit,rifelRange))
            {
                if(rifelHit.collider.CompareTag("Enemy"))
                {
                    rifelHit.collider.GetComponent<Zombie>().GetDamage(damageToZombie);
                }
            }
        }
        if(currentAmmoInMag <= 0)
        {
            rifelAnimator.SetBool("Shooting", false);
            rifelAnimator.SetBool("OutOfAmmo", true);
        }
    }

    public void ReloadRifel()
    {
        currentAmmoInMag = maxAmmoInMag;
        rifelAnimator.SetBool("OutOfAmmo", false);
        uiManagerRef.ChangeRifelBulletCount(currentAmmoInMag);
    }

    void AimInOutRifel()
    {
        if (isAiming)
        {
            isAiming = false;
            rifelAnimator.SetBool("Aiming", false);
        }
        else
        {
            isAiming = true;
            rifelAnimator.SetBool("Aiming", true);
        }
        AudioSource.PlayClipAtPoint(aimIn, transform.position);
    }
}
