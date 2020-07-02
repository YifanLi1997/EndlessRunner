using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollecter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] AudioClip coinClip;
    [SerializeField] float coinClipVolume = 0.5f;

    [Header("Only for Config")]
    [SerializeField] private int c_numberOfCoins = 0;

    void Start()
    {
        UpdateUIText();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Coin"))
        {
            Animator coinAnimator = other.gameObject.GetComponent<Animator>();
            coinAnimator.SetTrigger("IsDisappearing");
            StartCoroutine(DelayDisable(other));
            c_numberOfCoins++;
            AudioSource.PlayClipAtPoint(coinClip, Camera.main.transform.position, coinClipVolume);
            UpdateUIText();
        }
    }

    private void UpdateUIText()
    {
        coinText.text = "Coin: " + c_numberOfCoins.ToString();
    }

    IEnumerator DelayDisable(Collider other)
    {
        other.enabled = false;
        yield return new WaitForSeconds(0.5f); // the length of the disappearing animator
        other.enabled = true;
        other.gameObject.SetActive(false);
        other.transform.localScale = new Vector3(60,60,60); //reset transform
    }

}
