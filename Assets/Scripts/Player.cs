using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundType {
    Nome,
    Soft,
    Hard
}

public class Player : MonoBehaviour {

    [Header("Ground Properties")]//Propriedades terrestres
    public LayerMask groundLayer;//Layer que vai ser adicionada no sprite floor
    public float groundDistance;
    public bool isGrounded;
    public Vector3[] footOffset;    

    public float speed = 3f;
    public float jumpForce = 5f;
    private Rigidbody2D rb2d;
    private Animator animator;
    private Vector2 movement;//Trabalhar com sistemas vetoriais
    private float xVelocity;

    //Controlar a direção do personagem
    private int direction = 1;
    private float originalXScale;

    //Variáveis para verificar se o personagem está tocando o chão
    RaycastHit2D leftCheck;
    RaycastHit2D rightCheck;

    //Arma
    private Weapon weapon;

    //Animações
    private bool isFire;

    //Efeitos sonoros
    [Header("Audio")]
    [SerializeField] AudioCharacter audioPlayer = null;

    private LayerMask softGround;
    private LayerMask hardGround;
    private GroundType groundType;
    private Collider2D col;

    // Start is called before the first frame update
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        originalXScale = transform.localScale.x;

        softGround = LayerMask.GetMask("Ground");
        hardGround = LayerMask.GetMask("GroundHard");
        col = GetComponent<Collider2D>();

        weapon = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    void Update() {
        PhysicsCheck();

        if (isFire == false) {
            float horizontal = Input.GetAxisRaw("Horizontal");//Horizontal já está configurada no Unity em Project Settings
            movement = new Vector2(horizontal, 0);

            if (xVelocity * direction < 0f) {
                Flip();
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire1") && isFire == false && isGrounded) {
            movement = Vector2.zero;
            rb2d.velocity = Vector2.zero;

            animator.SetTrigger("fire");
        }

    }

    //Utilizado para objetos com física
    private void FixedUpdate() {
        if(!isFire) {
            xVelocity = movement.normalized.x * speed;
            rb2d.velocity = new Vector2(xVelocity, rb2d.velocity.y);
        }

        UpdateGround();

        if(isGrounded) {
            audioPlayer.PlaySteps(groundType, Mathf.Abs(xVelocity));
        }
    }

    private void LateUpdate() {

        if (GameManager.isGameOver) {
            animator.SetTrigger("die");
            return;
        }

        animator.SetFloat("xVelocity", Mathf.Abs(xVelocity));
        animator.SetFloat("yVelocity", rb2d.velocity.y);
        animator.SetBool("isGrounded", isGrounded);
        
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("fire")) {//Layer index 0 é a base layer
            isFire = true;
        } else {
            isFire = false;
        }
    }

    public void Shoot() {
        if (weapon != null) {
            weapon.Shoot();
        }
    }

    private void Flip() {//Não altera Y e Z nesse caso
        direction *= -1;//-1*-1=1 ou -1*1=-1 para girarmos o eixo X do persongem
        Vector3 scale = transform.localScale;

        scale.x = originalXScale * direction;
        transform.localScale = scale;
    }

    private void PhysicsCheck() {
        isGrounded = false;
        leftCheck = Reycast(new Vector2(footOffset[0].x, footOffset[0].y), Vector2.down, groundDistance, groundLayer);
        rightCheck = Reycast(new Vector2(footOffset[1].x, footOffset[1].y), Vector2.down, groundDistance, groundLayer);
    
        if (leftCheck || rightCheck) {//Variáveis trazem o Hit do personagem no chão
            isGrounded = true;//Se está no chão então pode pular
        }
    }

    private RaycastHit2D Reycast(Vector3 origin, Vector2 rayDirection, float lenght, LayerMask mask) {
        Vector3 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + origin, rayDirection, lenght, mask);

        // Debug
        // Color color = hit ? Color.red : Color.green;
        // Debug.DrawRay(pos + origin, rayDirection * lenght, color);

        return hit;
    }

    private void UpdateGround() {//Usa o colider para verificar qual layer o personagem está tocando
        if (col.IsTouchingLayers(softGround)) {
            groundType = GroundType.Soft;
        } else if (col.IsTouchingLayers(hardGround)) {
            groundType = GroundType.Hard;
        } else {
            groundType = GroundType.Nome;
        }
    }

}
