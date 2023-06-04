using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    public GameObject BuildMenu;

    private bool Toggel;

    private void Start()
    {
        BuildMenu.SetActive(false);
        Toggel = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleBuildMenu();
        }
    }

    private void ToggleBuildMenu()
    {
        Toggel = !Toggel;
        BuildMenu.SetActive(Toggel);

        if (Toggel)
        {
            // Enable interaction with the UI element
            BuildMenu.GetComponent<CanvasGroup>().interactable = true;
            BuildMenu.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            // Disable interaction with the UI element
            BuildMenu.GetComponent<CanvasGroup>().interactable = false;
            BuildMenu.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
}
