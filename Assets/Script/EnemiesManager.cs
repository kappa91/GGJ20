using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public Transform[] positions;
    public float spawnTime;
    public GameObject enemyPrefab;
    public bool end = false;
    // Start is called before the first frame update
    void Start()
    {
StartCoroutine(WaitAndSpawn());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator WaitAndSpawn()
    {
        while (!end)
        {
            yield return new WaitForSeconds(spawnTime);
            Transform SelectedPosition = positions[Random.Range(0, positions.Length)];
            GameObject lol =Instantiate(enemyPrefab);
            lol.transform.position =SelectedPosition.position;
            lol.GetComponent<Enemy>().em = this;
        }
    }
}
