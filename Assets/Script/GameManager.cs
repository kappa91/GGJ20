using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RepairingController repairController;
    public bool canRepair;

    public Animator _anim;

    public int score;

    public int currentSpotNumber;
    #region Singleton

    public static GameManager _thisInstance;

    public static GameManager Get()
    {
        if (_thisInstance == null)
        {
            GameObject newGameObject = new GameObject("GameManager");
            _thisInstance = newGameObject.AddComponent<GameManager>();
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
        CorrectButtonManager.Get().onCorrectButtonDelegate += ButtonCorrect;
    }

    public void RepairGameplay(Spot spot)
    {
        Debug.Log("Enter spot:" + spot.spotNumber);

        repairController.SetSpot(spot);
        currentSpotNumber = spot.spotNumber;
        canRepair = true;
    }

    public void RepairExit()
    {
        Debug.Log("Exit Repair");
        _anim.SetBool("isRepairing", false);
        _anim.SetBool("isRepairingTop", false);
        _anim.SetBool("correctButton", false);

        repairController.StopRepair();
        canRepair = false;
    }

    public bool isAttack;

    private void Update()
    {
        if (Input.GetButtonDown("Repair"))
        {
            if (canRepair)
            {
                canRepair = false;
                if (currentSpotNumber == 0 || currentSpotNumber == 3)
                {
                    _anim.SetBool("isRepairing", true);
                }
                else
                {
                    _anim.SetBool("isRepairingTop", true);
                }
                Debug.Log("Repair Pressed");
                repairController.StartRepair();

            }
        }

        if (Input.GetAxisRaw("Attack") == 1)
        {
            if (!isAttack)
            {
                _anim.SetTrigger("attack");
                RepairExit();
                isAttack = true;
            }
        }
        else if (Input.GetAxisRaw("Attack") == -1)
        {
            isAttack = false;
        }


    }

    public void ButtonCorrect()
    {
        _anim.SetTrigger("correctButtonTrigger");

        score += 50;
    }
}
