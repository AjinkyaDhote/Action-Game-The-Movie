using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PayLoadRangeScript : MonoBehaviour {

    private GameObject player;
    private PlayerHealthScript playerHealth;
    private bool outOfRange;
    private  BoxCollider boxCollider;
    private CapsuleCollider playerCollider;
    private Image screenBlurImage;
    private Light playerLight;
    private LightningBolt lightningBoltScript;
    private ParticleSystem myParticleSystem;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _blurIntensity = 0.8f;  
    public float BlurIntensity
    {
        get
        {
            return _blurIntensity;
        }
        set
        {
            _blurIntensity = value;
        }
    }
    private float _speedOfScreenBlur = 10.0f;
    public float SpeedOfScreenBlur
    {
        get
        {
            return _speedOfScreenBlur;
        }
        set
        {
            _speedOfScreenBlur = value;
        }
    }

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealthScript>();
        outOfRange = false;
        boxCollider = GetComponent<BoxCollider>();
        playerCollider = player.GetComponent<CapsuleCollider>();
        //playerCollider = player.GetComponent<CapsuleCollider>();
        screenBlurImage = player.transform.GetChild(0).GetChild(0).FindChild("FPS UI Canvas").FindChild("ScreenBlur").GetComponent<Image>();
        lightningBoltScript = player.GetComponent<LightningBolt>();
        playerLight = player.transform.GetChild(1).GetComponent<Light>();
        myParticleSystem = player.GetComponent<ParticleSystem>();
    }
	
	
	void Update ()
    {
        //Physics.IgnoreCollision(boxCollider, playerCollider);
        if (outOfRange)
        {
            playerHealth.PlayerDamage(0.1f);
            if (screenBlurImage.color.a < _blurIntensity)
            {
                screenBlurImage.color += new Color(0.0f, 0.0f, 0.0f, Time.deltaTime / _speedOfScreenBlur);
            }
            playerLight.enabled = false;
            myParticleSystem.Play();
            lightningBoltScript.enabled = false;
        }
        else
        {
            if (screenBlurImage.color.a > 0.0f)
            {
                screenBlurImage.color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
            }
            playerLight.enabled = true;
            myParticleSystem.Stop();
            lightningBoltScript.enabled = true;
        }
	}
 
    //void OnCollisionEnter(Collision other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    }
    //}


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            outOfRange = false;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            outOfRange = true;
            //playerHealth.PlayerDamage();
        }
    }
}
