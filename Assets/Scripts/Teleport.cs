using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform playerl, Destination;
    public GameObject Player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.SetActive(false);
            playerl.position = Destination.position;
            Player.SetActive(true);
        }
    }
}
