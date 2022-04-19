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
        int x = Random.Range(0, 4);
        GameObject trash = Instantiate(trashes[x], points[1].transform.position, Quaternion.identity);
        
        StartCoroutine(trashSpawn());
    }

    IEnumerator trashSpawn()
    {
        while (!frog.GetComponent<FrogMovement>().gameOver)
        {
            yield return new WaitForSeconds(time);

            int num = Random.Range(0, 5);
            int x = Random.Range(0, 4);

            GameObject trash = Instantiate(trashes[x], points[num].transform.position, Quaternion.identity);
        }
    }
}
