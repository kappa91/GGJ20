using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lampeggia : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(lol());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator lol(){
        while(true){
        yield return new WaitForSeconds(.35f);
        this.gameObject.GetComponent<Image>().enabled=false;
        yield return new WaitForSeconds(.35f);
        this.gameObject.GetComponent<Image>().enabled=true;
        }
    } 
}
