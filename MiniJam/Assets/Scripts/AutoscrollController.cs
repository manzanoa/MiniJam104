using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoscrollController : MonoBehaviour
{
    // TODO make this const for safety once we decide on a value
    public float CAMERA_SPEED = 1f; // Camera speed in UU per second
    public BoxCollider2D left, right, top, deathPlaneColl2D;
    public GameObject frog;
    public GameObject BGMController;
    public GameObject AmbientController;
    public int moveCamera = 0;
    public bool cameraActivated = false;
    public bool fadeOutComplete = false;
    public bool isClicked = false;
    public bool drawGizmosPassed = false;

    public TrashGenerator trashGenerator;

    // Used to draw collision boxes in editor
    Color collColor = Color.blue;
    Color deathPlaneColor = Color.red;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            setIsClicked(true);
        }

        if (moveCamera == 0 && isClicked)
        {
            ActivateCamera();
            trashGenerator.StartTrash();
            setIsClicked(false);
        }

        // Move the camera up slowly
        transform.position = new Vector3(transform.position.x, transform.position.y + (CAMERA_SPEED * Time.deltaTime * moveCamera), transform.position.z);

        if (frog.GetComponent<FrogMovement>().gameOver)
        {
            BGMController.SetActive(false);
            moveCamera = 0;
        }
    }

    public void setIsClicked(bool x)
    {
        isClicked = x;
    }


    public void ActivateCamera()
    {
        moveCamera = 1;
        //BGMController.GetComponent<BGMController>().StartLoop();
        foreach (AudioSource source in AmbientController.GetComponents<AudioSource>())
        {
            StartCoroutine(FadeOut(source, 0.75f));
        }
        cameraActivated = true;
    }

    // Fade out the ambient noise.
    // Credit to Boris1998 at https://forum.unity.com/threads/fade-out-audio-source.335031/
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        audioSource.tag = "not in loop";
        

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            audioSource.tag = "in loop";

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    // Draw the OOB box in the editor
    void OnDrawGizmos()
    {
        Gizmos.color = collColor;
        Gizmos.DrawWireCube(left.transform.position, left.size);
        Gizmos.DrawWireCube(right.transform.position, right.size);
        Gizmos.DrawWireCube(top.transform.position, top.size);
        Gizmos.color = deathPlaneColor;
        Gizmos.DrawWireCube(deathPlaneColl2D.transform.position, deathPlaneColl2D.size);
        drawGizmosPassed = true;
    }

}
