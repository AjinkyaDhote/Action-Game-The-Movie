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

    int delayTime;
    private int currentHealth;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        deadMaterial = Resources.Load("Materials/deadMaterial") as Material;
        ZombieDeath = Resources.Load("Sounds/ZombieDeath") as AudioClip;
        delayTime = 0;
        //agent = GetComponent<NavMeshAgent>();        
        if (transform.CompareTag("SmallEnemy"))
            currentHealth = 10;
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
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Defeated();
            meshRenderer.material.color = Color.red;       
            //foreach (Transform child in transform.parent.transform.parent)
            //{
            //    if (child.tag == "SmallEnemy")
            //    {
            //        child.GetComponent<Renderer>().material.color = colorDead;
            //    }
            //}
        }

    }

    void Defeated()
    {
        if (!_isKilled)
        {
            Debug.Log("Killed");
            _isKilled = true;
            GameManager.Instance.totalEnemiesKilled++;
            isPlayerDead = true;
            //gameObject.GetComponent<Renderer>().material.SetColor("spec", colorDead);
            Destroy(gameObject, delayTime);
        }
        AudioSource.PlayClipAtPoint(ZombieDeath, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        isPlayerDead = true;
        Destroy(gameObject, delayTime);
    }
}
