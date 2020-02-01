using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public string buttonNumber;
    RectTransform rect;
    Image spriteRenderer;
    //public Text txt; //debug
    bool isEntered;
    public Sprite[] buttonSprites;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        //txt = GetComponentInChildren<Text>();
        spriteRenderer = GetComponent<Image>();
    }

    public void ButtonSetup(string button)
    {
        buttonNumber = button;

        switch (buttonNumber)
        {
            case "button0":
                //txt.text = "A";
                spriteRenderer.sprite = buttonSprites[0];
                break;
            case "button1":
                //txt.text = "B";
                spriteRenderer.sprite = buttonSprites[1];
                break;
            case "button2":
                //txt.text = "X";
                spriteRenderer.sprite = buttonSprites[2];

                break;
            case "button3":
                spriteRenderer.sprite = buttonSprites[3];
                //txt.text = "Y";
                break;
            case "button4":
                spriteRenderer.sprite = buttonSprites[4];
                //txt.text = "LB";
                break;
            case "button5":
                spriteRenderer.sprite = buttonSprites[5];
                //txt.text = "RB";
                break;
        }
    }

    private void Update()
    {
        if (!isEntered)
        {
            if (rect.localPosition.x >= 145 && rect.localPosition.x <= 165)
            {
                CorrectButtonManager.Get().OnButtonEnter(this);
                isEntered = true;
            }
        }
        else if (rect.localPosition.x > 165)
        {
            CorrectButtonManager.Get().OnButtonExit();
        }
    }
}
