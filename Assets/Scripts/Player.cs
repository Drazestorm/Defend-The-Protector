using UnityEngine;

public class Player : Entity
{
    private float xInput;
    private bool canJump = true;

    [Header("Movement details")]
    [SerializeField] protected float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8;
    
    [Header("Win details")]
    [SerializeField] private float winKillCount = 2;

    [Header("Sound details")]
    [SerializeField] private AudioClip attackSound;

    protected override void Update()
    {
        base.Update();
        HandleInput();
        PlayerWin();
    }

    protected override void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    private void AttemptToJump() {
        if (isGrounded && canJump)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void HandleInput() {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            AttemptToJump();
        if (Input.GetKeyDown(KeyCode.Mouse0))
            HandleAttack();
    }

    public override void EnableMovementAndJump(bool enable)
    {
        base.EnableMovementAndJump(enable);
        canJump = enable;
    }

    protected override void Die()
    {
        base.Die();
        UI.instance.EnableGameOverUI();
        SoundFXObject.instance.StopMusicFX();
    }

    protected void PlayerWin()
    {
        if (!isDead && UI.instance.getKillCount() == winKillCount)
        {
            UI.instance.EnableWinUI();
            EnableMovementAndJump(false);
            SoundFXObject.instance.StopMusicFX();
        }
    }

    protected override void HandleAttack()
    {
        base.HandleAttack();
        SoundFXObject.instance.PlaySoundFX(attackSound, transform, 0.3f);
    }

}
