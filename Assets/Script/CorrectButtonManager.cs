using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectButtonManager : MonoBehaviour
{
    public bool isPlaying;
    ButtonInfo currentButton;

    public delegate void OnCorrectButton();
    public OnCorrectButton onCorrectButtonDelegate;

    #region Singleton

    public static CorrectButtonManager _thisInstance;

    public static CorrectButtonManager Get()
    {
        if (_thisInstance == null)
        {
            GameObject newGameObject = new GameObject("CorrectButtonManager");
            _thisInstance = newGameObject.AddComponent<CorrectButtonManager>();
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

    public void OnButtonEnter(ButtonInfo buttonInfo)
    {
        currentButton = buttonInfo;
    }

    private void Update()
    {
        //debug
        if (isPlaying && currentButton != null)
        {
            Debug.Log(currentButton.buttonNumber);
        }

        if (isPlaying)
        {
            if (currentButton != null && Input.GetButtonDown(currentButton.buttonNumber))
            {
                Destroy(currentButton.gameObject);
                currentButton = null;
                onCorrectButtonDelegate?.Invoke();
            }
            else if (Input.anyKeyDown)
            {
                //GameManager.Get().RepairExit();
            }
        }
    }

    public void OnButtonExit()
    {
        GameManager.Get().RepairExit();
    }
}
