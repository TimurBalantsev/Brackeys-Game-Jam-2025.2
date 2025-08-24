
using UnityEngine;

public class PlayerWalkingState : PlayerState
{
    private Player player;

    public void Enter(Player player)
    {
        this.player = player;
        player.Animator.Play("orc_run");
    }

    public void Exit()
    {
    }

    public PlayerState Update(float deltaTime)
    {
        Vector2 movementDirection = InputManager.Instance.GetMovementDirection();
        player.Move(movementDirection);
        return null;
    }

    public PlayerState Input(InputManager.InputActions inputAction)
    {
        switch (inputAction)
        {
            case InputManager.InputActions.MOVE:
                return null;
            case InputManager.InputActions.NONE:
                return new PlayerIdleState();
            case InputManager.InputActions.ATTACK:
                return new PlayerAttackingState();
            default:
                return null;
        }
    }
}
