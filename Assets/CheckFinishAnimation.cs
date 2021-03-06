﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFinishAnimation : MonoBehaviour
{
    public delegate void OnAnimFinished();
    public OnAnimFinished onAnimFinishedDelegate;

    Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 > 0.9f)
        {
            gameObject.SetActive(false);
            onAnimFinishedDelegate?.Invoke();
        }
    }
}
