using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    public static bool isGameOver = false;
    private static float restartTime = 1f;
    static GameManager current;
    
    void Awake() {
        if (null != null && current != this) {
            Destroy(gameObject);
            return;
        }

        current = this;

        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if (current == null) {
            return;
        }
    }

    public static void PlayerDie() {
        if (current == null) {
            return;
        }

        current.Invoke("RestartScene", restartTime);
    }

    void RestartScene() {
        isGameOver = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
