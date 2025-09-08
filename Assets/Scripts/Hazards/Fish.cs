using UnityEngine;

public class Fish : MonoBehaviour
{
    enum DirectionEnum
    {
        Left, Right
    }
    Fish(DirectionEnum direction)
    {
        m_direction = direction;
    }

    public float speed;
    private DirectionEnum m_direction;

    // Update is called once per frame
    void Update()
    {

        float fDirection = 1f;
        if (m_direction == DirectionEnum.Left) fDirection *= -1f;

        transform.Translate(0, fDirection * speed * Time.deltaTime, 0);
    }
}
