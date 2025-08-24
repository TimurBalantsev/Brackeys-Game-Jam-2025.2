using System;
using HitBox;
using UnityEngine;

public class Player : Entity.Entity
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform followPoint;
    
    [SerializeField] private LayerMask damageableLayerMask;
    
    
    //TODO put new animation in attack without the FX

    public Transform FollowPoint => followPoint;
    public Animator Animator => animator;
    private PlayerState activeState;
    private InputManager.InputActions currentInputAction = InputManager.InputActions.NONE;
    private Camera mainCam;
    private bool attackInputHeld = false;

    private void Start()
    {
        mainCam = Camera.main;
        ChangeState(new PlayerIdleState());
        InputManager.Instance.OnAttackPerformed += InputManager_OnAttackPerformed;
        InputManager.Instance.OnAttackReleased += InputManager_OnAttackReleased;
        stats.Initialize();
    }

    private void InputManager_OnAttackPerformed(object sender, EventArgs e)
    {
        attackInputHeld = true;
        currentInputAction = InputManager.InputActions.ATTACK; 
        PlayerState newState = activeState.Input(currentInputAction);
        if (newState != null) ChangeState(newState);
    }
    
    private void InputManager_OnAttackReleased(object sender, EventArgs e)
    {
        attackInputHeld = false;
    }

    private void Update()
    {
        HandleFlip(mainCam!.ScreenToWorldPoint(Input.mousePosition));
        StateInput();
        StateUpdate();
    }
    
    private void ChangeState(PlayerState newState)
    {
        activeState?.Exit();
        activeState = newState;
        activeState.Enter(this);
    }

    private void StateInput()
    {
        //this only check movement states, as other states will be instantly overwritten by an event call.
        if (attackInputHeld)
        {
            currentInputAction = InputManager.InputActions.ATTACK;
        }
        else if (InputManager.Instance.GetMovementDirection().Equals(Vector2.zero))
        {
            currentInputAction = InputManager.InputActions.NONE;
        }
        else
        {
            currentInputAction = InputManager.InputActions.MOVE;
        }
        
        PlayerState newState = activeState.Input(currentInputAction);
        if(newState != null) ChangeState(newState);
    }

    private void StateUpdate()
    {
        PlayerState newState = activeState.Update(Time.deltaTime);
        if(newState != null) ChangeState(newState);
    }

    public void Move(Vector2 movementDirection)
    {
        Vector2 newPosition = movementDirection * (stats.speed * Time.deltaTime);
        Vector3 delta = new Vector3 (newPosition.x, newPosition.y, 0);
        // rigidBody.MovePosition(newPosition);
        transform.position += delta;
    }

    protected override void Die()
    {
        Debug.Log("Player died");
    }
}
