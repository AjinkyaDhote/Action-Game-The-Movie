using UnityEngine;
using System.Collections;


class PlayerInRangeOfPayload1 : MonoBehaviour
{
    private PayLoadRangeScript payLoadRange;
    [Range(0, 255)]
    public byte playerCircleAlpha;
    private Color32 playerInsideColor;
    private Color32 playerOutsideColor;
    void Start()
    {
        payLoadRange = GetComponentInParent<PayLoadRangeScript>();
        playerInsideColor = new Color32(PayLoadRangeScript.colorScaler, (byte)(255 - PayLoadRangeScript.colorScaler), 0, playerCircleAlpha);
        playerOutsideColor = new Color32((byte)(PayLoadRangeScript.colorScaler * 2), (byte)(255 - (PayLoadRangeScript.colorScaler * 2)), 0, playerCircleAlpha);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //payLoadRange.circle.SetVertexCount(payLoadRange.circleSize[1]);
            //payLoadRange.circle.SetPositions(payLoadRange.circlePoints[1]);
            payLoadRange.range = PayLoadRangeScript.Range.OutsideFirstCircle;
            //payLoadRange.circle.SetColors(playerInsideColor, playerInsideColor);
            payLoadRange.circle.startColor = payLoadRange.circle.endColor = playerInsideColor;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //payLoadRange.circle.SetVertexCount(payLoadRange.circleSize[2]);
            //payLoadRange.circle.SetPositions(payLoadRange.circlePoints[2]);
            payLoadRange.range = PayLoadRangeScript.Range.OutsideSecondCircle;
            //payLoadRange.circle.SetColors(playerOutsideColor, playerOutsideColor);
            payLoadRange.circle.startColor = payLoadRange.circle.endColor = playerOutsideColor;
            //playerHealth.PlayerDamage();
        }
    }
}

