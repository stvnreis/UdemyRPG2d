public abstract class PlayerAiredState : EntityState
{
    protected PlayerAiredState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) { }

    public override void Update()
    {
        base.Update();

        if (player.Movement.x != 0)
        {
            player.SetVelocity(player.Movement.x * player.moveSpeed * player.inAirMoveMultiplier, rb.linearVelocityY);
        }

        if (rb.linearVelocityY < 0 && stateMachine.CurrentState != player.DashState) stateMachine.ChangeState(player.FallState);
    }
}