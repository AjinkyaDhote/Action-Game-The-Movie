using UnityEngine;
using System.Collections;


class PlayerInRangeOfPayload0 : MonoBehaviour
{
    private PayLoadRangeScript payLoadRange;
    [Range(0, 255)]
    public byte playerCircleAlpha;
    private Color32 playerInsideColor;
    private Color32 playerOutsideColor;
    private Animator animPayload;
    void Start()
    {
        payLoadRange = GetComponentInParent<PayLoadRangeScript>();
        playerInsideColor = new Color32(0, 255, 0, playerCircleAlpha);
        playerOutsideColor = new Color32(PayLoadRangeScript.colorScaler, (byte)(255 - PayLoadRangeScript.colorScaler), 0, playerCircleAlpha);
        animPayload = transform.parent.GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //payLoadRange.circle.SetVertexCount(payLoadRange.circleSize[0]);
            //payLoadRange.circle.SetPositions(payLoadRange.circlePoints[0]);
            payLoadRange.range = PayLoadRangeScript.Range.CompletelyInside;
            payLoadRange.circle.SetColors(playerInsideColor, playerInsideColor);
            animPayload.SetBool("IsPlayerOutOfCircle", false);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //payLoadRange.circle.SetVertexCount(payLoadRange.circleSize[1]);
            //payLoadRange.circle.SetPositions(payLoadRange.circlePoints[1]);
            payLoadRange.range = PayLoadRangeScript.Range.OutsideFirstCircle;
            payLoadRange.circle.SetColors(playerOutsideColor, playerOutsideColor);
            //playerHealth.PlayerDamage();
            animPayload.SetBool("IsPlayerOutOfCircle", true);
        }
    }
}

