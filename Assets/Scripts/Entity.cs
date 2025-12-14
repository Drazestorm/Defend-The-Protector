using System;
using System.Collections;
using UnityEditor;
using UnityEngine;


public class Entity : MonoBehaviour {

    protected Rigidbody2D rb;
    protected Animator anim;
    protected Collider2D colld;
    protected SpriteRenderer sr;

    [Header("Health details")]
    [SerializeField] protected int maxHealth = 1;
    [SerializeField] protected int currentHealth;
    [SerializeField] private Material damageMaterial;
    [SerializeField] private float damageFeedbackDuration = 0.1f;
    private Coroutine damageFeedbackCoroutine;

    [Header("Attack details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask whatIsTarget;


    [Header("Collision details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    protected bool isGrounded;


    protected int facingDir = 1;
    protected bool facingRight = true;
    protected bool canMove = true;
    protected bool isDead = false;

    [Obsolete]
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        colld = GetComponent<Collider2D>();
        currentHealth = maxHealth;
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleMovement();
        HandleAnimation();
        HandleFlip();
        HandleCollision();
    }

    public void DamageTargets(){
        Collider2D[] targetColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsTarget);
        foreach (Collider2D target in targetColliders){
            Entity enemyTarget = target.GetComponent<Entity>();
            enemyTarget.TakeDamage();
        }
    }

    private void TakeDamage()
    {

        currentHealth--;
        PlayDamageFeedback();

        if (currentHealth <= 0)
        {
            Die();
            isDead = true;
        }


    }

    protected virtual void Die()
    {
        anim.enabled = false;
        colld.enabled = false;

        rb.gravityScale = 12;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 15);

        Destroy(gameObject, 3);

    }

    private void PlayDamageFeedback()
    {
        if (damageFeedbackCoroutine != null)
            StopCoroutine(damageFeedbackCoroutine);
        StartCoroutine(DamageFeedbackCo());
    }

    private IEnumerator DamageFeedbackCo()
    {
        Material originalMaterial = sr.material;
        sr.material = damageMaterial;
        yield return new WaitForSeconds(damageFeedbackDuration);
        sr.material = originalMaterial;
    }


    public virtual void EnableMovementAndJump(bool enable)
    {
        canMove = enable;
    }

    protected void HandleAnimation()
    {
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    protected virtual void HandleAttack() {
        if (isGrounded){
            anim.SetTrigger("attack");
        }
    }

    protected virtual void HandleMovement()
    {
        // Movement to handle in child classes
    }

    protected virtual void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    protected virtual void HandleFlip(){
        if (rb.linearVelocity.x > 0 && facingRight == false)
            Flip();
        else if (rb.linearVelocity.x < 0 && facingRight == true)
            Flip();
        
    }

    public void Flip() {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir *= -1;
    }

}
