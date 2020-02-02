using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class TimeManager : MonoBehaviour
{
    public Animator startanim;
    public EnemiesManager em;
    public UnityStandardAssets._2D.Platformer2DUserControl pe;
    public float time;
    public Text text;
    public bool start = false;
    public GameObject gameOver;
    public bool win = false;
    // Start is called before the first frame update
    void Start()
    {
        em.enabled = false;
        pe.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!win)
        {
            if (!start && time > 0.0f&& startanim.GetCurrentAnimatorStateInfo(0).IsName("done"))
            {
                //start game
                em.enabled = true;
                pe.enabled = true;
                //Destroy(startanim.gameObject);
                //startanim = null;
                start = true;

            }

            if (Input.GetButtonDown("Restart"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (start)
            {
                time -= Time.deltaTime;
                text.text = ((int)time).ToString();
                Debug.Log((int)time);
                if (time <= 0.0f)
                {
                    start = false;
                    em.end = true;
                    pe.enabled = false;
                    //startanim.SetTrigger("win");
                    
                    //gameOver.gameObject.transform.DOScale(Vector3.one, 3.0f);
                    gameOver.gameObject.transform.DOScale(new Vector3(1,0.01f,0), 1f).OnComplete(delegate {
                        gameOver.gameObject.transform.DOScale(new Vector3(1,1,0), 1f);
                    });

                }


            }
        }
        else{
            startanim.SetTrigger("win");
             em.end = true;
                    pe.enabled = false;
        }
    }
}
