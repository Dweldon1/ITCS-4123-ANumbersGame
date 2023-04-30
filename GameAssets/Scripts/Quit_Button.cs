using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_Button : MonoBehaviour
{
    public void QuitGame()
    {
        if (UnityEditor.EditorApplication.isPlaying == true)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        } else {
            Application.Quit();
        }
    }
}
