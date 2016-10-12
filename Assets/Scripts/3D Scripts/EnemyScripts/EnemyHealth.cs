using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    //private Animator anim;
    private bool _isKilled = false;
    public bool IsKilled
    {
        get
        {
            return _isKilled;
        }
    }
    private bool isPlayerDead;
    //private NavMeshAgent agent;
    private Material deadMaterial;
    private AudioClip ZombieDeath;
    private MeshRenderer meshRenderer;

    public int currentHealth;
    AI_movement aiMovementScript;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        deadMaterial = Resources.Load("Materials/deadMaterial") as Material;
        ZombieDeath = Resources.Load("Sounds/ZombieDeath") as AudioClip;
        aiMovementScript = transform.GetComponentInParent<AI_movement>();
        //agent = GetComponent<NavMeshAgent>();        
        if (transform.CompareTag("SmallEnemy"))
            currentHealth = 5;
        else
            currentHealth = 60;
        //anim = transform.parent.parent.GetComponent<Animator> ();
        isPlayerDead = false;
        _isKilled = false;
        //GameManager.Instance.totalEnemiesKilled = 0;
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("damage");
        if (aiMovementScript != null)
        {
            aiMovementScript.Detection();
        }
        if (currentHealth <= 0)
        {
            Defeated();
            meshRenderer.material.color = Color.red;
        }

    }

    void Defeated()
    {
        if (!_isKilled)
        {
            //Debug.Log("Killed");
            _isKilled = true;
            GameManager.Instance.totalEnemiesKilled++;
            isPlayerDead = true;
            //gameObject.GetComponent<Renderer>().material.SetColor("spec", colorDead);
            Destroy(gameObject);
        }
        //AudioSource.PlayClipAtPoint(ZombieDeath, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        isPlayerDead = true;
        Destroy(gameObject);
    }
}
