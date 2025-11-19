public class PlayerHeavyJumpAttackState : EntityState
{
    private const string AnimTrigger = "heavyJumpAttackTrigger";
    private bool hasTouchedGround;
    public PlayerHeavyJumpAttackState(Player player, StateMachine stateMachine) : base(player, stateMachine, "heavyJumpAttack") { }

    public override void Enter()
    {
        base.Enter();

        hasTouchedGround = false;

        player.SetVelocity(player.heavyJumpAttackVelocity.x * player.FacingDirection, player.heavyJumpAttackVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled && player.GroundDetected)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (player.GroundDetected && !hasTouchedGround)
        {
            hasTouchedGround = true;
            anim.SetTrigger(AnimTrigger);
            player.SetVelocity(0f, rb.linearVelocityY);
        }
    }
}