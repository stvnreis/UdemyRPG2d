using UnityEngine;

public class PlayerBasicAttackState : EntityState
{
    private const int attackComboLimit = 3;
    private const int firstComboAttackIndex = 1;
    private int basicAttackComboIndex = 1;
    private int attackDirection;
    private float attackVelocityTimer;
    private float lastAttackTime;
    private bool comboAttackQueued;

    public PlayerBasicAttackState(Player player, StateMachine stateMachine) : base(player, stateMachine, "basicAttack") { }

    public override void Enter()
    {
        base.Enter();

        comboAttackQueued = false;

        if (HasPassedComboResetTime())
            basicAttackComboIndex = firstComboAttackIndex;


        HandleAttackDirection();
        anim.SetInteger("basicAttackIndex", basicAttackComboIndex);
        ApplyAttackVelocity();
    }

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if (playerInput.Player.Attack.WasPressedThisFrame())
            QueueNextComboAttack();

        if (triggerCalled)
            HandleChangeState();

    }

    public override void Exit()
    {
        base.Exit();

        switch (basicAttackComboIndex)
        {
            case < attackComboLimit:
                basicAttackComboIndex++;
                break;
            default:
                basicAttackComboIndex = firstComboAttackIndex;
                break;
        }

        lastAttackTime = Time.time;
    }

    private void HandleAttackVelocity()
    {

        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0)
            player.SetVelocity(0f, 0f);
    }

    private void ApplyAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;
        Vector2 attackVelocity = player.attackVelocity[basicAttackComboIndex - 1];

        player.SetVelocity(attackVelocity.x * attackDirection, attackVelocity.y);
    }

    // if player is moving, use movement direction(allows player to rotate during combo). 
    // Else, use player facing direction
    private void HandleAttackDirection()
    {

        attackDirection = player.Movement.x != 0 ? ((int)player.Movement.x) : player.FacingDirection;
    }

    private void QueueNextComboAttack()
    {

        if (basicAttackComboIndex < attackComboLimit)
            comboAttackQueued = true;
    }

    private void HandleChangeState()
    {
        if (comboAttackQueued)
        {
            ChangeAnimatorBoolean(false);
            player.EnterAttackStateWithDelay();
        }
        else
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    private bool HasPassedComboResetTime() => Time.time - lastAttackTime > player.BasicAttackComboResetTime;
}