using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class RepairingController : MonoBehaviour
{

    public float speed;

    public float speedLevel1, speedLevel2, speedLevel3;

    public int buttonNumber;
    public int buttonNumberLevel1, buttonNumberLevel2, buttonNumberLevel3;

    public float spawnRate;
    float timer;

    Spot currentSpot;

    public GameObject buttonPrefab;
    public Transform spawnPoint, repairingBox;

    public string[] buttonNumberCode = { "button0", "button1", "button2", "button3", "button4", "button5" };

    public UnityStandardAssets._2D.PlatformerCharacter2D charaController;

    public SpriteRenderer[] car;
    public Sprite[] carLevel1;
    public Sprite[] carLevel2;
    public Sprite[] carLevel3;


    public void SetSpot(Spot spot)
    {
        currentSpot = spot;
    }

    public void StartRepair()
    {
        repairingBox.DOScale(1, .2f);
        currentSpot.isRepairing = true;
        charaController.lockControl = true;
        SelectLevel();
        StartCoroutine(GenerateRepairSequence());
    }

    public void StopRepair()
    {
        repairingBox.DOScale(0, .2f);
        CorrectButtonManager.Get().isPlaying = false;
        charaController.lockControl = false;
        foreach (ButtonInfo go in FindObjectsOfType<ButtonInfo>())
        {
            Destroy(go.gameObject);
        }
    }

    IEnumerator GenerateRepairSequence()
    {
        timer = 0;
        SpawnButton();
        CorrectButtonManager.Get().isPlaying = true;
        int counter = 1;
        while (currentSpot.isRepairing)
        {
            if (counter == buttonNumber)
            {
                counter = 0;
                if (currentSpot.status == Spot.Status.level3)
                {
                    GameManager.Get().RepairExit();
                    currentSpot.status = Spot.Status.done;
                }
                else
                {
                    currentSpot.status++;
                    SelectLevel();
                }
            }
            timer += Time.deltaTime;
            if (timer > spawnRate)
            {
                timer = 0;
                SpawnButton();
                counter++;
            }
            yield return null;
        }


    }

    void SpawnButton()
    {
        GameObject buttonRepair = GameObject.Instantiate(buttonPrefab, spawnPoint);
        buttonRepair.GetComponent<ButtonInfo>().ButtonSetup(buttonNumberCode[Random.Range(0, buttonNumberCode.Length - 1)]);
        buttonRepair.GetComponent<RectTransform>().DOLocalMoveX(173, speed).SetEase(Ease.InSine).OnComplete(
            () =>
            {
                Destroy(buttonRepair);
            }
            );
    }

    void SelectLevel()
    {
        switch (currentSpot.status)
        {
            case Spot.Status.level1:
                speed = speedLevel1;
                buttonNumber = buttonNumberLevel1;
                ChangeSpriteCar(currentSpot.spotNumber, carLevel1[currentSpot.spotNumber]);
                break;
            case Spot.Status.level2:
                speed = speedLevel2;
                buttonNumber = buttonNumberLevel2;
                ChangeSpriteCar(currentSpot.spotNumber, carLevel2[currentSpot.spotNumber]);
                break;
            case Spot.Status.level3:
                speed = speedLevel3;
                buttonNumber = buttonNumberLevel3;
                ChangeSpriteCar(currentSpot.spotNumber, carLevel3[currentSpot.spotNumber]);
                break;
        }
    }

    void ChangeSpriteCar(int index, Sprite carPiece)
    {
        car[index].sprite = carPiece;
    }
}
