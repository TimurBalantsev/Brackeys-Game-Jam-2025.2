using UnityEngine;

public class Scarecrow : MonoBehaviour, Interactable
{
    public void Interact(Player player)
    {
        Debug.Log($"BOO {player}");
    }
}
