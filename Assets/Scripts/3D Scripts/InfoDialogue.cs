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

    void Awake()
    {
        dof = camera.GetComponent<UnityStandardAssets.ImageEffects.DepthOfField>();
        depthOfField(false);
        infoBox = transform.FindChild("InfoText").GetComponent<Text>();
        infoBox.gameObject.SetActive(false);
        pauseMenu = pauseMenuGO.GetComponent<PauseMenu>();
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
