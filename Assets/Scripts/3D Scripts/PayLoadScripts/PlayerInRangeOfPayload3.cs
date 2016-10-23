using UnityEngine;
using System.Collections;


class PlayerInRangeOfPayload3 : MonoBehaviour
{
    private PayLoadRangeScript payLoadRange;
    [Range(0, 255)]
    public byte playerCircleAlpha;
    private Color32 playerInsideColor;
    private Color32 playerOutsideColor;
    void Start()
    {
        payLoadRange = GetComponentInParent<PayLoadRangeScript>();     
        playerInsideColor = new Color32((byte)(PayLoadRangeScript.colorScaler * 4), (byte)(255 - (PayLoadRangeScript.colorScaler * 4)), 0, playerCircleAlpha);
        playerOutsideColor = new Color32(255, 0, 0, playerCircleAlpha);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //payLoadRange.circle.SetVertexCount(payLoadRange.circleSize[3]);
            //payLoadRange.circle.SetPositions(payLoadRange.circlePoints[3]);
            payLoadRange.range = PayLoadRangeScript.Range.OutsideThirdCircle;
            payLoadRange.circle.SetColors(playerInsideColor, playerInsideColor);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //payLoadRange.circle.SetVertexCount(payLoadRange.circleSize[3]);
            //payLoadRange.circle.SetPositions(payLoadRange.circlePoints[3]);
            payLoadRange.range = PayLoadRangeScript.Range.OutsideFourthCircle;
            payLoadRange.circle.SetColors(playerOutsideColor, playerOutsideColor);
            //playerHealth.PlayerDamage();
        }
    }
}

