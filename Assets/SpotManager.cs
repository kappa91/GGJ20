using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotManager : MonoBehaviour
{
    public Spot[] spots;
    public TimeManager timeManager;


    #region Singleton

    public static SpotManager _thisInstance;

    public static SpotManager Get()
    {
        if (_thisInstance == null)
        {
            GameObject newGameObject = new GameObject("SpotManager");
            _thisInstance = newGameObject.AddComponent<SpotManager>();
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
        RepairingController.Get().onSpotDoneDelegate += CheckGameWin;
    }


    void CheckGameWin()
    {
        foreach (Spot s in spots)
        {
            if (s.status != Spot.Status.done)
            {
                return;
            }
        }

        timeManager.win = true;
    }
}
