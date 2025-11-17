using UnityEngine;

public class PlayerWallSlideState : EntityState
{
    public PlayerWallSlideState(Player player, StateMachine stateMachine) : base(player, stateMachine, "wallSlide") { }

    public override void Update()
    {
        base.Update();
        HandleWallSlide();

        if (playerInput.Player.Jump.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.WallJumpState);
        }

        if (!player.WallDetected)
            stateMachine.ChangeState(player.FallState);

        if (player.GroundDetected)
        {

            stateMachine.ChangeState(player.IdleState);
            player.Flip();
        }


    }

    private void HandleWallSlide()
    {
        if (player.Movement.y < 0)
        {
            player.SetVelocity(player.Movement.x, rb.linearVelocityY);
        }
        else
        {
            player.SetVelocity(player.Movement.x, rb.linearVelocityY * player.WallGripMultiplier);
        }
    }
}