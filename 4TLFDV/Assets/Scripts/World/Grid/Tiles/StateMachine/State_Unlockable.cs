using UnityEngine;

public class State_Unlockable : TileState
{
    private readonly int unlockCost;

    public State_Unlockable(FarmTile tile, int cost) : base(tile)
    {
        unlockCost = cost;
    }

    public override void OnEnterState()
    {
        tile.SetSprite(tile.spriteUnlockableTile);
        TogglePriceLabel(true);
        Grid_Manager.Instance.ExpandLockedVisibleAround(tile.Position);
    }

    public override void UpdateState()
    {
        if (Grid_Manager.Instance.Player.TrySpendGold(unlockCost))
        {
            TogglePriceLabel(false);
            tile.SetState(new State_Grass(tile));
        }
        else
        {
            Debug.Log("Not enough gold.");
        }
    }

    public override void OnExitState()
    {
        Grid_Manager.Instance.TryPromoteAdjacentLockedVisible(tile.Position);
    }

    private void TogglePriceLabel(bool show)
    {
        if (tile.unlockPriceLabel != null)
        {
            tile.unlockPriceLabel.SetActive(show);
        }

        if (show && tile.priceText != null)
        {
            tile.priceText.text = $"${unlockCost:N0}";
        }
    }
}
