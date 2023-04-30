using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUI : MonoBehaviour
{

    private TextMeshProUGUI Text;
    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCount(PlayerCollected player) {
       Text.text = player.numberCollected.ToString();
    }

}
