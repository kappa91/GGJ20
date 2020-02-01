using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public Animator startanim;
    public EnemiesManager em;
    public UnityStandardAssets._2D.PlatformerCharacter2D pe;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        em.enabled = false;
        pe.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startanim!= null && startanim.GetCurrentAnimatorStateInfo(0).IsName("done"))
        {
            //start game
            em.enabled = true;
            pe.enabled = true;
            Destroy(startanim.gameObject);
            startanim = null;

        }

        if(Input.GetButtonDown("restart")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        time -= Time.deltaTime;
    }
}
