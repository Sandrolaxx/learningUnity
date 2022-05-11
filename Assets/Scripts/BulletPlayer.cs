using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : Bullet {

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
        if (collision.CompareTag("Enemy")) {
            Health health = collision.GetComponent<Health>();
            
            health.AddDamage(damage);
            
            Explode();

            if (health.health == 0) {
                collision.GetComponentInParent<EnemyPatrol>().Die();
            }
        }
        
        if (collision.CompareTag("Ground")) {
            Explode();
        }
    }

}
