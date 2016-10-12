using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScreenGlitch : MonoBehaviour
{
    public Material glitchMaterial;

    void OnRenderImage ( RenderTexture src, RenderTexture dst )
    {
        Graphics.Blit( src, dst, glitchMaterial );
    }
}
