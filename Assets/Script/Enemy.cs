﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public float moveY;
    public float time;
    private bool move = true;
    bool isDead;

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            float newY = this.transform.position.y - moveY;
            this.transform.DOMoveY(newY, time);
        }
        else
        {
            this.transform.DOMoveY(this.transform.position.y, 0.0f);
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (move && other.gameObject.tag.Equals("Player"))
        {
            //    //DAMAGE TO PLAYER
            //    GameManager.Get()._anim.SetTrigger("damageTrigger");
            //    GameManager.Get().RepairExit();
            //    move = false;
            //    //animazione e destroy
            //    Destroy(gameObject);
            if (!isDead)
            {
                if (GameManager.Get().isAttack)
                {
                    isDead = true;
                    StartCoroutine(WaitToDestroy());
                }
            }
        }
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (move && other.gameObject.tag.Equals("Player"))
        {
            //    //DAMAGE TO PLAYER
            //    GameManager.Get()._anim.SetTrigger("damageTrigger");
            //    GameManager.Get().RepairExit();
            //    move = false;
            //    //animazione e destroy
            //    Destroy(gameObject);
            if (!isDead)
            {
                if (GameManager.Get().isAttack)
                {
                    isDead = true;
                    StartCoroutine(WaitToDestroy());
                }
            }
        }
        //else
        if (move && other.gameObject.tag.Equals("Car"))
        {
            //DAMAGE TO CAR
            move = false;

            if (other.gameObject.GetComponent<ColliderController>().spot.status != Spot.Status.level1)
            {
                other.gameObject.GetComponent<ColliderController>().spot.status--;
                RepairingController.Get().SelectLevel(other.gameObject.GetComponent<ColliderController>().spot);
            }


            //animazione e destroy
            Destroy(gameObject);
        }
    }
}
