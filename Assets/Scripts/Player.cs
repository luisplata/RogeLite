using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody2D rb;
    public Tilemap tilemap;

    private float moveSpeed = 5f;
    private PlayerMediator mediator;

    public void Initialize(PlayerMediator mediator)
    {
        this.mediator = mediator;
    }

    public void ApplyStats()
    {
        moveSpeed = mediator.playerStats.MoveSpeed;
    }

    void Update()
    {
        Vector2 moveDirection = new Vector2(joystick.Horizontal, joystick.Vertical);

        //moveDirection.Normalize();

        rb.linearVelocity = moveDirection * moveSpeed;
    }
}