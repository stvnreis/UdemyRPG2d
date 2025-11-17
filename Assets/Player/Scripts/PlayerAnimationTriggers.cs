using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player;

    public void Awake()
    {

        player = GetComponentInParent<Player>();
    }

    public void TriggerCurrentState()
    {
        player.CallAnimationTrigger();
    }
}
