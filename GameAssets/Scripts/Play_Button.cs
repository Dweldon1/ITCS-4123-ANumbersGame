using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play_Button : MonoBehaviour
{
  public void StartGame() {
    SceneManager.LoadScene("Game");
    Cursor.lockState = CursorLockMode.Locked;
  }
}
