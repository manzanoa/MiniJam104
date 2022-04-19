using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGenerator : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();
    public List<GameObject> trashes = new List<GameObject>();
    public GameObject frog;
    public float time = 10;

    public void StartTrash()
    {
<<<<<<< Updated upstream
=======
        int x = Random.Range(0, 4);

        GameObject trash = Instantiate(trashes[x], points[1].transform.position, Quaternion.identity);

>>>>>>> Stashed changes
        StartCoroutine(trashSpawn());
    }

    IEnumerator trashSpawn()
    {
<<<<<<< Updated upstream
        while(!frog.GetComponent<FrogMovement>().gameOver)
        {
=======
        while (!frog.GetComponent<FrogMovement>().gameOver)
        {
            yield return new WaitForSeconds(time);

>>>>>>> Stashed changes
            int num = Random.Range(0, 5);
            int x = Random.Range(0, 4);

            GameObject trash = Instantiate(trashes[x], points[num].transform.position, Quaternion.identity);
<<<<<<< Updated upstream

            yield return new WaitForSeconds(time);
        }
    }


=======
        }
    }
>>>>>>> Stashed changes
}
