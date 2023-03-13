using DG.Tweening;
using UnityEngine;

public class SellManager : MonoBehaviour
{
    public static SellManager Instance;

    [Space(10)]
    [Header("Settings")]
    [Space(10)]
    [SerializeField] private float coinFlightDuration = 1f;
    [Space(10)]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform coinStartPos;
    [SerializeField] private Transform coinEndPos;
   

    private int coinsSold = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SellBlock(Block block)
    {
        InstantiateCoin(block.transform.position);
        AddCoins(block.coinValue);
        Destroy(block.gameObject);
    }

    private void InstantiateCoin(Vector3 startPos)
    {
        GameObject coin = Instantiate(coinPrefab, startPos, Quaternion.identity);
        coin.transform.SetParent(transform);
        coin.transform.DOPath(new Vector3[] { coinStartPos.position, coinEndPos.position }, coinFlightDuration, PathType.Linear)
            .SetEase(Ease.Linear)
            .OnComplete(() => Destroy(coin));
    }

    private void AddCoins(int amount)
    {
        coinsSold += amount;
        UIManager.Instance.UpdateCoins(coinsSold);
    }
}
