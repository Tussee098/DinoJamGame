using UnityEngine;

public class DinoModelRotation : MonoBehaviour
{
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (h > 0)  transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        if (h < 0) transform.rotation = Quaternion.Euler(0f, -90f, 0f);
    }
}
