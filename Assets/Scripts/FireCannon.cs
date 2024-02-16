using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class FireCannon : MonoBehaviour
{
    public Rigidbody cannonBallPrefab;
    public GameObject fireEffectPrefab; // Assignez votre préfabriqué d'effet de feu ici via l'inspecteur
    private GameObject fireEffectInstance; // Pour garder une référence à l'instance de l'effet de feu
    public GameObject explosionEffectPrefab;

    public Transform cannonExit; // Point de sortie du boulet de canon
    public TMP_Text powderText; // ou TextMeshProUGUI si vous utilisez l'interface utilisateur


    public float powderAmount = 0.0f; // Quantité de poudre, ajustable dans l'inspecteur
    public float baseVelocity = 5.0f; // Vitesse de base du boulet de canon


    private void UpdatePowderText()
    {
        if (powderText != null)
        {
            powderText.text = "Poudre : " + powderAmount.ToString("F2"); // "F2" pour deux décimales
        }
    }

    public void AdjustPowderAmount(float amountToAdd)
    {
        powderAmount += amountToAdd;
        UpdatePowderText();
    }

    void Start()
    {
        UpdatePowderText();
        XRSimpleInteractable interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnSelectEnter);
    }

    void OnSelectEnter(SelectEnterEventArgs args)
    {
        IgniteFuse();
    }


    // Méthode pour simuler l'allumage de la mèche
    public void IgniteFuse()
    {
        if (fireEffectInstance == null) // Vérifie si l'effet n'a pas déjà été instancié
        {
            // Instancie l'effet de feu à la position de la mèche
            fireEffectInstance = Instantiate(fireEffectPrefab, transform.position, Quaternion.identity, transform);
        } else
        {
            fireEffectInstance.SetActive(true);
        }
        StartCoroutine(FireAfterDelay(3.0f));
    }

    IEnumerator FireAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (fireEffectInstance != null)
        {
            fireEffectInstance.SetActive(false);
        }

        Fire();
        ResetCannon();
    }

    private void ResetCannon()
    {
        powderAmount = 0.0f;
        UpdatePowderText();
    }


    public void Fire()
    {
        if (explosionEffectPrefab != null)
        {
            GameObject explosionEffect = Instantiate(explosionEffectPrefab, cannonExit.position, Quaternion.identity);
            Destroy(explosionEffect, 2f); // Assurez-vous de détruire l'objet après un certain temps pour éviter de surcharger la scène
        }

        Rigidbody newBall = Instantiate(cannonBallPrefab, cannonExit.position, Quaternion.identity);
        // Calculer la vitesse initiale basée sur la quantité de poudre
        float initialVelocity = baseVelocity * powderAmount;
        // Appliquer la vitesse initiale au boulet de canon
        newBall.velocity = cannonExit.forward * initialVelocity;
    }

    void Update()
    {

    }
}
