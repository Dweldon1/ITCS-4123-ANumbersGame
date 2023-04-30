using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameEnded = false;

    public PlayerCollected player;
    public EquationUI correctNum;

    public int answer;

    public int collected;


    void start () {
        player = GetComponent<PlayerCollected>();
        correctNum = GetComponent<EquationUI>();
    }

    void Update() {
        answer = correctNum.x + correctNum.y;
        collected = player.numberCollected;
    }

    public void EndGame()
    {
        if (gameEnded == false)
        {
            Invoke("nextScene", 0.5f);
            gameEnded = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void nextScene()
    {
        if (player.numberCollected == answer) {
            SceneManager.LoadScene("GameEnd Win");
        } else {
            SceneManager.LoadScene("GameEnd Lose");
        }
    }

}

