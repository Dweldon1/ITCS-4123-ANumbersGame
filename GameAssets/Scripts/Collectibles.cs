using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerCollected player = other.GetComponent<PlayerCollected>();

        if (player != null)
        {
            player.Collected();
            gameObject.SetActive(false);

        }
    }
}
