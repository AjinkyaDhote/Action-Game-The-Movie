using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class InfoDialogue : MonoBehaviour
{
    public GameObject camera;
    public GameObject pauseMenuGO;
    private PauseMenu pauseMenu;
    private UnityStandardAssets.ImageEffects.DepthOfField dof;
    private Text infoBox1;
    private Text infoBox2;
    private Text infoBox3;
    private Text title1;
    private Text title2;
    private Text title3;
    private Image background;
    private Image oneImage;
    private Image twoImage1;
    private Image twoImage2;

    void Awake()
    {
        dof = camera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>();
        depthOfField(false);

        infoBox1 = transform.FindChild("InfoText1").GetComponent<Text>();
        infoBox1.gameObject.SetActive(false);
        infoBox2 = transform.FindChild("InfoText2").GetComponent<Text>();
        infoBox2.gameObject.SetActive(false);
        infoBox3 = transform.FindChild("InfoText3").GetComponent<Text>();
        infoBox3.gameObject.SetActive(false);

        title1 = transform.FindChild("title1").GetComponent<Text>();
        title1.gameObject.SetActive(false);
        title2 = transform.FindChild("title2").GetComponent<Text>();
        title2.gameObject.SetActive(false);
        title3 = transform.FindChild("title3").GetComponent<Text>();
        title3.gameObject.SetActive(false);

        pauseMenu = pauseMenuGO.GetComponent<PauseMenu>();

        background = transform.FindChild("Background").GetComponent<Image>();
        background.gameObject.SetActive(false);

        oneImage = transform.FindChild("oneImage").GetComponent<Image>();
        oneImage.transform.gameObject.SetActive(false);
        oneImage.sprite = null;

        twoImage1 = transform.FindChild("twoImage1").GetComponent<Image>();
        twoImage1.transform.gameObject.SetActive(false);
        twoImage1.sprite = null;

        twoImage2 = transform.FindChild("twoImage2").GetComponent<Image>();
        twoImage2.transform.gameObject.SetActive(false);
        twoImage2.sprite = null;

        GameManager.Instance.infoDialogue = false;
    }

    public void playInfo(string infoString)
    {
        infoBox1.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        infoString += "\n\nPress Q to continue";
        infoBox1.text = infoString;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.infoDialogue = true;
        //Cursor.visible = true;
        depthOfField(true);
    }

    public void playInfoOneImage(string infoString, Sprite i_sprite, string titleText)
    {
        infoBox2.gameObject.SetActive(true);
        title1.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        infoString += "\n\nPress Q to continue";
        infoBox2.text = infoString;
        title1.text = titleText;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.infoDialogue = true;
        //Cursor.visible = true;
        depthOfField(true);

        oneImage.transform.gameObject.SetActive(true);
        oneImage.sprite = i_sprite;
    }

    public void playInfoTwoImage(string infoString, Sprite i_sprite1, string titleText1, Sprite i_sprite2, string titleText2)
    {
        infoBox3.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        infoString += "\n\nPress Q to continue";
        infoBox3.text = infoString;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.infoDialogue = true;
        //Cursor.visible = true;
        depthOfField(true);

        twoImage1.transform.gameObject.SetActive(true);
        twoImage1.sprite = i_sprite1;

        title2.gameObject.SetActive(true);
        title2.text = titleText1;

        twoImage2.transform.gameObject.SetActive(true);
        twoImage2.sprite = i_sprite2;

        title3.gameObject.SetActive(true);
        title3.text = titleText2;
    }

    void Update()
    {
        if (!pauseMenu.isPaused && Input.GetKeyDown(KeyCode.Q))
        {
            infoBox1.gameObject.SetActive(false);
            infoBox2.gameObject.SetActive(false);
            infoBox3.gameObject.SetActive(false);

            title1.gameObject.SetActive(false);
            title2.gameObject.SetActive(false);
            title3.gameObject.SetActive(false);

            background.gameObject.SetActive(false);

            infoBox1.text = "";
            infoBox2.text = "";
            infoBox3.text = "";

            title1.text = "";
            title2.text = "";
            title3.text = "";

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
