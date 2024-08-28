using UnityEngine;

public class PlayerDropItemState : PlayerBaseState
{
    private const string DropAnimationName = "Drop Item";
    private const string DropAnimationTag = "Drop Item";
    private const float CrossFadeDuration = .2f;

    private readonly InventoryItem ItemToDrop;

    private bool isItemDropped;

    public PlayerDropItemState(PlayerStateMachine stateMachine,
        InventoryItem itemToDrop) : base(stateMachine)
    {
        ItemToDrop = itemToDrop;
    }

    public override void Enter()
    {
        SetAnimation(DropAnimationName, CrossFadeDuration);

        StateMachine.Selector.DisableSelector();
    }

    public override void Exit()
    {
        StateMachine.Selector.EnableSelector();
    }

    public override void Tick()
    {
        float normalizedTime = GetNormalizedTime(DropAnimationTag);

        if(!isItemDropped && normalizedTime >= StateMachine.DropItemValues.DropItemNormalizedTime)
        {
            StateMachine.Inventory.RemoveItem(ItemToDrop);
            StateMachine.ItemDropper.DropItem(ItemToDrop.ItemData);
            isItemDropped = true;
        }

        if(normalizedTime >= StateMachine.DropItemValues.ExitStateNormalizedTime)
        {
            OnFreelook();
            return;
        }
    }
}
