using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetExplosion : MonoBehaviour
{
    public GameObject targetBroken; // La cible cassée à activer

    private void OnCollisionEnter(Collision collision)
    {
        // Vérifier si l'objet en collision a le tag "CannonBall"
        if (collision.collider.CompareTag("CannonBall"))
        {
            Explode();
        }
    }

    private void Explode()
    {

        // Activer la cible cassée
        if (targetBroken != null)
        {
            GameObject brokenTargetInstance = Instantiate(targetBroken, transform.position, transform.rotation);
            // Appliquer une force aux fragments si nécessaire
            foreach (Transform child in brokenTargetInstance.transform)
            {
                Rigidbody rb = child.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(1000, transform.position, 5);
                }
            }
        }

        // Désactiver ou détruire la cible originale
        Destroy(gameObject);
    }
}

