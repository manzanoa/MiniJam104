using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public bool restartPassed = false;
    public bool quitPassed = false;

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        restartPassed = true;
        quitPassed = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
        quitPassed = true;
        restartPassed = false;
    }
}
