using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : Bullet {
    
    protected override void Awake() {
        base.Awake();
    }
    
    protected override void Start() {
        base.Start();
    }
    
    protected override void FixedUpdate() {
        base.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)  {
        if (collision.CompareTag("Player")) {
            Health health = collision.GetComponent<Health>();
            
            if (health == null) {
                return;
            }
            
            if (!GameManager.isGameOver) {
                health.AddDamage(damage);
                UIManager.UpdateLifeUI(health.health);
            }

            Explode();

            if (health.health == 0) {
                GameManager.isGameOver = true;
                GameManager.PlayerDie();
            }
            
        }
        
        if (collision.CompareTag("Ground")) {
            Explode();
        }
    }

}
