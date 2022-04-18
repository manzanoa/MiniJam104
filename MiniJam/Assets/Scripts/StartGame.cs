using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject credits;
    public GameObject frog;

    private void Awake()
    {
        credits.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenCredits()
    {
        credits.SetActive(true);
        frog.SetActive(false);
    }
    public void CloseCredits()
    {
        credits.SetActive(false);
        frog.SetActive(true);
    }
}
