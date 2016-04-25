using UnityEngine;
using System.Collections;

public class ThrowCrate : MonoBehaviour {

    public float throwPower;
    GameObject DestroyedCratePrefab;
    GameObject DestroyedCrate;
    Collider crateCollider, enemyBodyCollider, enemyHeadCollider, winTriggerCollider, detectionCollider, playerInRangeCollider;
    Rigidbody rigidBody;
    GameObject player;
    bool isCrateReleased;

    void Start ()
    {
        isCrateReleased = false;
        DestroyedCratePrefab = Resources.Load("CreateDestroyedPrefab/CrateDestroyed") as GameObject;
        crateCollider = GetComponent<Collider>();
        enemyBodyCollider = transform.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<Collider>();
        enemyHeadCollider = transform.parent.parent.parent.parent.parent.parent.GetChild(1).GetChild(0).GetComponent<Collider>();
        detectionCollider = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetChild(4).GetComponent<Collider>();
        playerInRangeCollider = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetChild(5).GetComponent<Collider>();

        player = GameObject.FindGameObjectWithTag("Player");
        winTriggerCollider = GameObject.Find("WinTrigger").GetComponent<Collider>();
        rigidBody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody.useGravity = false;

        IgnoreCollisons();
    }

    void Update()
    {
        IgnoreCollisons();
    }

    void FixedUpdate ()
    {
        if(isCrateReleased)
        {
            rigidBody.AddForce((player.transform.position - transform.position) * throwPower, ForceMode.Acceleration);
        }
    }

    public void Release()
    {
        transform.parent = null;   
        isCrateReleased = true;
        rigidBody.useGravity = true;
    }

    void OnTriggerEnter(Collider other)
    {
        DestroyedCrate = Instantiate(DestroyedCratePrefab, transform.position, transform.rotation) as GameObject;
        if(other.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealthScript>().PlayerDamage();
        }
        Destroy(gameObject);
        Destroy(DestroyedCrate, 5.0f);
    }

    void IgnoreCollisons()
    {
        Physics.IgnoreCollision(enemyBodyCollider, crateCollider);
        Physics.IgnoreCollision(enemyHeadCollider, crateCollider);
        Physics.IgnoreCollision(winTriggerCollider, crateCollider);
        Physics.IgnoreCollision(detectionCollider, crateCollider);
        Physics.IgnoreCollision(playerInRangeCollider, crateCollider);
    }
}
