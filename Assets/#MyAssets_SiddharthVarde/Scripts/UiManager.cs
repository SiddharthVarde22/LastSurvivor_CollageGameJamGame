using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    Text handGunBullets, rifelBullets, healthKitText;
    [SerializeField]
    GameObject handGunImageRef, rifelImageRef, pauseMenu, gameOverMenu, gameWin, loadingPanel, credits, howToPlay;
    [SerializeField]
    Image healthBarFiller;

    float percent;

    private void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        if (gameOverMenu != null)
        {
            gameOverMenu.SetActive(false);
        }
        if (gameWin != null)
        {
            gameWin.SetActive(false);
        }
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(false);
        }
        if(credits != null)
        {
            credits.SetActive(false);
        }
        if(howToPlay != null)
        {
            howToPlay.SetActive(false);
        }
        Time.timeScale = 1;
    }
    public void PlayButtonPressed()
    {
        loadingPanel.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void ExitPressed()
    {
        Application.Quit();
    }

    public void ChangeHandGunBulletCount(int bullets)
    {
        handGunBullets.text = bullets + "";
    }

    public void ChangeRifelBulletCount(int bullets)
    {
        rifelBullets.text = bullets + "";
    }

    public void ChangeHelthKitText(int number)
    {
        healthKitText.text = number + "";
    }

    public void ChangeGunLogo(string name)
    {
        if(name == "handgun")
        {
            handGunImageRef.SetActive(true);
            rifelImageRef.SetActive(false);
        }
        else
        {
            handGunImageRef.SetActive(false);
            rifelImageRef.SetActive(true);
        }
    }

    public void HelthBarChange(float currentHelth, float maxHelth)
    {
        percent = currentHelth / maxHelth;
        healthBarFiller.rectTransform.localScale = new Vector3(percent, 1, 1);
        healthBarFiller.color = Color.Lerp(Color.red, Color.green, percent);
    }

    public void ResumeButtonPressed()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void MainMenuPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
    }

    public void GameWin()
    {
        Time.timeScale = 0;
        gameWin.SetActive(true);
    }

    public void CreditsPressed()
    {
        credits.SetActive(true);
    }
    public void howToPlayPressed()
    {
        howToPlay.SetActive(true);
    }
    public void backPressed()
    {
        if(credits.activeSelf)
        {
            credits.SetActive(false);
        }
        if(howToPlay.activeSelf)
        {
            howToPlay.SetActive(false);
        }
    }
}
