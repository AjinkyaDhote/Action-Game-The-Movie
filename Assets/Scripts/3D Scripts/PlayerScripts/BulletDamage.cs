using UnityEngine;
using System.Collections;

public class BulletDamage : MonoBehaviour
{
    AI_movement aiMovementScript;
    EnemyHealth enemyHealthScript;
    WeaponSystem weaponSystemScript;
    const int HEAD_SHOT_DAMAGE = 1000;
    private float timeToDestroyBullet;
    private bool _isFired = false;
    public bool IsFired
    {
        get
        {
            return _isFired;
        }
        set
        {
            weaponSystemScript = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(0).GetComponent<WeaponSystem>();
            timeToDestroyBullet = Time.time + weaponSystemScript.currentWeaponInfo.bulletLifeTime;
            _isFired = value;
        }
    }
  
    private void Start()
    {
     
    }
    private void Update()
    {
        if(_isFired)
        {
            if (Time.time > timeToDestroyBullet)
            {
                Destroy(gameObject);
            }
                
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("HeadCollider") && other.CompareTag("BodyCollider"))
        {
            aiMovementScript = other.transform.GetComponent<AI_movement>(); ;
            if (!aiMovementScript.IsPlayerSeen)
            {
                aiMovementScript.Detection();
            
            }
        }
        if (other.CompareTag("HeadCollider"))
        {
            enemyHealthScript = other.transform.GetComponentInParent<EnemyHealth>();
            if ((enemyHealthScript != null) && !enemyHealthScript.IsKilled)
            {
                Debug.Log("hiiii");
                enemyHealthScript.Damage(HEAD_SHOT_DAMAGE);
            }
            GameManager.Instance.headShots++;
            //Destroy(gameObject);
        }
        else if(other.CompareTag("BodyCollider"))
        {
            enemyHealthScript = other.transform.GetComponentInParent<EnemyHealth>();
            if ((enemyHealthScript != null) && !enemyHealthScript.IsKilled)
            {
                Debug.Log("hiiii");

                enemyHealthScript.Damage(1);
            }
           // Destroy(gameObject);
        }
        else if (other.CompareTag("Wall") || other.CompareTag("PayLoad"))
        {
            Destroy(gameObject);
        }      
    }
}
