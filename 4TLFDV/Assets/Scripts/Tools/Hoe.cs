using UnityEngine;

[CreateAssetMenu(fileName = "New Hoe", menuName = "Tools/Hoe")]
public class Hoe : BaseTool
{
    public override void UseTool(FarmTile targetTile)
    {
        if (targetTile != null)
        {
            targetTile.TillTile();
            Debug.Log("Used Hoe on tile.");
        }
    }
}
