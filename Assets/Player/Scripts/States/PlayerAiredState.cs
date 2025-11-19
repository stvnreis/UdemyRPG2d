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

        HandleChangeState();
    }

    /* 
        method changes player state:
        if pressed lightAttack -> LightJumpAttack
        if pressed heavyAttack -> HeavyJumpAttack
        if is not dashing -> fall
    */
    private void HandleChangeState()
    {

        if (playerInput.Player.Attack.WasPressedThisFrame())
            stateMachine.ChangeState(player.LightJumpAttackState);
        else if (playerInput.Player.HeavyAttack.WasPressedThisFrame())
            stateMachine.ChangeState(player.HeavyJumpAttackState);
        else if (rb.linearVelocityY < 0 && stateMachine.CurrentState != player.DashState)
            stateMachine.ChangeState(player.FallState);
    }
}