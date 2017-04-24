using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
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
        //_glitchMaterial = Resources.Load<Material>("Materials/ScreenGlitch");
        glitchShader = Resources.Load<Shader>("Shaders/ScreenGlitch");
        //_glitchMaterial.shader = glitchShader;
        //_glitchMaterial.mainTexture = null;
        if(glitchShader)
        {
            _glitchMaterial = CreateMaterial(glitchShader);
            _glitchMaterial.shader = glitchShader;
            _glitchMaterial.mainTexture = null;
        }
    }
    private static Material CreateMaterial(Shader shader)
    {
        if (!shader)
            return null;
        Material m = new Material(shader);
        m.hideFlags = HideFlags.HideAndDontSave;
        return m;
    }
    private static void DestroyMaterial(Material mat)
    {
        if (mat)
        {
            DestroyImmediate(mat);
            mat = null;
        }
    }
    void OnDisable()
    {
        DestroyMaterial(_glitchMaterial);
    }
    void OnRenderImage ( RenderTexture src, RenderTexture dst )
    {
        if (_glitchMaterial != null)
        {
            Graphics.Blit(src, dst, _glitchMaterial);
        }      
    }
}
