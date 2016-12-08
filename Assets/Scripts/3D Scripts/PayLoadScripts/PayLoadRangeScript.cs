using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PayLoadRangeScript : MonoBehaviour
{

    private const float THETA_SCALE = 0.01f;
    private const int NUMBER_OF_CIRCLES = 4;
    private const float RATE_OF_GLITCH_INTENSITY = 0.03f;
    public static readonly byte colorScaler = 63;

    public float rateOfHealthDegeneration;
    public float rateOfHealthRegeneration;
    private GameObject player;
    private PlayerHealthScript playerHealth;
    public enum Range { CompletelyInside, OutsideFirstCircle, OutsideSecondCircle, OutsideThirdCircle, OutsideFourthCircle }
    [HideInInspector]
    public Range range;
    [HideInInspector]
    public LineRenderer circle;
    //[HideInInspector]
    //public Vector3[][] circlePoints;
    //[HideInInspector]
    //public int[] circleSize;

    //private Image screenBlurImage;
    //private Light playerLight;
    //private LightningBolt lightningBoltScript;
    //private ParticleSystem myParticleSystem;
    //[SerializeField]
    //[Range(0.0f, 1.0f)]
    //private float _blurIntensity = 0.8f;  
    //public float BlurIntensity
    //{
    //    get
    //    {
    //        return _blurIntensity;
    //    }
    //    set
    //    {
    //        _blurIntensity = value;
    //    }
    //}
    //private float _speedOfScreenBlur = 10.0f;
    //public float SpeedOfScreenBlur
    //{
    //    get
    //    {
    //        return _speedOfScreenBlur;
    //    }
    //    set
    //    {
    //        _speedOfScreenBlur = value;
    //    }
    //}

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealthScript>();
        range = Range.CompletelyInside;
        circle = transform.GetChild(4).GetComponent<LineRenderer>();      
        float radius = transform.GetChild(0).GetComponent<SphereCollider>().bounds.extents.x;
        float theta = 0f;     
        int circleSize = (int)((1f / THETA_SCALE) + 1f);
        Vector3[] circlePoints = new Vector3[circleSize];
        for (int j = 0; j < circleSize; j++)
        {
            theta += (2.0f * Mathf.PI * THETA_SCALE);
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);
            circlePoints[j] = new Vector3(x, y, 0);
        }       
        circle.SetVertexCount(circleSize);
        circle.SetPositions(circlePoints);

        //screenBlurImage = player.transform.GetChild(0).GetChild(0).FindChild("FPS UI Canvas").FindChild("ScreenBlur").GetComponent<Image>();
        //lightningBoltScript = player.GetComponent<LightningBolt>();
        //playerLight = player.transform.GetChild(1).GetComponent<Light>();
        //myParticleSystem = player.GetComponent<ParticleSystem>();
    }


    void Update()
    {     
        switch (range)
        {
            case Range.CompletelyInside:
                playerHealth.PlayerRegenerate(rateOfHealthRegeneration * Time.deltaTime);
                break;
            case Range.OutsideFirstCircle:
                playerHealth.PlayerDamage(rateOfHealthDegeneration * (int)Range.OutsideFirstCircle * Time.deltaTime, RATE_OF_GLITCH_INTENSITY);
                break;
            case Range.OutsideSecondCircle:
                playerHealth.PlayerDamage(rateOfHealthDegeneration * (int)Range.OutsideSecondCircle * Time.deltaTime, (RATE_OF_GLITCH_INTENSITY + 0.01f));
                break;
            case Range.OutsideThirdCircle:
                playerHealth.PlayerDamage(rateOfHealthDegeneration * (int)Range.OutsideThirdCircle * Time.deltaTime, (RATE_OF_GLITCH_INTENSITY + 0.02f));
                break;
            case Range.OutsideFourthCircle:
                playerHealth.PlayerDamage(rateOfHealthDegeneration * (int)Range.OutsideFourthCircle * Time.deltaTime, (RATE_OF_GLITCH_INTENSITY + 0.03f));
                break;
        }

        //if (outOfRange)
        //{
        //    playerHealth.PlayerDamage(rateOfHealthDegeneration);
        //    #region Screenblur and LightningBoltScript 
        //    //if (screenBlurImage.color.a < _blurIntensity)
        //    //{
        //    //    screenBlurImage.color += new Color(0.0f, 0.0f, 0.0f, Time.deltaTime / _speedOfScreenBlur);
        //    //}
        //    //playerLight.enabled = false;
        //    //myParticleSystem.Play();
        //    //lightningBoltScript.enabled = false;
        //    #endregion
        //}
        //else
        //{
        //    #region Screenblur and LightningBoltScript 
        //    //if (screenBlurImage.color.a > 0.0f)
        //    //{
        //    //    screenBlurImage.color = Color.clear;
        //    //}
        //    //playerLight.enabled = true;
        //    //myParticleSystem.Stop();
        //    //lightningBoltScript.enabled = true;
        //    #endregion
        //}
    }

    //void OnCollisionEnter(Collision other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    }
    //}
}
