using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public float moveY;
    public float time;
    private bool move = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(move){
        float newY = this.transform.position.y - moveY;
        this.transform.DOMoveY(newY, time);
        }
        else{
           this.transform.DOMoveY(this.transform.position.y, 0.0f); 
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(move && other.gameObject.tag.Equals("Player")){
            //DAMAGE TO PLAYER
            move = false;
            //animazione e destroy
        }
        else if(move && other.gameObject.tag.Equals("Car")){
            //DAMAGE TO CAR
            move = false;
            //animazione e destroy
        }
    }
}
