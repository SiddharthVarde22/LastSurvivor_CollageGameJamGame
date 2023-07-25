using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 10f;
    [SerializeField]
    float rotSpeed = 5f;
    [SerializeField]
    GameObject handGun;
    [SerializeField]
    GameObject rifel;
    [SerializeField]
    const int maxHelth = 100;
    [SerializeField]
    AudioClip playerGetsHurt;
    [SerializeField]
    AudioClip introClip;

    float horInput;
    float verInput;
    float mouseXinput;
    int currentHelth = maxHelth;
    int numberOfMedKits = 1;

    GameObject currentGun;
    Animator currentGunAnimator;
    public UiManager uiManRefInGame;
    void Start()
    {
        currentGun = handGun;
        rifel.SetActive(false);
        uiManRefInGame = GameObject.FindGameObjectWithTag("UiManager").GetComponent<UiManager>();
        uiManRefInGame.ChangeGunLogo(currentGun.name);
        uiManRefInGame.HelthBarChange(currentHelth, maxHelth);
        uiManRefInGame.ChangeHelthKitText(numberOfMedKits);
        currentGunAnimator = currentGun.GetComponent<Animator>();
        AudioSource.PlayClipAtPoint(introClip, transform.position, 5);
    }

    // Update is called once per frame
    void Update()
    {
        horInput = Input.GetAxis("Horizontal");
        verInput = Input.GetAxis("Vertical");
        mouseXinput = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;

        Move();
        Rotate();
        SwitchWepon();
        Die();
        Heal();
        Pause();
    }

    void Pause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            uiManRefInGame.PauseGame();
        }
    }

    void Move()
    {
        transform.position += ((transform.forward * verInput) + (transform.right * horInput)) * moveSpeed * Time.deltaTime;
    }

    void Rotate()
    {
        transform.rotation *= Quaternion.Euler(0, mouseXinput, 0);
    }

    void SwitchWepon()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(currentGun == handGun)
            {
                currentGunAnimator.SetTrigger("HolsterHandGun");
                currentGun = rifel;
                currentGunAnimator = currentGun.GetComponent<Animator>();
            }
            else
            {
                currentGunAnimator.SetTrigger("HolsterRifel");
                currentGun = handGun;
                currentGunAnimator = currentGun.GetComponent<Animator>();
            }
            uiManRefInGame.ChangeGunLogo(currentGun.name);
        }
    }

    public void GetDamage(int amount)
    {
        currentHelth -= amount;
        if(currentHelth < 0)
        {
            currentHelth = 0;
        }
        uiManRefInGame.HelthBarChange(currentHelth, maxHelth);
        AudioSource.PlayClipAtPoint(playerGetsHurt, transform.position);
    }

    void Die()
    {
        if(currentHelth <= 0)
        {
            Debug.Log("Game over");
            uiManRefInGame.GameOver();
        }
    }

    public void Heal()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (numberOfMedKits > 0)
            {
                currentHelth = maxHelth;
                uiManRefInGame.HelthBarChange(currentHelth, maxHelth);
                numberOfMedKits--;
                uiManRefInGame.ChangeHelthKitText(numberOfMedKits);
            }
        }
    }

    public void IncreaseMedKit()
    {
        numberOfMedKits++;
        uiManRefInGame.ChangeHelthKitText(numberOfMedKits);
    }

    public void WinTheGame()
    {
        uiManRefInGame.GameWin();
    }
}
