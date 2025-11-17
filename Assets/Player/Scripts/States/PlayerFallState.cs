public class PlayerFallState : PlayerAiredState
{
    public PlayerFallState(Player player, StateMachine stateMachine) : base(player, stateMachine, "jumpFall") { }

    public override void Update()
    {
        base.Update();

        if (player.GroundDetected) { stateMachine.ChangeState(player.IdleState); }

        if (player.WallDetected) { stateMachine.ChangeState(player.WallSlideState); }
    }
}