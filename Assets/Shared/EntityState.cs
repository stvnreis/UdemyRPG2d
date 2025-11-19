using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;
    protected Rigidbody2D rb;
    protected readonly Animator anim;
    protected readonly PlayerInputSet playerInput;
    protected float stateTimer;
    protected bool triggerCalled;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;

        anim = player.Anim;
        rb = player.Rb;
        playerInput = player.Input;
    }

    public virtual void Enter()
    {
        ChangeAnimatorBoolean(true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        anim.SetFloat("yVelocity", rb.linearVelocityY);

        if (playerInput.Player.Dash.WasPressedThisFrame() && CanDash())
            stateMachine.ChangeState(player.DashState);
    }

    public virtual void Exit()
    {
        ChangeAnimatorBoolean(false);
    }

    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }

    protected void ChangeAnimatorBoolean(bool state) => anim.SetBool(animBoolName, state);

    private bool CanDash() => !player.WallDetected && stateMachine.CurrentState != player.DashState && (player.GroundDetected || !player.HasDashedMidAir);
}
