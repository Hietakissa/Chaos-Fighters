using HietakissaUtils;
using UnityEngine.UI;
using UnityEngine;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerController opponent;
    [SerializeField] SpriteRenderer spriteRend;
    [SerializeField] Direction defaultPointingDirection;
    [SerializeField] bool player1;

    [SerializeField] Image debugStaminaBar;

    //[Header("Variables")]
    Rigidbody2D rb;

    StateMachineController stateMachine;
    StaminaManager staminaManager = new StaminaManager();
    MovementSettingsSO movement;
    Direction pointingDirection;

    Vector2 inputVector;
    Vector2 acceleration;
    bool isGrounded;

    public bool IsPlayer1 => player1;
    public StateMachineController StateMachine => stateMachine;
    public SpriteRenderer SpriteRenderer => spriteRend;
    public Vector2 InputVector => inputVector;
    public Rigidbody2D RB => rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        movement = GameManager.Instance.MovementSettings;
        staminaManager.Init();

        stateMachine = new StateMachineController(this);
        stateMachine.EnterState(PlayerState.Moving);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) TakeDamage(1);

        HandleSpriteFlipping();
        HandleInput();
        staminaManager.Update();
        stateMachine.CurrentState.UpdateState();


        if (IsPlayer1)
        {

        }
        else
        {

        }




        void HandleSpriteFlipping()
        {
            if (opponent.transform.position.x > transform.position.x) pointingDirection = Direction.Right;
            else if (opponent.transform.position.x < transform.position.x) pointingDirection = Direction.Left;

            spriteRend.flipX = pointingDirection != defaultPointingDirection;
        }

        void HandleInput()
        {
            inputVector = Vector2.zero;

            if (IsPlayer1)
            {
                if (Input.GetKey(KeyCode.A)) inputVector.x = -1;
                if (Input.GetKey(KeyCode.D)) inputVector.x += 1;

                if (Input.GetKeyDown(KeyCode.W) && isGrounded) rb.AddForce(Vector2.up * movement.JumpForce, ForceMode2D.Impulse);
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftArrow)) inputVector.x = -1;
                if (Input.GetKey(KeyCode.RightArrow)) inputVector.x += 1;

                if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded) rb.AddForce(Vector2.up * movement.JumpForce, ForceMode2D.Impulse);
            }

            DebugTextManager.Instance.SetVariable($"Input", inputVector.ToString(), this);
            DebugTextManager.Instance.SetVariable($"Acceleration", acceleration.ToString(), this);
        }
    }

    void FixedUpdate()
    {
        stateMachine.CurrentState.FixedUpdateState();
    }

    public void HandleMovement(float speedMultiplier = 1f)
    {
        acceleration = inputVector * movement.MoveSpeed * speedMultiplier;
        acceleration.y = Mathf.Clamp(acceleration.y, -5f, 10f);


        float directionChangeMultiplier = ((inputVector.x >= 0 && rb.linearVelocityX < 0) || (inputVector.x < 0 && rb.linearVelocityX >= 0) ? 2.5f : 1f);

        Vector2 force = acceleration * directionChangeMultiplier * Time.deltaTime;
        if (inputVector.x == 0) force += new Vector2(-rb.linearVelocityX * movement.Drag * Time.deltaTime, 0f);
        if (rb.linearVelocityY < 0f) force += Physics2D.gravity;

        rb.AddForce(force);
        rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX, -movement.MaxSpeed, movement.MaxSpeed);
    }


    public void TakeDamage(int damage)
    {
        if (stateMachine.CurrentStateEnum == PlayerState.Blocking) staminaManager.TakeHit();
    }
}

enum Direction
{
    Left,
    Right
}