public class PlayerDashState : EntityState
{
    private float originalGravityScale;
    private int dashDir;
    public PlayerDashState(Player player, StateMachine stateMachine) : base(player, stateMachine, "dash") { }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0f;
        dashDir = player.Movement.x != 0 ? ((int)player.Movement.x) : player.FacingDirection; ;

        if (!player.GroundDetected) player.hasDashedMidAir = true;
    }

    public override void Update()
    {
        base.Update();
        CancelDashIfNeeded();

        player.SetVelocity(player.DashForce * dashDir, 0f);

        // early return when player is still dashing
        if (stateTimer > 0) return;

        HandleStateUpdate();
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0f, 0f);
        rb.gravityScale = originalGravityScale;
    }

    /**
        Method should change to: 
        -> Idle state when ground is detected
        -> Fall when none is detected
    **/
    private void HandleStateUpdate()
    {

        if (player.GroundDetected)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else
        {
            stateMachine.ChangeState(player.FallState);
        }
    }

    private void CancelDashIfNeeded()
    {

        if (player.WallDetected)
        {
            if (player.GroundDetected) stateMachine.ChangeState(player.IdleState);
            else stateMachine.ChangeState(player.WallSlideState);
        }
    }
}