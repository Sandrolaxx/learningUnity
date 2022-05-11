using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {//Responsável por realizar o movimento do projétil ao longo do eixo-X

    public Vector2 direction;
    public float speed;
    public GameObject explosion;
    public int damage = 1;

    protected Animator animator;
    protected float livingTime = 3f;
    protected Rigidbody2D rd2d;
    protected SpriteRenderer _renderer;

    protected virtual void Awake() {
        rd2d = GetComponent<Rigidbody2D>();
        animator = explosion.GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start() {//protected virtual para poder acessar os métodos nas classes filhas
        Destroy(gameObject, livingTime);
    }

    protected virtual void FixedUpdate() {
        Movement();        
    }

    private void Movement() {
        Vector2 movement = direction.normalized * speed;

        rd2d.velocity = movement;
    }

    public void Explode() {
        speed = 0f;

        _renderer.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        if (explosion != null) {
            explosion.SetActive(true);
        }

        Destroy(gameObject, 1.5f);
    }

}
