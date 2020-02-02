using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public float moveY;
    public float time;
    private bool move = true;
    bool isDead;
    public Animator anim;
    public EnemiesManager em;
    public AudioClip enemyDestroyedSound, carDestroyedSound;



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

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("done"))
        {
            Destroy(gameObject);
        }
        if (em != null && em.end)
        {
            anim.SetTrigger("die");
            StartCoroutine(WaitToDestroy());
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

                    anim.SetTrigger("die");
                    StartCoroutine(WaitToDestroy());
                    StartCoroutine(AudioManager.Get().PlayIndependentSoundClipRoutine(enemyDestroyedSound));
                }
            }
        }
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(.35f);
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
                    anim.SetTrigger("die");
                    StartCoroutine(WaitToDestroy());
                    StartCoroutine(AudioManager.Get().PlayIndependentSoundClipRoutine(enemyDestroyedSound));

                }
            }
        }
        //else
        if (move && other.gameObject.tag.Equals("Car"))
        {
            //DAMAGE TO CAR
            move = false;
            other.gameObject.GetComponent<Animator>().SetTrigger("explode");

            if (other.gameObject.GetComponent<ColliderController>().spot.status != Spot.Status.level1)
            {
                other.gameObject.GetComponent<ColliderController>().spot.status--;

                RepairingController.Get().SelectLevel(other.gameObject.GetComponent<ColliderController>().spot);
            }
            StartCoroutine(AudioManager.Get().PlayIndependentSoundClipRoutine(carDestroyedSound));


            //animazione e destroy
            Destroy(gameObject);
        }
    }
}
