using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
