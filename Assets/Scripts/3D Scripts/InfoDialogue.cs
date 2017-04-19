using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class InfoDialogue : MonoBehaviour
{
    public GameObject camera;
    public GameObject pauseMenuGO;
    private PauseMenu pauseMenu;
    private UnityStandardAssets.ImageEffects.DepthOfField dof;
    private Text infoBox;
    private Image oneImage;
    private Image twoImage1;
    private Image twoImage2;

    void Awake()
    {
        dof = camera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>();
        depthOfField(false);
        infoBox = transform.FindChild("InfoText").GetComponent<Text>();
        infoBox.gameObject.SetActive(false);
        pauseMenu = pauseMenuGO.GetComponent<PauseMenu>();

        oneImage = transform.FindChild("oneImage").GetComponent<Image>();
        oneImage.transform.gameObject.SetActive(false);
        oneImage.sprite = null;

        twoImage1 = transform.FindChild("twoImage1").GetComponent<Image>();
        twoImage1.transform.gameObject.SetActive(false);
        twoImage1.sprite = null;

        twoImage2 = transform.FindChild("twoImage2").GetComponent<Image>();
        twoImage2.transform.gameObject.SetActive(false);
        twoImage2.sprite = null;
    }

    public void playInfo(string infoString)
    {
        infoBox.gameObject.SetActive(true);
        infoString += "\n\nPress Q to continue";
        infoBox.text = infoString;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.infoDialogue = true;
        //Cursor.visible = true;
        depthOfField(true);
    }

    public void playInfoOneImage(string infoString, Sprite i_sprite)
    {
        infoBox.gameObject.SetActive(true);
        infoString += "\n\nPress Q to continue";
        infoBox.text = infoString;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.infoDialogue = true;
        //Cursor.visible = true;
        depthOfField(true);

        oneImage.transform.gameObject.SetActive(true);
        oneImage.sprite = i_sprite;
    }

    public void playInfoTwoImage(string infoString, Sprite i_sprite1, Sprite i_sprite2)
    {
        infoBox.gameObject.SetActive(true);
        infoString += "\n\nPress Q to continue";
        infoBox.text = infoString;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.infoDialogue = true;
        //Cursor.visible = true;
        depthOfField(true);

        twoImage1.transform.gameObject.SetActive(true);
        twoImage1.sprite = i_sprite1;

        twoImage2.transform.gameObject.SetActive(true);
        twoImage2.sprite = i_sprite2;
    }

    void Update()
    {
        if (!pauseMenu.isPaused && Input.GetKeyDown(KeyCode.Q))
        {
            infoBox.gameObject.SetActive(false);
            infoBox.text = "";
            depthOfField(false);
            Cursor.lockState = CursorLockMode.Locked;
            GameManager.Instance.infoDialogue = false;
            //Cursor.visible = false;
            Time.timeScale = 1;

            oneImage.transform.gameObject.SetActive(false);
            oneImage.sprite = null;

            twoImage1.transform.gameObject.SetActive(false);
            twoImage1.sprite = null;

            twoImage2.transform.gameObject.SetActive(false);
            twoImage2.sprite = null;
        }
    }

    private void depthOfField(bool enable)
    {
        if (enable)
        {
            dof.focalLength = 0.01f;
            dof.focalSize = 0.0f;
            dof.aperture = 1.0f;
        }
        else
        {
            dof.focalLength = 10.0f;
            dof.focalSize = 0.05f;
            dof.aperture = 0.5f;
        }
    }
}
