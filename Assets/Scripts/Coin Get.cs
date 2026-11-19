using UnityEngine;

public class CoinGet : MonoBehaviour
{
    public Transform playerl;
    public GameObject Coin;
    private int CoinAmount;
    private void OnTriggerEnter(Collider other)
    {
        Coin.SetActive(false);
        CoinAmount = CoinAmount + 1;
        Debug.Log(CoinAmount);
    }
}
