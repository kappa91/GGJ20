using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RepairingController repairController;
    public bool canRepair;
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

    public void RepairGameplay(Spot spot)
    {
        Debug.Log("Enter spot:" + spot.spotNumber);

        repairController.SetSpot(spot);
        canRepair = true;
    }

    public void RepairExit()
    {
        Debug.Log("Exit spot");

        repairController.StopRepair();
        canRepair = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Repair"))
        {
            if (canRepair)
            {
                Debug.Log("Repair Pressed");
                repairController.StartRepair();
            }
        }
    }
}
