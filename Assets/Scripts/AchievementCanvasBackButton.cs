using UnityEngine;

public class AchievementCanvasBackButton : MonoBehaviour
{
    [HideInInspector]
    public Transform Mount;
    private MainMenuCamControl _mainMenuCamControl;
    public void SetRightMount()
    {
        _mainMenuCamControl = _mainMenuCamControl ?? GameObject.Find("Camera").GetComponent<MainMenuCamControl>();
        _mainMenuCamControl.setMount(Mount);
    }
}
