using UnityEngine;
using System.Collections;

public class BulletDamage : MonoBehaviour
{
    AI_movement aiMovementScript;
    EnemyHealth enemyHealthScript;
    WeaponSystem weaponSystemScript;
    const int HEAD_SHOT_DAMAGE = 1000;
    void Start()
    {
        weaponSystemScript = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetChild(0).GetComponent<WeaponSystem>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SmallEnemy"))
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
                enemyHealthScript.Damage(HEAD_SHOT_DAMAGE);
            }
            GameManager.Instance.headShots++;
            gameObject.SetActive(false);
        }
        else if(other.CompareTag("BodyCollider"))
        {
            enemyHealthScript = other.transform.GetComponentInParent<EnemyHealth>();
            if ((enemyHealthScript != null) && !enemyHealthScript.IsKilled)
            {
                enemyHealthScript.Damage(weaponSystemScript.currentWeaponInfo.damageDealt);
            }
            gameObject.SetActive(false);
        }
    }
}
