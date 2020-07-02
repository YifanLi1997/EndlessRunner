using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollecter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;

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
            UpdateUIText();
        }
    }

    private void UpdateUIText()
    {
        coinText.text = "Coin: " + c_numberOfCoins.ToString();
    }

    IEnumerator DelayDisable(Collider other)
    {
        yield return new WaitForSeconds(0.5f); // the length of the disappearing animator
        other.gameObject.SetActive(false);
    }

}
