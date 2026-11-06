using UnityEngine;

public class teleport : MonoBehaviour
{
    public Transform playerl, Destination;
    public GameObject Player;
    private void OnTriggerEnter(Collider other)
    {
        Player.SetActive(false);
        playerl.position = Destination.position;
        Player.SetActive(true);
    }
}
