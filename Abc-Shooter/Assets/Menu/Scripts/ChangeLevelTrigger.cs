using UnityEngine;

public class ChangeLevelTrigger : MonoBehaviour
{
    public enum ChangeLevelType
    {
        GoLevel,
        GoMenu
    }

    [SerializeField] private ChangeLevelType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case ChangeLevelType.GoLevel:
                    FindObjectOfType<Level>().StartGame();
                    break;
                case ChangeLevelType.GoMenu:
                    FindObjectOfType<LevelManager>().StartWinGamePanel();
                    break;
            }
        }
    }
}