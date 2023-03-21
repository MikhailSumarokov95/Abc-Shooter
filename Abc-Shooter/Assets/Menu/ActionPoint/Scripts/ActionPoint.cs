using UnityEngine;

public class ActionPoint : MonoBehaviour
{
    [SerializeField] private GameObject UIElement;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIElement.SetActive(true);
            FindObjectOfType<MenuManager>().OnPause(true);
        }
    }
}
