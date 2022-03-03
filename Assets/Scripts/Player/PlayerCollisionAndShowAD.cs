using UnityEngine;
using TMPro;
using System;
using GoogleMobileAds.Api;

public class PlayerCollisionAndShowAD : MonoBehaviour
{
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private TextMeshProUGUI _coinCounter;
    [SerializeField] private int _coinCost;
    [SerializeField] private float _timeMagnifier;
    private float _timerAD;
    private InterstitialAd _interstitialAd;
    private string _interstitialID;

    private void Start()
    {
        _timerAD = PlayerPrefs.GetFloat("Timer");
        MobileAds.Initialize(initStatus => { });
        _interstitialID = "ca-app-pub-3940256099942544/1033173712";
    }

    private void Awake()
    {
        _interstitialAd = new InterstitialAd(_interstitialID);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _interstitialAd.LoadAd(adRequest);
    }

    private void Update()
    {
        _timerAD += Time.deltaTime;
        PlayerPrefs.SetFloat("Timer", _timerAD);
    }

    private void ShowAd()
    {
        if(_timerAD >= 45 && _interstitialAd.IsLoaded())
        {
            _interstitialAd.Show();
            _timerAD = 0;
            PlayerPrefs.SetFloat("Timer", _timerAD);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Obstacle"))
        {
            ShowAd();
            _loseScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            _coinCounter.text = (Convert.ToInt32(_coinCounter.text) + _coinCost).ToString();
            Time.timeScale += _timeMagnifier;
        }
    }
}
