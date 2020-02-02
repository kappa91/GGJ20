using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CorrectButtonManager : MonoBehaviour
{
    public bool isPlaying;
    ButtonInfo currentButton;

    public delegate void OnCorrectButton();
    public OnCorrectButton onCorrectButtonDelegate;

    public GameObject correctFeedback, wrongFeedback;

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

    private void Start()
    {
        wrongFeedback.GetComponent<CheckFinishAnimation>().onAnimFinishedDelegate += Exit;
    }

    private void Exit()
    {
        GameManager.Get().RepairExit();
    }

    public void OnButtonEnter(ButtonInfo buttonInfo)
    {
        currentButton = buttonInfo;
    }

    private void Update()
    {

        if (isPlaying)
        {
            if (currentButton != null && Input.GetButtonDown(currentButton.buttonNumber))
            {
                Destroy(currentButton.gameObject);
                currentButton = null;
                onCorrectButtonDelegate?.Invoke();
                correctFeedback.SetActive(true);
            }
            else if (Input.anyKeyDown)
            {
                wrongFeedback.SetActive(true);
            }
        }
    }

    public void OnButtonExit()
    {
        GameManager.Get().RepairExit();
    }
}
