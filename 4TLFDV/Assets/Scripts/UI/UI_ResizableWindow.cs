using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class UI_ResizableWindow : MonoBehaviour, IDragHandler
{
    [Header("Resize Settings")]
    [SerializeField] private float minWidthPercentage = 0.2f;
    [SerializeField] private float maxWidthPercentage = 0.6f;
    [SerializeField] private float minHeightPercentage = 0.3f;
    [SerializeField] private float maxHeightPercentage = 0.8f;

    private RectTransform rectTransform;
    private Canvas parentCanvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentCanvas = GetComponentInParent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (parentCanvas == null) return;

        float canvasWidth = parentCanvas.pixelRect.width;
        float canvasHeight = parentCanvas.pixelRect.height;

        float minWidth = canvasWidth * minWidthPercentage;
        float maxWidth = canvasWidth * maxWidthPercentage;
        float minHeight = canvasHeight * minHeightPercentage;
        float maxHeight = canvasHeight * maxHeightPercentage;

        float targetWidth = Mathf.Clamp(rectTransform.rect.width + eventData.delta.x, minWidth, maxWidth);
        float targetHeight = Mathf.Clamp(rectTransform.rect.height - eventData.delta.y, minHeight, maxHeight);

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetHeight);
    }
}
