using UnityEngine;

public class ObjectToProtect : Entity
{
    [Header("NPC details")]
    [SerializeField] private Transform player;

    [SerializeField] private AudioClip musicAudio;

    private void Awake()
    {
        SoundFXObject.instance.PlayMusicFX(musicAudio, 0.1f);
    }

    protected override void Update()
    {
        HandleFlip();
    }

    protected override void HandleFlip()
    {
        if (player.transform.position.x > transform.position.x && facingRight == false)
            Flip();
        else if (player.transform.position.x < transform.position.x && facingRight == true)
            Flip();
    }

    protected override void Die()
    {
        base.Die();
        UI.instance.EnableGameOverUI();
    }
}
