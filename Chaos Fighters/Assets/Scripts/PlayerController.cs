using HietakissaUtils.QOL;
using System.Collections;
using HietakissaUtils;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

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

    [SerializeField] TextMeshProUGUI victoryText;
    [SerializeField] SceneReference menuScene;

    //[Header("Variables")]
    Rigidbody2D rb;

    StateMachineController stateMachine;
    StaminaManager staminaManager = new StaminaManager();
    MovementSettingsSO movement;
    Direction pointingDirection;

    Vector2 inputVector;
    Vector2 acceleration;
    bool isGrounded;
    public bool IsBlocking;

    int lives;
    int health;

    Vector2 startPos;

    bool enabledInput = true;

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
    public event Action OnHit;


    void Awake()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();

        movement = GameManager.Instance.MovementSettings;
        
        staminaManager.Init(this);

        stateMachine = new StateMachineController(this);
        stateMachine.EnterState(PlayerState.Idling);

        if (RoastResult.RoastComplete)
        {
            RoastResult.RoastLoads++;
            
            if (RoastResult.RoastLoads == 2)
            {
                if (IsPlayer1)
                {
                    SetLives(RoastResult.Player1LivesBeforeRoast);
                    opponent.SetLives(RoastResult.Player2LivesBeforeRoast);

                    health = RoastResult.Player1HealthBeforeRoast;
                    opponent.health = RoastResult.Player2HealthBeforeRoast;

                    TakeDamage(RoastResult.Player1Damage);
                    opponent.TakeDamage(RoastResult.Player2Damage);
                }
                else
                {
                    SetLives(RoastResult.Player2LivesBeforeRoast);
                    opponent.SetLives(RoastResult.Player1LivesBeforeRoast);

                    health = RoastResult.Player2HealthBeforeRoast;
                    opponent.health = RoastResult.Player1HealthBeforeRoast;

                    TakeDamage(RoastResult.Player2Damage);
                    opponent.TakeDamage(RoastResult.Player1Damage);
                }

                // Last to load > clean up
                RoastResult.RoastComplete = false;
                RoastResult.RoastLoads = 0;
            }
        }
        else
        {
            // Roast not complete > entered from main menu > set default lives and health
            health = GameManager.Instance.CombatSettings.MaxHealth;
            lives = 2;
        }

        DebugTextManager.Instance.SetVariable("Health", health.ToString(), this);
    }

    void Update()
    {
        isGrounded = transform.position.y > -0.8f && transform.position.y < 0f;
        //isGrounded = false;
        //Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, 0.45f);
        //for (int i = 0; i < colls.Length; i++)
        //{
        //    DebugTextManager.Instance.SetVariable($"Standing On[{i}]", $"{colls[i].gameObject.name}{(colls[i].gameObject.isStatic ? "(Ground)" : "")}", this);
        //    if (colls[i].gameObject.isStatic)
        //    {
        //        isGrounded = true;
        //        break;
        //    }
        //}

        DebugTextManager.Instance.SetVariable("Grounded", isGrounded.ToString(), this);


        if (enabledInput)
        {
            HandleSpriteFlipping();
            HandleInput();
            staminaManager.Update();
            stateMachine.CurrentState.UpdateState();
            HandleStateTransitions();
        }


        void HandleSpriteFlipping()
        {
            if (opponent.transform.position.x > transform.position.x) pointingDirection = Direction.Right;
            else if (opponent.transform.position.x < transform.position.x) pointingDirection = Direction.Left;
            //if (inputVector.x > 0) pointingDirection = Direction.Right;
            //else if (inputVector.x < 0) pointingDirection = Direction.Left;

            spriteRend.flipX = pointingDirection != defaultPointingDirection;
        }

        void HandleInput()
        {
            inputVector = Vector2.zero;

            if (Input.GetKey(GetKeyCodeForKey(Key.Left))) inputVector.x = -1;
            if (Input.GetKey(GetKeyCodeForKey(Key.Right))) inputVector.x += 1;

            if (Input.GetKeyDown(GetKeyCodeForKey(Key.Jump)) && isGrounded && !IsBlocking) rb.AddForce(Vector2.up * GameManager.Instance.MovementSettings.JumpForce, ForceMode2D.Impulse);

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
                //if (stateMachine.CurrentState.EnterPredicate(this)) return;
                //if (IsBlocking && stateMachine.CurrentStateEnum == PlayerState.Blocking) return;
                if (stateMachine.CurrentState.CanExit && stateMachine.CurrentState.ValidExitStates.Contains(stateEnum) && state.EnterPredicate(this)) stateMachine.EnterState(stateEnum);
            }
        }
    }

    void FixedUpdate()
    {
        if (enabledInput) stateMachine.CurrentState.FixedUpdateState();
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
        else
        {
            health -= damage;
            DebugTextManager.Instance.SetVariable("Health", health.ToString(), this);

            if (health <= 0)
            {
                LoseLife();
            }
            else if (health <= 50 && !RoastResult.RoastThisRound)
            {
                StartCoroutine(LoadRoastCor());
            }
        }
        OnHit?.Invoke();
    }

    void SetLives(int lives)
    {
        this.lives = lives;

        DebugTextManager.Instance.SetVariable("Lives", lives.ToString(), this);

        if (lives < 0)
        {
            enabledInput = false;
            if (IsPlayer1) victoryText.text = "Bitch Star Wins!";
            else victoryText.text = "Pawssacre Wins!";

            StartCoroutine(LoadMenuSceneAfterSecondsCor(5f));
        }
    }

    void LoseLife()
    {
        lives--;
        RoastResult.RoastThisRound = false;


        DebugTextManager.Instance.SetVariable("Lives", lives.ToString(), this);

        if (lives < 0)
        {
            enabledInput = false;
            if (IsPlayer1) victoryText.text = "Bitch Star Wins!";
            else victoryText.text = "Pawssacre Wins!";

            StartCoroutine(LoadMenuSceneAfterSecondsCor(5f));
        }
        else
        {
            ResetRound();
            opponent.ResetRound();
        }
    }

    public void ResetRound()
    {
        transform.position = startPos;
        pointingDirection = defaultPointingDirection;
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;
        StartCoroutine(EnableRBAfterSecondsCor(0.25f));
        //ForceToAdd = -rb.linearVelocity;
        staminaManager.ReplenishStamina(GameManager.Instance.StaminaSettings.MaxStamina);
        health = GameManager.Instance.CombatSettings.MaxHealth;

        DebugTextManager.Instance.SetVariable("Health", health.ToString(), this);
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

    IEnumerator EnableRBAfterSecondsCor(float seconds)
    {
        enabledInput = false;
        rb.linearVelocity = Vector2.zero;
        yield return QOL.WaitForSeconds.Get(seconds);
        rb.linearVelocity = Vector2.zero;
        rb.simulated = true;
        enabledInput = true;
    }

    IEnumerator LoadMenuSceneAfterSecondsCor(float seconds)
    {
        yield return QOL.WaitForSeconds.Get(seconds);
        stateMachine.CurrentState?.ExitState();
        yield return SceneManager.LoadSceneAsync(0);
    }

    IEnumerator LoadRoastCor()
    {
        enabledInput = false;
        opponent.enabledInput = false;

        if (IsPlayer1)
        {
            RoastResult.Player1LivesBeforeRoast = lives;
            RoastResult.Player2LivesBeforeRoast = opponent.lives;

            RoastResult.Player1HealthBeforeRoast = health;
            RoastResult.Player2HealthBeforeRoast = opponent.health;
        }
        else
        {
            RoastResult.Player1LivesBeforeRoast = opponent.lives;
            RoastResult.Player2LivesBeforeRoast = lives;

            RoastResult.Player1HealthBeforeRoast = opponent.health;
            RoastResult.Player2HealthBeforeRoast = health;
        }

        RoastResult.RoastThisRound = true;
        stateMachine.CurrentState?.ExitState();
        yield return SceneManager.LoadSceneAsync(2);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Kill Trigger"))
        {
            LoseLife();
        }
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