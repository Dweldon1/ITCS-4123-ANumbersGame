using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollected : MonoBehaviour
{
   public int numberCollected { get; private set; }

   public UnityEvent<PlayerCollected> onCollected;

   public void Collected() {
        numberCollected++;
        onCollected.Invoke(this);
   }
}
