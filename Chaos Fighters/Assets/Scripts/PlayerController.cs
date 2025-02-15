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
    BoxCollider2D coll;

    MovementSettingsSO movement;
    StaminaManager staminaManager = new StaminaManager();
    Direction pointingDirection;

    Vector2 inputVector;
    Vector2 acceleration;
    bool isGrounded;
    bool blocking = true;

    public bool IsPlayer1 => player1;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        movement = GameManager.Instance.MovementSettings;
        staminaManager.Init();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) TakeDamage(1);

        HandleSpriteFlipping();
        HandleInput();
        //HandleMovement();

        if (!player1) return;
        staminaManager.Update();
        debugStaminaBar.fillAmount = staminaManager.Stamina / GameManager.Instance.StaminaSettings.MaxStamina;
    }

    void FixedUpdate()
    {
        HandleMovement();
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

            //if (Input.GetKeyDown(KeyCode.W)) velocity.y = movement.JumpForce;
            if (Input.GetKeyDown(KeyCode.W)) rb.AddForce(Vector2.up * movement.JumpForce, ForceMode2D.Impulse);
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow)) inputVector.x = -1;
            if (Input.GetKey(KeyCode.RightArrow)) inputVector.x += 1;

            if (Input.GetKeyDown(KeyCode.UpArrow)) rb.AddForce(Vector2.up * movement.JumpForce, ForceMode2D.Impulse);
        }

        if (player1) DebugTextManager.Instance.AddLine($"Input: {inputVector}");
        if (player1) DebugTextManager.Instance.AddLine($"Acceleration: {acceleration}");
    }

    void HandleMovement()
    {
        acceleration = inputVector * movement.MoveSpeed;
        //acceleration.y += -9.81f * movement.GravityForce * Time.deltaTime; // Gravity
        
        //velocity.x += velocity.x * -(movement.Drag * movement.Drag) * Time.deltaTime; // Horizontal drag

        //acceleration.x = Mathf.Clamp(acceleration.x, -movement.MaxSpeed, movement.MaxSpeed);
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
        if (blocking) staminaManager.TakeHit();
    }
}

enum Direction
{
    Left,
    Right
}