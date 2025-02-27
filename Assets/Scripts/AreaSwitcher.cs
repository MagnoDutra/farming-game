using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaSwitcher : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Transform startPoint;

    void Start()
    {
        PlayerController.instance.transform.position = startPoint.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.Tags.PLAYER))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
