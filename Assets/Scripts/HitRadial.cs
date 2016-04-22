using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HitRadial : MonoBehaviour {

    public Image HitImage;
    private GameObject enemy;
    private Image HitRadialImage;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void createNewRadial(GameObject AttackingEnemy)
    {
        enemy = AttackingEnemy;
        HitRadialImage = (Image)Instantiate(HitImage, new Vector3(0, 0, 0), Quaternion.identity);
        HitRadialImage.transform.parent = transform.FindChild("Main Camera").transform.FindChild("Gun Camera").transform.FindChild("FPS UI Canvas");
        Destroy(HitRadialImage.gameObject, 2.0f);
        HitRadialImage.GetComponent<HitRadialPrefab>().startRotation(enemy, HitRadialImage);

    }
}
