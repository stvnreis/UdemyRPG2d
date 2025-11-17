using UnityEngine;

public class PlayerBasicAttackState : EntityState
{
    private float attackVelocityTimer;
    public PlayerBasicAttackState(Player player, StateMachine stateMachine) : base(player, stateMachine, "basicAttack") { }

    public override void Enter()
    {
        base.Enter();

        GenerateAttackVelocity();
    }

    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if (triggerCalled) stateMachine.ChangeState(player.IdleState);
    }

    private void HandleAttackVelocity()
    {

        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0)
            player.SetVelocity(0f, 0f);
    }

    private void GenerateAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(player.attackVelocity.x * player.FacingDirection, player.attackVelocity.y);
    }
}