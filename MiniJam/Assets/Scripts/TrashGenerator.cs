using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGenerator : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();
    public List<GameObject> trashes = new List<GameObject>();
    public GameObject frog;
    public float time = 10;
    public GameObject trash;
    public bool gameover = false;

    public void StartTrash()
    {
        StartCoroutine(trashSpawn());
    }

    IEnumerator trashSpawn()
    {
        while(!gameover)
        {
            int num = Random.Range(0, 5);
            int x = Random.Range(0, 4);

            trash = Instantiate(trashes[x], points[num].transform.position, Quaternion.identity);

            yield return new WaitForSeconds(time);
        }
    }


}
