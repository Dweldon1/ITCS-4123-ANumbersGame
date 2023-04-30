using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
