using UnityEngine;
using System.Collections;

public class EnemyThrow : MonoBehaviour {
    private GameObject crate;
    private GameObject roar;
    private GameObject attack;
    private Animator anim;
    private throwScript throwing;
    private GameObject player;
    
    //private bool isPlayerSeen = false;
    Collider enemyBodyCollider, playerCollider, enemyHeadCollider;
    private AudioSource roarSource;
    private AudioSource attackSource;
    // Use this for initialization
    void Start () {
        crate = GameObject.FindGameObjectWithTag("Crate");
        roar = GameObject.FindGameObjectWithTag("Roar");
        attack = GameObject.FindGameObjectWithTag("Attack");
        throwing = crate.GetComponent<throwScript>();
        anim = GetComponent<Animator>();
        anim.SetBool("isPlayerInRange", false);
        anim.SetBool("throwCrate", false);
        anim.SetBool("isPlayerRunning", false);
        anim.SetBool("isPlayerSeen", false);
        anim.SetBool("hasYelled", false);
        player = GameObject.FindGameObjectWithTag("Player");
        enemyBodyCollider = transform.GetChild(3).GetChild(2).GetComponent<Collider>();
        enemyHeadCollider = transform.GetChild(3).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Collider>();
        playerCollider = player.GetComponent<Collider>();
        //transform.position += transform.forward * 0 * Time.deltaTime;
        roarSource = roar.GetComponent<AudioSource>();
        attackSource = attack.GetComponent<AudioSource>();
        roarSource.playOnAwake = false;
        attackSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(player.transform.position, transform.position) < 20.0f && (!anim.GetBool("isPlayerSeen")))
            distance();
        if (anim.GetBool("isPlayerSeen"))
        {
           Physics.IgnoreCollision(enemyBodyCollider, playerCollider);
            
           Physics.IgnoreCollision(enemyHeadCollider, playerCollider);
            
            transform.LookAt(player.transform);
          
            anim.SetBool("hasYelled", true);
            
            Debug.Log("Following player");
            if (Vector3.Distance(transform.position, player.transform.position) < 3f)
            {
                if (!attackSource.isPlaying)
                    attackSource.Play();


                Debug.Log("Caught player");

                anim.SetBool("isPlayerInRange", true);
                anim.SetBool("isPlayerRunning", false);
                transform.localRotation = Quaternion.Euler(0.0f, transform.eulerAngles.y, 0.0f);
                
                //transform.position = new Vector3(transform.position.x, 0.67f, transform.position.z);
            }
            else if (anim.GetBool("throwCrate"))
            
            {
                if (!anim.GetBool("isPlayerDead"))
                {
                    transform.position = new Vector3(transform.position.x, 0.67f, transform.position.z);
                    transform.position += transform.forward * 6 * Time.deltaTime;
                }
                

                if (Vector3.Distance(player.transform.position, transform.position) > 15.0f)
                {
                   
                    anim.SetBool("isPlayerRunning", true);
                    anim.SetBool("isPlayerInRange", false);
                    Debug.Log("Player running away from me");
                }
            }

        }
    }

    void thrower()
        {
        
        throwing.release(); 
        anim.SetBool("throwCrate", true);
        //transform.position += transform.forward * 3 * Time.deltaTime;
    }

    void DamagePlayer()
    {
        player.GetComponent<PlayerHealthScript>().PlayerDamage();
    }

    void distance()
    {

      
        {

            transform.LookAt(player.transform);
            //Debug.Log((Vector3.Distance(player.transform.position, transform.position)));
           anim.SetBool("isPlayerSeen", true);
            //isPlayerSeen = true;
            if (!roarSource.isPlaying)
                roarSource.Play();
            anim.SetBool("isPlayerInRange", true);
         
            
        }
    }

   
}
