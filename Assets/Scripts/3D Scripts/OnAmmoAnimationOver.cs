using UnityEngine;
using System.Collections;

public class OnAmmoAnimationOver : MonoBehaviour
{
    public void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
