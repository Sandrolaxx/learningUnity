using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    
    public int totalHealth = 3;
    public int health;
    private SpriteRenderer _renderer;

    void Awake() {
        _renderer = GetComponentInParent<SpriteRenderer>();
        health = totalHealth;
    }

    void Update() {
        
    }

    public void AddDamage(int value) {
        health = health - value;

        //Retorno Visual
        StartCoroutine("VisualFeedback");
        //Game Over
        if (health <= 0) {
            health = 0;
        }
    }

    private IEnumerator VisualFeedback() {
        _renderer.color = Color.red;

        yield return new WaitForSeconds(0.2f);//Após 0.2s ele muda para cor branca

        _renderer.color = Color.white;
    }

}
