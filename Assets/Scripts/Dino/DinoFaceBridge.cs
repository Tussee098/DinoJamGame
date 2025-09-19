using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DinoFaceBridge : MonoBehaviour
{
    [SerializeField]
    private Material m_mat;

    public enum FaceEnum
    {
        Normal = 0,
        Hurt = 1,
        Dazed = 2,
        Happy = 3,
    }

    public void SetFace(FaceEnum face)
    {
        float offset = 0f;
        Debug.Log(face);
        switch (face)
        {
            case FaceEnum.Normal:
                offset = 0;
                break;
            case FaceEnum.Hurt:
                offset = -0.23f;
                break;
            case FaceEnum.Dazed:
                offset = -0.46f;
                break;
            case FaceEnum.Happy:
                offset = -0.69f;
                break;
            default:
                break;

        }
        SetMaterialOffset(offset);
    }

    private void SetMaterialOffset(float offset)
    {
        m_mat.SetTextureOffset("_BaseMap", new Vector2(0f, offset));
    }

}
