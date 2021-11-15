using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : StateMachine
{
    private GameManager _manager;
    public float playerSpeed = 5f;
    public float jumpSpeed = 7f;
    public float climbSpeed = 5f;
    public float gravityScale = 5f;

    // States
    public IdleState _idle;
    public RunningState _running;
    public ClimbingState _climbing;
    public JumpingState _jumping;
    public DroppingState _dropping;
    public DyingState _dying;

    // Firing Behaviour
    [SerializeField] private Transform bowLocation;
    [SerializeField] private GameObject arrowObject;

    // Others
    [HideInInspector] public Vector2 inputVector = new Vector2();
    [HideInInspector]public Rigidbody2D playerRigidbody;
    [HideInInspector]public Animator playerAnimator;
    private BoxCollider2D playerCollider;
    [SerializeField] BoxCollider2D feetCollider;

    private void Awake() 
    {
        FindComponents();
        InitializeStates();
    }

    private void FindComponents()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    private void InitializeStates()
    {
        _idle = new IdleState(this);
        _running = new RunningState(this);
        _climbing = new ClimbingState(this);
        _jumping = new JumpingState(this);
        _dropping = new DroppingState(this);
        _dying = new DyingState(this);
    }

    protected override void Start()
    {
        base.Start();
        _manager = FindObjectOfType<GameManager>();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void ChangeState(BaseState newState)
    {
        if (currentState != _dying)
        {
            base.ChangeState(newState);
        }
    }

    protected override BaseState GetInitialState()
    {
        return _idle;
    }

    public void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
        if (inputVector.y > Mathf.Epsilon && CheckLadderContact())
        {
            ChangeState(_climbing);
        }
        else if (Mathf.Abs(inputVector.x) > Mathf.Epsilon && currentState == _idle)
        {
            ChangeState(_running);
        }
    }

    public void OnJump(InputValue value)
    {
        if (CheckGroundContact() || CheckLadderContact())
        {
            ChangeState(_jumping);
        }
    }

    public void OnFire(InputValue value)
    {
        playerAnimator.SetTrigger("Shooting");
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + new Vector3(0,0,10f);
        Vector3 bowToTarget = targetPosition - bowLocation.position;
        FlipSprite((int) bowToTarget.x);

        GameObject arrow = Instantiate(arrowObject, bowLocation.position, transform.rotation);
        ArrowController arrowController = arrow.GetComponent<ArrowController>();
        arrowController.SetDirection(bowToTarget);
    }

    private Vector2 HandleClimbing(Vector2 movement)
    {
        return movement;
    }

    public void FlipSprite(int direction)
    {
        transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
    }

    public bool CheckLadderContact()
    {
        return playerCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }

    public bool CheckGroundContact()
    {
        return feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    public bool CheckBounceContact()
    {
        return feetCollider.IsTouchingLayers(LayerMask.GetMask("Bounce"));
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Enemy" && currentState != _dying)
        {
            ChangeState(_dying);
            _manager.TallyDeath();
        }   
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Hazards") && currentState != _dying)
        {
            ChangeState(_dying);
            _manager.TallyDeath();
        }
        else if (other.tag == "Coin")
        {
            _manager.CoinCollected();
            Destroy(other.gameObject);
        }
    }
}
