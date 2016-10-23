﻿using UnityEngine;
using System.Collections;

public class BulletDamage : MonoBehaviour
{
    AI_movement aiMovementScript;
    EnemyHealth enemyHealthScript;
    WeaponSystem weaponSystemScript;
    Transform playerTransform;
    PlayerShooting playerShootingScript;
    const int HEAD_SHOT_DAMAGE = 1000;
    private float timeToDestroyBullet;
    private bool _isFired = false;
    private ParticleSystem enemyHitParticleEffect;
    //--------------------------------Friendly Fire ON--------------------------------------------------------
    //PayLoadHealthScript payLoadHealthScript;
    //--------------------------------------------------------------------------------------------------------
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        playerShootingScript = player.GetComponent<Transform>().GetChild(0).GetChild(0).GetComponent<PlayerShooting>();
        //--------------------------------Friendly Fire ON--------------------------------------------------------
        //payLoadHealthScript = GameObject.FindGameObjectWithTag("NewPayload").GetComponent<Transform>().GetChild(2).GetComponent<PayLoadHealthScript>();
        //---------------------------------------------------------------------------------------------------------
    }
    private void Update()
    {
        if (_isFired)
        {
            if (Time.time > timeToDestroyBullet)
            {
                Destroy(gameObject);
            }

        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("HeadCollider"))
        {
            aiMovementScript = other.transform.GetComponentInParent<AI_movement>();
            if (!aiMovementScript.IsPlayerPayloadSeen)
            {
                aiMovementScript.Detection(playerTransform, false);
            }
            aiMovementScript.isChasingPayload = false;
            enemyHealthScript = other.transform.GetComponentInParent<EnemyHealth>();
            if ((enemyHealthScript != null) && !enemyHealthScript.IsKilled)
            {
                PlayEnemyHitParticle(other.contacts[0].point, other.contacts[0].normal);
                enemyHealthScript.Damage(HEAD_SHOT_DAMAGE);
            }
            GameManager.Instance.headShots++;
            Destroy(gameObject);
        }
        else if (other.collider.CompareTag("BodyCollider"))
        {
            aiMovementScript = other.transform.GetComponentInParent<AI_movement>();
            if (!aiMovementScript.IsPlayerPayloadSeen)
            {
                aiMovementScript.Detection(playerTransform);
            }
            aiMovementScript.isChasingPayload = false;
            enemyHealthScript = other.transform.GetComponentInParent<EnemyHealth>();
            if ((enemyHealthScript != null) && !enemyHealthScript.IsKilled)
            {
                PlayEnemyHitParticle(other.contacts[0].point, other.contacts[0].normal);
                enemyHealthScript.Damage(1);
            }
            Destroy(gameObject);
        }
        //--------------------------------Friendly Fire ON--------------------------------------------------------
        //else if (other.collider.CompareTag("NewPayload"))
        //{
        //    payLoadHealthScript.PayLoadDamage();
        //    Destroy(gameObject);
        //}
        //---------------------------------------------------------------------------------------------------------

        else
        {
            playerShootingScript.PlayWallHitPreFab(other.contacts[0].point, other.contacts[0].normal);
            Destroy(gameObject);
        }
    }

    private void PlayEnemyHitParticle(Vector3 hitPoint, Vector3 hitNormal)
    {
        enemyHitParticleEffect = enemyHealthScript.gameObject.transform.FindChild("EnemyHitParticleEffect").GetComponent<ParticleSystem>();
        enemyHitParticleEffect.transform.position = hitPoint;
        enemyHitParticleEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
        enemyHitParticleEffect.Play();
    }
}
