public class PlayerIdleState : PlayerGroundedState
{

    public PlayerIdleState(Player player, StateMachine stateMachine) : base(player, stateMachine, "idle") { }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0f, rb.linearVelocityY);
    }

    public override void Update()
    {
        base.Update();

        if (player.Movement.x == player.FacingDirection && player.WallDetected) return;

        if (player.Movement.x != 0)
            stateMachine.ChangeState(player.MoveState);
    }
}