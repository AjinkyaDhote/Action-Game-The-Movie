using UnityEngine;
using System.Collections;


class PlayerInRangeOfPayload : MonoBehaviour
{
    private PayLoadRangeScript payLoadRange;
    [Range(0, 255)]
    public byte playerCircleAlpha;
    private Color32 playerInsideColor;
    private Color32 playerOutsideColor;
    void Start()
    {
        payLoadRange = GetComponentInParent<PayLoadRangeScript>();
        playerInsideColor = new Color32(0, 255, 0, playerCircleAlpha);
        playerOutsideColor = new Color32(255, 0, 0, playerCircleAlpha);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            payLoadRange.outOfRange = false;
            payLoadRange.circle.SetColors(playerInsideColor, playerInsideColor);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            payLoadRange.outOfRange = true;
            payLoadRange.circle.SetColors(playerOutsideColor, playerOutsideColor);
            //playerHealth.PlayerDamage();
        }
    }
}

