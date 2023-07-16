using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator ani;

    [SerializeField] private AudioSource soundDeathEffect;

    // Start is called before the first frame update
    private void Start()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
            Debug.Log(">>>>");
        }
    }

    private void Die()
    {
        soundDeathEffect.Play();
        rig.bodyType = RigidbodyType2D.Static;  
        ani.SetTrigger("death");
    }

    private void StartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
