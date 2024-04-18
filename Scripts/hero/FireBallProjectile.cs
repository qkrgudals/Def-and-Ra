using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FireBallProjectile : MonoBehaviour
{
    public int damage;
    public float speed;

    private GameObject caster;

    void Update()
    {
        StartCoroutine(DestroyObject());

        gameObject.transform.TransformDirection(Vector3.forward);
        gameObject.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    public void SetCaster(GameObject caster)
    {
        this.caster = caster;
    }
    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy targetEnemy = other.gameObject.GetComponent<Enemy>();
            targetEnemy?.HeroTakeDamage(damage, caster);
            Destroy(gameObject);
        }
    }
}
