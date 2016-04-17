// BulletScript.cs
using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	Rigidbody bullet;
	//Transform Enemy;
	bool isCorrectEnemyDetected;

	float bulletSpeed;
	void Start()
	{
		bulletSpeed = 20f;
		isCorrectEnemyDetected = false;
		bullet = GetComponent<Rigidbody> ();
	
	}

	void Update () {
		if (isCorrectEnemyDetected) {

			//bullet.MovePosition (transform.position +  (Enemy.position- transform.position).normalized * bulletSpeed * Time.deltaTime);
			//bullet.velocity = transform.up * bulletSpeed;
			bullet.AddForce(transform.up * bulletSpeed);
		}
	}

	/*void OnBecameInvisible () {
		this.gameObject.SetActive(false);
	}*/
	void OnCollisionEnter(Collision other)
	{
		if (other.collider.tag == "Enemy") 
		{
			Debug.Log ("Bullet Hit Enemy");
		}

	}

	public void GetCorrectEnemy(Transform enemyPosition)
	{
		//Enemy = enemyPosition;
		isCorrectEnemyDetected = true;
	}

}
