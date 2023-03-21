using UnityEngine;

public class BuilderSpaceShip : MonoBehaviour
{
    [SerializeField] private GameObject[] shipParts;

    private void Start()
    {
        foreach (var part in shipParts) 
            part.SetActive(false);

        var shipAssemblyStage = Progress.GetShipAssemblyStage();
        for (var i = 0; i < shipAssemblyStage; i++)
        {
            shipParts[i].gameObject.SetActive(true);
        }
    }

    public void AddShipAssemblyStage()
    {
        var shipAssemblyStage = Progress.GetShipAssemblyStage();
        if (shipAssemblyStage < Progress.GetNumberPartsFoundShip())
        {
            shipAssemblyStage++;
            Progress.SetShipAssemblyStage(shipAssemblyStage);
            shipParts[shipAssemblyStage - 1].SetActive(true);
        }
    }
}
