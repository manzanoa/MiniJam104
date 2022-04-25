using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject credits;
    public GameObject frog;
    public bool awakePassed = false;
    public bool quitPassed = false;
    public bool loadPassed = false;
    public bool openPassed = false;
    public bool closePassed = false;

    private void Awake()
    {
        credits.SetActive(false);
        awakePassed = true;
        quitPassed = false;
        loadPassed = false;
        openPassed = false;
        closePassed = false;
    }
    public void QuitGame()
    {
        Application.Quit();
        awakePassed = false;
        quitPassed = true;
        loadPassed = false;
        openPassed = false;
        closePassed = false;

    }
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
        awakePassed = false;
        quitPassed = false;
        loadPassed = true;
        openPassed = false;
        closePassed = false;
    }

    public void OpenCredits()
    {
        credits.SetActive(true);
        frog.SetActive(false);
        awakePassed = false;
        quitPassed = false;
        loadPassed = false;
        openPassed = true;
        closePassed = false;
    }
    public void CloseCredits()
    {
        credits.SetActive(false);
        frog.SetActive(true);
        awakePassed = false;
        quitPassed = false;
        loadPassed = false;
        openPassed = false;
        closePassed = true;
    }
}
