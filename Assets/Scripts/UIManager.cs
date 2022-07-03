using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {
    
    static UIManager current;

    public TextMeshProUGUI UIlife;

    void Awake() {
        if (null != null && current != this) {
            Destroy(gameObject);
            return;
        }

        current = this;
    }

    public static void UpdateLifeUI(int life) {
        if (current == null) {
            return;
        }

        current.UIlife.text=life.ToString();
    }
}
