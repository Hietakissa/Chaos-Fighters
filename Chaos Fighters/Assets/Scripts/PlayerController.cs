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
    [SerializeField] BoxCollider2D normalAttackHitbox;

    //[Header("Variables")]
    Rigidbody2D rb;

    StateMachineController stateMachine;
    StaminaManager staminaManager = new StaminaManager();
    MovementSettingsSO movement;
    Direction pointingDirection;

    Vector2 inputVector;
    Vector2 acceleration;
    bool isGrounded;

    public Vector2 ForceToAdd;

    public bool IsPlayer1 => player1;
    public StateMachineController StateMachine => stateMachine;
    public SpriteRenderer SpriteRenderer => spriteRend;
    public Vector2 InputVector => inputVector;
    public Rigidbody2D RB => rb;
    public bool IsGrounded => isGrounded;
    public StaminaManager StaminaManager => staminaManager;
    public Image DebugStaminaBar => debugStaminaBar;
    public Vector2 FacingDirectionVector => (pointingDirection == Direction.Right ? Vector2.right : Vector2.left) + Vector2.up;
    public BoxCollider2D NormalAttackHitbox => normalAttackHitbox;
    public PlayerController Opponent => opponent;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        movement = GameManager.Instance.MovementSettings;
        staminaManager.Init(this);

        stateMachine = new StateMachineController(this);
        stateMachine.EnterState(PlayerState.Idling);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) TakeDamage(1);

        isGrounded = false;
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].gameObject.isStatic)
            {
                isGrounded = true;
                DebugTextManager.Instance.SetVariable("Standing On", colls[i].gameObject.name, this);
                break;
            }
        }

        DebugTextManager.Instance.SetVariable("Grounded", isGrounded.ToString(), this);


        HandleSpriteFlipping();
        HandleInput();
        staminaManager.Update();
        stateMachine.CurrentState.UpdateState();
        HandleStateTransitions();



        void HandleSpriteFlipping()
        {
            //if (opponent.transform.position.x > transform.position.x) pointingDirection = Direction.Right;
            //else if (opponent.transform.position.x < transform.position.x) pointingDirection = Direction.Left;
            if (inputVector.x > 0) pointingDirection = Direction.Right;
            else if (inputVector.x < 0) pointingDirection = Direction.Left;

            spriteRend.flipX = pointingDirection != defaultPointingDirection;
        }

        void HandleInput()
        {
            inputVector = Vector2.zero;

            if (Input.GetKey(GetKeyCodeForKey(Key.Left))) inputVector.x = -1;
            if (Input.GetKey(GetKeyCodeForKey(Key.Right))) inputVector.x += 1;

            if (Input.GetKeyDown(GetKeyCodeForKey(Key.Jump)) && isGrounded) rb.AddForce(Vector2.up * GameManager.Instance.MovementSettings.JumpForce, ForceMode2D.Impulse);

            DebugTextManager.Instance.SetVariable($"Input", inputVector.ToString(), this);
            DebugTextManager.Instance.SetVariable($"Acceleration", acceleration.ToString(), this);
        }

        void HandleStateTransitions()
        {
            for (int i = 0; i < stateMachine.States.Count; i++)
            {
                State state = stateMachine.States[i];
                PlayerState stateEnum = GetEnumForState(state);

                //if (state == stateMachine.CurrentState && state.EnterPredicate(this)) return;
                if (stateMachine.CurrentState.CanExit && stateMachine.CurrentState.ValidExitStates.Contains(stateEnum) && state.EnterPredicate(this)) stateMachine.EnterState(stateEnum);
            }
        }
    }

    void FixedUpdate()
    {
        stateMachine.CurrentState.FixedUpdateState();
    }

    public void HandleMovement(float speedMultiplier = 1f, float horizontalDragMultiplier = 1f)
    {
        acceleration = inputVector * movement.MoveSpeed * speedMultiplier;
        acceleration.y = Mathf.Clamp(acceleration.y, -5f, 10f);


        float directionChangeMultiplier = ((inputVector.x >= 0 && rb.linearVelocityX < 0) || (inputVector.x < 0 && rb.linearVelocityX >= 0) ? 2.5f : 1f);

        Vector2 force = acceleration * directionChangeMultiplier * Time.deltaTime;
        if (inputVector.x == 0) force += new Vector2(-rb.linearVelocityX * movement.Drag * Time.deltaTime, 0f) * horizontalDragMultiplier;
        if (rb.linearVelocityY < 0f) force += Physics2D.gravity * movement.GravityForce;

        if (ForceToAdd.magnitude > 0)
        {
            rb.AddForce(ForceToAdd, ForceMode2D.Impulse);
            ForceToAdd = Vector2.zero;
        }
        rb.AddForce(force);
        rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX, -movement.MaxSpeed, movement.MaxSpeed);
    }


    public void TakeDamage(int damage)
    {
        if (stateMachine.CurrentStateEnum == PlayerState.Blocking) staminaManager.TakeHit();
    }

    public KeyCode GetKeyCodeForKey(Key key)
    {
        if (player1)
        {
            return key switch
            {
                Key.Jump => KeyCode.W,
                Key.Block => KeyCode.S,
                Key.Attack => KeyCode.C,
                Key.Special => KeyCode.V,
                Key.Left => KeyCode.A,
                Key.Right => KeyCode.D,
                _ => KeyCode.None
            };
        }
        else
        {
            return key switch
            {
                Key.Jump => KeyCode.UpArrow,
                Key.Block => KeyCode.DownArrow,
                Key.Attack => KeyCode.Comma,
                Key.Special => KeyCode.Period,
                Key.Left => KeyCode.LeftArrow,
                Key.Right => KeyCode.RightArrow,
                _ => KeyCode.None
            };
        }
    }
    PlayerState GetEnumForState(State state)
    {
        if (state is JumpingState) return PlayerState.Jumping;
        else if (state is MovingState) return PlayerState.Moving;
        else if (state is AttackState) return PlayerState.Attacking;
        else if (state is BlockingState) return PlayerState.Blocking;
        else if (state is IdleState) return PlayerState.Idling;
        else return PlayerState.Idling;
    }
}

enum Direction
{
    Left,
    Right
}

public enum Key
{
    Jump,
    Block,
    Attack,
    Special,
    Left,
    Right
}