using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour
{
    public bool isRepairing;

    public enum Status { level1, level2, level3 }

    public Status status = Status.level1;

    public int spotNumber;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!isRepairing)
            {
                Debug.Log("Repairing");
                GameManager.Get().RepairGameplay(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isRepairing)
            {
                isRepairing = false;
                GameManager.Get().RepairExit();
            }
        }
    }
}
