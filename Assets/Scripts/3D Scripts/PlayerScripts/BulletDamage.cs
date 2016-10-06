using UnityEngine;
using System.Collections;

public class BulletDamage : MonoBehaviour
{
    AI_movement aiMovementScript;
    EnemyHealth enemyHealthScript;
    WeaponSystem weaponSystemScript;
    const int HEAD_SHOT_DAMAGE = 1000;
    Collider bulletCollider;
    Collider payloadCollider;
    Collider payloadColliderBox;
    void Start()
    {
        weaponSystemScript = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(0).GetComponent<WeaponSystem>();
        bulletCollider = GetComponent<Collider>();
        payloadCollider = GameObject.FindGameObjectWithTag("PayLoad").GetComponent<SphereCollider>();
        payloadColliderBox = GameObject.FindGameObjectWithTag("PayLoad").GetComponent<BoxCollider>();        
    }

    void Update()
    {
        Physics.IgnoreCollision(bulletCollider, payloadCollider);
        Physics.IgnoreCollision(bulletCollider, payloadColliderBox);        
    }


    void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag("HeadCollider") && other.CompareTag("BodyCollider"))
        {
            aiMovementScript = other.transform.GetComponent<AI_movement>(); ;
            if (!aiMovementScript.IsPlayerSeen)
            {
                aiMovementScript.Detection();
                int a = 0;
            }
        }*/
        if (other.CompareTag("HeadCollider"))
        {
            enemyHealthScript = other.transform.GetComponentInParent<EnemyHealth>();
            if ((enemyHealthScript != null) && !enemyHealthScript.IsKilled)
            {
                enemyHealthScript.Damage(HEAD_SHOT_DAMAGE);
            }
            GameManager.Instance.headShots++;
            Destroy(gameObject);
        }
        else if(other.CompareTag("BodyCollider"))
        {
            enemyHealthScript = other.transform.GetComponentInParent<EnemyHealth>();
            if ((enemyHealthScript != null) && !enemyHealthScript.IsKilled)
            {
                enemyHealthScript.Damage(weaponSystemScript.currentWeaponInfo.damageDealt);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        //Debug.Log("Bullet Collided with" + other.gameObject.name);
    }
}
