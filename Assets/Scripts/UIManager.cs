using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Space(10)]
    [Header("Settings")]
    [Space(10)]
    public float coinCountVibrationDuration = 0.5f;
    public float coinCountVibrationIntensity = 0.1f;

    [Space(10)]
    [Header("Elements")]
    [Space(10)]
    public TextMeshProUGUI coinCountText;
    public TextMeshProUGUI blockCountText;
    public GameObject MaxCountText;
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

    public void UpdateCoins(int coins)
    {
        coinCountText.text = coins.ToString();

        StartCoroutine(VibrateCoinCount());
    }

    private IEnumerator VibrateCoinCount()
    {
        Vector3 originalPos = coinCountText.transform.position;

        float timeElapsed = 0f;
        while (timeElapsed < coinCountVibrationDuration)
        {
            float x = Random.Range(-coinCountVibrationIntensity, coinCountVibrationIntensity);
            float y = Random.Range(-coinCountVibrationIntensity, coinCountVibrationIntensity);

            coinCountText.transform.position = originalPos + new Vector3(x, y, 0f);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        coinCountText.transform.position = originalPos;
    }
}
