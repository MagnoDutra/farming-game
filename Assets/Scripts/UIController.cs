using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject[] toolBarActivatorIcons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchTool(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchTool(int tool)
    {
        foreach (GameObject activator in toolBarActivatorIcons)
        {
            activator.SetActive(false);
        }

        toolBarActivatorIcons[tool].SetActive(true);
    }
}
