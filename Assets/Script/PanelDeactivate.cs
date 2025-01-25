using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelDeactivate : MonoBehaviour
{
    public GameObject panel; // Panel to be activated and deactivated

    private bool isPanelActive = false; // To track panel state

    void Start()
    {
        // Activate the panel when the scene starts
        if (panel != null)
        {
            panel.SetActive(true);
            isPanelActive = true;
        }
    }

    void Update()
    {
        // Check for user click and deactivate the panel
        if (isPanelActive && Input.GetMouseButtonDown(0)) // 0 is for left mouse button
        {
            if (panel != null)
            {
                panel.SetActive(false);
                isPanelActive = false;
            }
        }
    }
}