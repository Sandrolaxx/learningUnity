using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Enemy Properties")]
    public float speed;
    public float attackDistance;//Quando player estiver a essa distância o NPC vai atirar

    [HideInInspector]
    public int direction;

    [Header("RayCast Properties")]
    public LayerMask layerGround;
    public float lengthGround;//Objeto em branco
    public float lengthWall;//Objeto em branco
    public Transform rayPointGround;
    public Transform rayPointWall;
    public RaycastHit2D hitGround;
    public RaycastHit2D hitWall;
    
    protected Animator animator;
    protected Rigidbody2D rb2d;

    protected Transform player;
    protected float playerDistance;

    protected virtual void Awake() {
        player = FindObjectOfType<Player>().transform;
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        direction = (int)transform.localScale.x;
    }

    protected virtual void Update() {
        GetDistancePlayer();
    }

    protected virtual void Flip() {//Outra maneira de girar o personagem
        direction *= -1;
        transform.localScale = new Vector2(direction, transform.localScale.y);
    }

    protected virtual RaycastHit2D RaycastGround() {
        hitGround = Physics2D.Raycast(rayPointGround.position, Vector2.down, lengthGround, layerGround);

        // Debug
        Color color = hitGround ? Color.red : Color.green;
        Debug.DrawRay(rayPointGround.position, Vector2.down * lengthGround, color);

        return hitGround;
    }

    protected virtual RaycastHit2D RaycastWall() {
        hitWall = Physics2D.Raycast(rayPointWall.position, Vector2.right * direction, lengthWall, layerGround);

        // Debug
        Color color = hitWall ? Color.yellow : Color.blue;
        Debug.DrawRay(rayPointWall.position, Vector2.right * direction * lengthWall, color);

        return hitWall;
    }

    protected void GetDistancePlayer() {
        playerDistance = player.position.x - transform.position.x;
        Debug.Log(playerDistance);
    }

}
