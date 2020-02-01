using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour
{
    public bool isRepairing;

    public enum Status { level1, level2, level3, done }

    public Status status = Status.level1;

    public int spotNumber;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (status != Status.done)
            {
                if (!isRepairing)
                {
                    Debug.Log("Repairing");
                    GameManager.Get().RepairGameplay(this);
                }
            }
            else
            {
                GameManager.Get().currentSpotNumber = spotNumber;
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (status != Status.done)
            {
                if (!isRepairing)
                {
                    Debug.Log("Repairing");
                    GameManager.Get().RepairGameplay(this);
                }
            }
            else
            {
                GameManager.Get().currentSpotNumber = spotNumber;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && status != Status.done)
        {

            isRepairing = false;
            GameManager.Get().RepairExit();
        }
    }
}
