using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HitRadialPrefab : MonoBehaviour
{

    private GameObject enemy;
    bool enemyTrue;
    float angleBetween;
    Image hitRadialImage;

    // Use this for initialization
    void Start()
    {
        enemyTrue = false;
    }

    // Update is called once per frame
    void Update()
    {
      
            startRotation(enemy, hitRadialImage);
            //Debug.Log("aaaa");

    }
    public void startRotation(GameObject enemyArgument, Image self)
    {
        enemy = enemyArgument;
        enemyTrue = true;
        hitRadialImage = self;
        angleBetween = Vector3.Angle(enemy.transform.forward, this.transform.parent.transform.parent.transform.parent.forward);
        Vector3 cross = Vector3.Cross(enemy.transform.forward, this.transform.parent.transform.parent.transform.parent.forward);
        if (cross.y < 0) angleBetween = -angleBetween;

        hitRadialImage.rectTransform.anchoredPosition3D = new Vector3(300 * Mathf.Sin((angleBetween * Mathf.PI) / 180), -300 * Mathf.Cos((angleBetween * Mathf.PI) / 180), 0);
        //Debug.Log(" angle of hit : " + angleBetween);
        hitRadialImage.rectTransform.localScale = new Vector3(4, 4, 4);
        hitRadialImage.rectTransform.localRotation = Quaternion.Euler(0, 0, angleBetween - 180);
        hitRadialImage.fillAmount = 1;


    }
}
