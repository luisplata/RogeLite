using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody2D rb;
    public Tilemap tilemap;

    private float moveSpeed = 5f;
    private PlayerMediator mediator;

    private bool playerCanMove;

    public void Initialize(PlayerMediator mediator)
    {
        this.mediator = mediator;
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