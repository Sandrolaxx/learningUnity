using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : Enemy {

    [Header("Attack Properties")]
    public float timerWaitAttack;
    public float timerShootAttack;

    private Weapon weapon;

    private bool idle;
    private bool shoot;
    
    protected override void Awake() {
        base.Awake();
    }
    
    void Start() {
        weapon = GetComponentInChildren<Weapon>();
    }

    protected override void Update() {
        base.Update();
        if(!RaycastGround().collider || RaycastWall().collider) {
            Flip();
        }    
    }

    private void FixedUpdate() {

        if (CanAttack()) {
            Attack();
        } else {
            Movement();
        }

    }

    private void LateUpdate() {
        animator.SetBool("idle", idle);
    }

    private void Movement() {
        float horizontalVelocity = speed;
        horizontalVelocity = horizontalVelocity * direction;
        rb2d.velocity = new Vector2(horizontalVelocity, rb2d.velocity.y);
        idle = false;
    }

    private bool CanAttack() {//Se o player está no range de attack ele atira
        return ((float)Mathf.Abs(playerDistance)) <= attackDistance;
    }

    private void Attack() {
        StopMovement();
        DistanceFlipPlayer();
        CanShoot();
    }

    private void StopMovement() {
        rb2d.velocity = Vector3.zero;
        idle = true;
    }

    private void DistanceFlipPlayer() {//Verificar o lado do player
        if (playerDistance >= 0) {//Se maior que zero significa que o player está na direita
            if (direction == -1) {//Se -1 o inimigo está olhando para a esquerda, então viramos
                Flip();
            }
        } else {
            if (direction == 1) {//Se 1 o inimigo está olhando para a direita, então viramos
                Flip();
            }
        }
    }

    private void CanShoot() {
        if (!shoot) {
            StartCoroutine("Shoot");
        }
    }

    private IEnumerator Shoot() {//Utilizando coroutine
        shoot = true;

        yield return new WaitForSeconds(timerWaitAttack);

        AnimationShoot();

        yield return new WaitForSeconds(timerShootAttack);

        shoot = false;
    }

    private void AnimationShoot() {
        animator.SetTrigger("shoot");
    }

    private void ShootPrefab() {
        if (weapon != null) {
            weapon.Shoot();
        }
    }

    public void Die() {
        rb2d.velocity = Vector2.zero;
        animator.SetTrigger("die");
    }

    private void OnDisabled() {
        Destroy(gameObject, 3f);
    }

}
