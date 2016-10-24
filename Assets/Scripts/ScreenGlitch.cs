using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScreenGlitch : MonoBehaviour
{
    private Material _glitchMaterial;
    public Material glitchMaterial
    {
        get
        {
            return _glitchMaterial;
        }
    }
    private Shader glitchShader;
    void Awake()
    {
        _glitchMaterial = Resources.Load<Material>("Materials/ScreenGlitch");
        glitchShader = Resources.Load<Shader>("Shaders/ScreenGlitch");
        _glitchMaterial.shader = glitchShader;
        _glitchMaterial.mainTexture = null;
    }
    void OnRenderImage ( RenderTexture src, RenderTexture dst )
    {
        if (_glitchMaterial != null)
        {
            Graphics.Blit(src, dst, _glitchMaterial);
        }      
    }
}
