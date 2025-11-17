public class PlayerJumpState : PlayerAiredState
{
    public PlayerJumpState(Player player, StateMachine stateMachine) : base(player, stateMachine, "jumpFall") { }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(rb.linearVelocityX, player.jumpForce);
    }
}