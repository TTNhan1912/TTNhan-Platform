using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    private int cheries = 0;
    private int kiwi = 0;
    private int dautay = 0;

    [SerializeField] private Text cheriesText;
    [SerializeField] private Text kiwiText;
    [SerializeField] private Text daytayText;

    [SerializeField] private AudioSource soundCollectEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            soundCollectEffect.Play();
            Destroy(collision.gameObject);
            cheries++;
            cheriesText.text = " " + cheries;
        }
        if (collision.gameObject.CompareTag("Kiwi"))
        {
            soundCollectEffect.Play();
            Destroy(collision.gameObject);
            kiwi++;
            kiwiText.text = " " + kiwi;
        }
        if (collision.gameObject.CompareTag("DauTay"))
        {
            soundCollectEffect.Play();
            Destroy(collision.gameObject);
            dautay++;
            daytayText.text = " " + dautay;
        }
    }
}
