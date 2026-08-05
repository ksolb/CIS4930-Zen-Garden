using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject soundSettingsPanel;

    public void OpenSoundSettings()
    {
        mainMenuPanel.SetActive(false);
        soundSettingsPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        soundSettingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}