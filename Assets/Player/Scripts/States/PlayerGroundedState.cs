public abstract class PlayerGroundedState : EntityState
{
    protected PlayerGroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) { }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocityY < 0 && !player.GroundDetected) stateMachine.ChangeState(player.FallState);

        if (playerInput.Player.Jump.WasPressedThisFrame())
        {

            stateMachine.ChangeState(player.JumpState);
        }

        if (playerInput.Player.Attack.WasPressedThisFrame())
            stateMachine.ChangeState(player.BasicAttackState);
    }
}