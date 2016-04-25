using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HitRadial : MonoBehaviour
{
    Transform enemyTransform;
    Image hitRadialImage;
    void Start()
    {
        hitRadialImage = GetComponent<Image>();
    }

    void Update()
    {
        if (enemyTransform)
        {
            StartRotation(enemyTransform);
        }      
    }
    public void StartRotation(Transform enemyArgument)
    {
        enemyTransform = enemyArgument;
        float angleBetween = Vector3.Angle(enemyTransform.forward, transform.parent.parent.parent.parent.forward);
        Vector3 cross = Vector3.Cross(enemyTransform.forward,transform.parent.parent.parent.parent.forward);
        if (cross.y < 0)
        {
            angleBetween = -angleBetween;
        }
        if(hitRadialImage)
        {
            hitRadialImage.rectTransform.anchoredPosition3D = new Vector3(300 * Mathf.Sin((angleBetween * Mathf.PI) / 180), -300 * Mathf.Cos((angleBetween * Mathf.PI) / 180), 0);
            //Debug.Log(" angle of hit : " + angleBetween);
            hitRadialImage.rectTransform.localScale = new Vector3(4, 4, 4);
            hitRadialImage.rectTransform.localRotation = Quaternion.Euler(0, 0, angleBetween - 180);
        }       
    }
}
