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
    public GameObject CarCompleted;
    public RectTransform secondsAdd;
    bool isRestarted;
    // Start is called before the first frame update
    #region Singleton

    public static TimeManager _thisInstance;

    public static TimeManager Get()
    {
        if (_thisInstance == null)
        {
            GameObject newGameObject = new GameObject("TimeManager");
            _thisInstance = newGameObject.AddComponent<TimeManager>();
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

    public void AddSeconds()
    {
        time += 2;
        secondsAdd.DOScaleY(1, .25f).OnComplete(() =>
        {

            secondsAdd.DOScaleY(0, .25f).SetDelay(.5f);
        });
    }

    void Start()
    {
        em.enabled = false;
        pe.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Restart") && !isRestarted)
        {
            isRestarted = true;
            SceneManager.LoadScene("Intro");
        }

        if (!win)
        {
            if (!start && time > 0.0f && startanim.GetCurrentAnimatorStateInfo(0).IsName("done"))
            {
                //start game
                em.enabled = true;
                pe.enabled = true;
                //Destroy(startanim.gameObject);
                //startanim = null;
                start = true;

            }


            if (start)
            {
                time -= Time.deltaTime;
                text.text = ((int)time).ToString();
                //Debug.Log((int)time);


                if (time <= 20)
                {
                    text.color = Color.red;
                }
                else if (time <= 60)
                {
                    text.color = new Color(0.8867924f, 0.5007113f, 0.1547704f);
                }

                if (time <= 0.0f)
                {
                    start = false;
                    em.end = true;
                    pe.enabled = false;
                    //startanim.SetTrigger("win");

                    //gameOver.gameObject.transform.DOScale(Vector3.one, 3.0f);
                    gameOver.gameObject.transform.DOScale(new Vector3(1, 0.01f, 0), 1f).OnComplete(delegate
                    {
                        gameOver.gameObject.transform.DOScale(new Vector3(1, 1, 0), 1f);
                    });

                }


            }
        }
        else
        {
            CarCompleted.SetActive(true);
            startanim.SetTrigger("win");
            em.end = true;
            pe.enabled = false;
        }
    }
}
