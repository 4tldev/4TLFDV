using UnityEngine;

[CreateAssetMenu(fileName = "New Watering Can", menuName = "Tools/Watering Can")]
public class WateringCan : BaseTool
{
    public override void UseTool(FarmTile targetTile)
    {
        if (targetTile != null)
        {
            targetTile.WaterTile();
            Debug.Log("Used Watering Can on tile.");
        }
    }
}
