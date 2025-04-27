using UnityEngine;
using UnityEngine.UI;

public static class UIHelpers
{
    public static void ForceRefreshLayout(GameObject layoutObject)
    {
        if (layoutObject.TryGetComponent(out RectTransform rectTransform))
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }
    }
}
