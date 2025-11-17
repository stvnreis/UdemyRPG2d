public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, StateMachine stateMachine) : base(player, stateMachine, "move") { }

    public override void Update()
    {

        base.Update();

        player.SetVelocity(player.Movement.x * player.moveSpeed, rb.linearVelocityY);

        if (player.Movement.x == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}