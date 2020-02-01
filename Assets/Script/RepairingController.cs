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
    public int counterButton;

    Spot currentSpot;

    public GameObject buttonPrefab;
    public Transform spawnPoint, repairingBox;

    public string[] buttonNumberCode = { "button0", "button1", "button2", "button3", "button4", "button5" };

    public UnityStandardAssets._2D.PlatformerCharacter2D charaController;

    public SpriteRenderer[] car;
    public Sprite[] carLevel1;
    public Sprite[] carLevel2;
    public Sprite[] carLevel3;
    public Sprite[] carLevel4;

    #region Singleton

    public static RepairingController _thisInstance;

    public static RepairingController Get()
    {
        if (_thisInstance == null)
        {
            GameObject newGameObject = new GameObject("RepairingController");
            _thisInstance = newGameObject.AddComponent<RepairingController>();
        }

        return _thisInstance;
    }

    void Awake()
    {
        if (_thisInstance == null)
        {
            _thisInstance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    #endregion

    private void Start()
    {
        CorrectButtonManager.Get().onCorrectButtonDelegate += CorrectButton;
    }

    public void SetSpot(Spot spot)
    {
        currentSpot = spot;
    }

    void CorrectButton()
    {
        counterButton++;
    }

    public void StartRepair()
    {
        repairingBox.DOScale(1, .2f).OnComplete(() => CorrectButtonManager.Get().isPlaying = true);
        currentSpot.isRepairing = true;
        charaController.lockControl = true;
        SelectLevel(currentSpot);
        StartCoroutine(GenerateRepairSequence());
    }

    public void StopRepair()
    {
        repairingBox.DOScale(0, .2f);
        CorrectButtonManager.Get().isPlaying = false;
        charaController.lockControl = false;
        if (currentSpot != null)
            currentSpot.isRepairing = false;
        foreach (ButtonInfo go in FindObjectsOfType<ButtonInfo>())
        {
            Destroy(go.gameObject);
        }
    }

    IEnumerator GenerateRepairSequence()
    {
        timer = 0;
        SpawnButton();
        counterButton = 0;
        while (currentSpot.isRepairing)
        {
            if (counterButton == buttonNumber)
            {
                counterButton = 0;
                currentSpot.status++;
                SelectLevel(currentSpot);
            }
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
        buttonRepair.GetComponent<ButtonInfo>().ButtonSetup(buttonNumberCode[Random.Range(0, buttonNumberCode.Length)]);
        buttonRepair.GetComponent<RectTransform>().DOLocalMoveX(400, speed).OnComplete(
            () =>
            {
                Destroy(buttonRepair);
            }
            );
    }

    public void SelectLevel(Spot spot)
    {
        switch (spot.status)
        {
            case Spot.Status.level1:
                speed = speedLevel1;
                buttonNumber = buttonNumberLevel1;
                ChangeSpriteCar(spot.spotNumber, carLevel1[spot.spotNumber]);
                break;
            case Spot.Status.level2:
                speed = speedLevel2;
                buttonNumber = buttonNumberLevel2;
                ChangeSpriteCar(spot.spotNumber, carLevel2[spot.spotNumber]);
                break;
            case Spot.Status.level3:
                speed = speedLevel3;
                buttonNumber = buttonNumberLevel3;
                ChangeSpriteCar(spot.spotNumber, carLevel3[spot.spotNumber]);
                break;
            case Spot.Status.done:
                GameManager.Get().RepairExit();
                ChangeSpriteCar(spot.spotNumber, carLevel4[spot.spotNumber]);
                break;
        }
    }

    void ChangeSpriteCar(int index, Sprite carPiece)
    {
        car[index].sprite = carPiece;
    }
}
