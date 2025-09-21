using UnityEngine;
using UnityEngine.UI;

public class GameUIBubble : MonoBehaviour
{
    [Range(0f, 1f)] public float value = 0f; // 0 = transparent, 1 = fully blue
    public Image blueOverlay;

    void Update()
    {


        Color c = blueOverlay.color;
        c.a = Mathf.Clamp01(value); // adjust only alpha
        blueOverlay.color = c;

    }
}
