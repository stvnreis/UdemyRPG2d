public class PlayerLightJumpAttackState : EntityState
{
    public PlayerLightJumpAttackState(Player player, StateMachine stateMachine) : base(player, stateMachine, "lightJumpAttack") { }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {

            if (player.GroundDetected) stateMachine.ChangeState(player.IdleState);
            else stateMachine.ChangeState(player.FallState);
        }
    }
}