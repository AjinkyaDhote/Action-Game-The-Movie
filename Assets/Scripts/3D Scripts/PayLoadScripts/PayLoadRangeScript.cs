using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PayLoadRangeScript : MonoBehaviour
{

    private const float THETA_SCALE = 0.01f;

    private GameObject player;
    private PlayerHealthScript playerHealth;
    [HideInInspector]
    public bool outOfRange;
    private BoxCollider boxCollider;
    private CapsuleCollider playerCollider;

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
        outOfRange = false;
        boxCollider = GetComponent<BoxCollider>();
        playerCollider = player.GetComponent<CapsuleCollider>();

        //To make the circle of life
        {
            LineRenderer circle = transform.GetChild(1).GetComponent<LineRenderer>();
            float radius = transform.GetChild(0).GetComponent<SphereCollider>().bounds.extents.x;
            float theta = 0f;
            int size = (int)((1f / THETA_SCALE) + 1f);
            circle.SetVertexCount(size);
            for (int i = 0; i < size; i++)
            {
                theta += (2.0f * Mathf.PI * THETA_SCALE);
                float x = radius * Mathf.Cos(theta);
                float y = radius * Mathf.Sin(theta);
                circle.SetPosition(i, new Vector3(x, y, 0));
            }
        }

        //screenBlurImage = player.transform.GetChild(0).GetChild(0).FindChild("FPS UI Canvas").FindChild("ScreenBlur").GetComponent<Image>();
        //lightningBoltScript = player.GetComponent<LightningBolt>();
        //playerLight = player.transform.GetChild(1).GetComponent<Light>();
        //myParticleSystem = player.GetComponent<ParticleSystem>();
    }


    void Update()
    {
        //Physics.IgnoreCollision(boxCollider, playerCollider);

        if (outOfRange)
        {
            playerHealth.PlayerDamage(0.01f);
            //if (screenBlurImage.color.a < _blurIntensity)
            //{
            //    screenBlurImage.color += new Color(0.0f, 0.0f, 0.0f, Time.deltaTime / _speedOfScreenBlur);
            //}
            //playerLight.enabled = false;
            //myParticleSystem.Play();
            //lightningBoltScript.enabled = false;
        }
        else
        {
            //if (screenBlurImage.color.a > 0.0f)
            //{
            //    screenBlurImage.color = Color.clear;
            //}
            //playerLight.enabled = true;
            //myParticleSystem.Stop();
            //lightningBoltScript.enabled = true;
        }
    }

    //void OnCollisionEnter(Collision other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    }
    //}
}
