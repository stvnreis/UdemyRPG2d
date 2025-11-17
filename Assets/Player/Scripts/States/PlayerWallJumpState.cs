public class PlayerWallJumpState : PlayerAiredState
{
    public PlayerWallJumpState(Player player, StateMachine stateMachine) : base(player, stateMachine, "jumpFall") { }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(player.WallJumpForce.x * -player.FacingDirection, player.WallJumpForce.y);
    }

    public override void Update()
    {
        base.Update();

        if (player.WallDetected) stateMachine.ChangeState(player.WallSlideState);
    }
}