using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class RepairingController : MonoBehaviour
{

    public float speed;

    public float speedLevel1, speedLevel2, speedLevel3;

    public float spawnRate;
    float timer;

    Spot currentSpot;

    public GameObject buttonPrefab;
    public Transform spawnPoint;

    public void SetSpot(Spot spot)
    {
        currentSpot = spot;
    }

    public void StartRepair()
    {
        currentSpot.isRepairing = true;

        switch (currentSpot.status)
        {
            case Spot.Status.level1:
                speed = speedLevel1;
                break;
            case Spot.Status.level2:
                speed = speedLevel2;
                break;
            case Spot.Status.level3:
                speed = speedLevel3;
                break;
        }

        StartCoroutine(GenerateRepairSequence());
    }

    public void StopRepair()
    {

    }

    IEnumerator GenerateRepairSequence()
    {
        SpawnButton();
        while (currentSpot.isRepairing)
        {
            timer += Time.deltaTime;
            if (timer > spawnRate)
            {
                timer = 0;
                SpawnButton();
            }
            yield return null;
        }
    }

    void SpawnButton()
    {
        GameObject buttonRepair = GameObject.Instantiate(buttonPrefab, spawnPoint);

        buttonRepair.GetComponent<RectTransform>().DOLocalMoveY(-116, speed).OnComplete(
            () =>
            {
                Destroy(buttonRepair);
            }
            );
    }
}
