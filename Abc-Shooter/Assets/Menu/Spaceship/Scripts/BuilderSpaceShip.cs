using UnityEngine;

public class BuilderSpaceShip : MonoBehaviour
{
    [SerializeField] private GameObject[] shipParts;
    [SerializeField] private GameObject effect;

    private void Start()
    {
        foreach (var part in shipParts) 
            part.SetActive(false);

        var shipAssemblyStage = Progress.GetShipAssemblyStage();
        for (var i = 0; i < shipAssemblyStage; i++)
        {
            shipParts[i].SetActive(true);
        }
    }

    public void AddShipAssemblyStage()
    {
        var shipAssemblyStage = Progress.GetShipAssemblyStage();
        if (shipAssemblyStage < Progress.GetNumberPartsFoundShip() && shipAssemblyStage < shipParts.Length)
        {
            shipAssemblyStage++;
            Progress.SetShipAssemblyStage(shipAssemblyStage);
            var part = shipParts[shipAssemblyStage - 1];
            part.SetActive(true);
            Instantiate(effect, part.transform);
        }
    }

    [ContextMenu("AddParts")]
    public void AddParts()
    {
        Progress.SetNumberPartsFoundShip(Progress.GetNumberPartsFoundShip() + 1);
    }
}
