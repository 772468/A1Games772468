using UnityEngine;

public class CoinGet : MonoBehaviour
{
    public Transform playerl;
    public GameObject Coin;
    public int CoinAmount;
    private void OnTriggerEnter(Collider other)
    {
        Coin.SetActive(false);
        CoinAmount = CoinAmount + 1;
    }
}
   