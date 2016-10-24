using UnityEngine;
using System.Collections;

public class MainMenuCamControl : MonoBehaviour
{

    public Transform currentMount;
    float moveFactor = 0.1f;
    float turnFactor = 0.03f;

    private float speedFactor;
    private float moveTime = 0.8f;
    private float turnTime = 1.5f;

    public Transform ajinkyaLt, ajinkyaRt;
    public Transform ameyaLt, ameyaRt;
    public Transform leeLt, leeRt;
    public Transform ravalLt, ravalRt;
    public Transform rohanLt, rohanRt;
    public Transform suwasLt, suwasRt;
    public Transform tannaLt, tannaRt;
    public Transform kaushalLt, kaushalRt;
    public Transform vikramLt, vikramRt;
    public Transform MenuMount;

    public GameObject canvas;

    void Start()
    {
        speedFactor = moveFactor;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, currentMount.position, speedFactor);
        transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, speedFactor);
    }

    public void setMount( Transform newMount )
    {
        currentMount = newMount;
    }

    public void playCredits()
    {
        canvas.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine("playCreditsCoroutine");
    }

    public void goToMainMenu()
    {
        speedFactor = 0.15f;
        currentMount = MenuMount;
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        StopCoroutine("playCreditsCoroutine");
    }

    public IEnumerator playCreditsCoroutine()
    {
        // ajinkya
        currentMount = ajinkyaRt;
        speedFactor = moveFactor;
        yield return new WaitForSeconds(moveTime);

        currentMount = ajinkyaLt;
        speedFactor = turnFactor;
        yield return new WaitForSeconds(turnTime);

        // ameya
        currentMount = ameyaRt;
        speedFactor = moveFactor;
        yield return new WaitForSeconds(moveTime);

        currentMount = ameyaLt;
        speedFactor = turnFactor;
        yield return new WaitForSeconds(turnTime);

        // lee
        currentMount = leeRt;
        speedFactor = moveFactor;
        yield return new WaitForSeconds(moveTime);

        currentMount = leeLt;
        speedFactor = turnFactor;
        yield return new WaitForSeconds(turnTime);

        // raval
        currentMount = ravalRt;
        speedFactor = moveFactor;
        yield return new WaitForSeconds(moveTime);

        currentMount = ravalLt;
        speedFactor = turnFactor;
        yield return new WaitForSeconds(turnTime);

        // rohan
        currentMount = rohanRt;
        speedFactor = moveFactor;
        yield return new WaitForSeconds(moveTime);

        currentMount = rohanLt;
        speedFactor = turnFactor;
        yield return new WaitForSeconds(turnTime);

        // suwas
        currentMount = suwasRt;
        speedFactor = moveFactor;
        yield return new WaitForSeconds(moveTime);

        currentMount = suwasLt;
        speedFactor = turnFactor;
        yield return new WaitForSeconds(turnTime);

        // tanna
        currentMount = tannaRt;
        speedFactor = moveFactor;
        yield return new WaitForSeconds(moveTime);

        currentMount = tannaLt;
        speedFactor = turnFactor;
        yield return new WaitForSeconds(turnTime);

        // kaushal
        currentMount = kaushalRt;
        speedFactor = moveFactor;
        yield return new WaitForSeconds(moveTime);

        currentMount = kaushalLt;
        speedFactor = turnFactor;
        yield return new WaitForSeconds(turnTime);


        // vikram
        currentMount = vikramRt;
        speedFactor = moveFactor;
        yield return new WaitForSeconds(moveTime);

        currentMount = vikramLt;
        speedFactor = turnFactor;
        yield return new WaitForSeconds(turnTime);

        speedFactor = moveFactor;
        currentMount = MenuMount;
    }
}
