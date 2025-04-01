using UnityEngine;

public class Player : MonoBehaviour
{
    private Joystick joystick;
    [SerializeField] private Rigidbody2D rb;

    private float moveSpeed = 5f;
    private PlayerMediator mediator;

    private bool playerCanMove;

    public void Initialize(PlayerMediator mediatorInComming, Joystick joystick1)
    {
        mediator = mediatorInComming;
        joystick = joystick1;
    }

    public void ApplyStats()
    {
        if (!mediator) return;
        moveSpeed = mediator.PlayerStats.MoveSpeed;
    }

    void Update()
    {
        if (!mediator || !playerCanMove) return;
        Vector2 moveDirection = new Vector2(joystick.Horizontal, joystick.Vertical);

        //moveDirection.Normalize();

        rb.linearVelocity = moveDirection * moveSpeed;
    }

    public void DisableControls()
    {
        mediator = null;
    }

    public void CanMove(bool canMove)
    {
        playerCanMove = canMove;
    }
}