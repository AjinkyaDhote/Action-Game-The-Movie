using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class IntializeEffects : MonoBehaviour
{

    BloomOptimized bloomOptimized;
    Antialiasing antialiasing;
    ScreenSpaceAmbientOcclusion screenSpace;
    VignetteAndChromaticAberration vignette;
    ColorCorrectionCurves colorCorrection;

    void Start()
    {
       bloomOptimized = gameObject.AddComponent<BloomOptimized>();
       bloomOptimized.fastBloomShader = Resources.Load<Shader>("ImportEffects/MobileBloom 1");

       gameObject.AddComponent<Tonemapping>();
       screenSpace =  gameObject.AddComponent<ScreenSpaceAmbientOcclusion>();
       screenSpace.m_SSAOShader = Resources.Load<Shader>("ImportEffects/SSAOShader 1");
       screenSpace.m_RandomTexture = Resources.Load<Texture2D>("ImportEffects/RandomVectors 1");
       vignette = gameObject.AddComponent<VignetteAndChromaticAberration>();       
       colorCorrection = gameObject.AddComponent<ColorCorrectionCurves>();
       //antialiasing = gameObject.AddComponent<Antialiasing>();                                                       
    }	    
}
