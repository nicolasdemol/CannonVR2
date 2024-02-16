using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VerticalRotationControl : MonoBehaviour
{
    public Transform canon; // Assignez votre canon ici via l'inspecteur
    private XRBaseInteractor interactor = null;
    private Vector3 lastInteractorPosition;
    private float controlRotation = 0f;

    void Awake()
    {
        var interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnSelectEnter);
        interactable.selectExited.AddListener(OnSelectExit);
    }

    void OnSelectEnter(SelectEnterEventArgs args)
    {
        interactor = args.interactorObject as XRBaseInteractor;
        if (interactor != null)
        {
            lastInteractorPosition = interactor.transform.position;
        }
    }

    void OnSelectExit(SelectExitEventArgs args)
    {
        interactor = null;
    }

    void Update()
    {
        if (interactor != null)
        {
            Vector3 currentInteractorPosition = interactor.transform.position;
            Vector3 crankToCurrentInteractor = currentInteractorPosition - transform.position;
            Vector3 crankToLastInteractor = lastInteractorPosition - transform.position;

            // Calculer l'angle de rotation
            float deltaAngle = Vector3.SignedAngle(crankToLastInteractor, crankToCurrentInteractor, canon.right);
            controlRotation += deltaAngle;

            // Appliquer la rotation à la manivelle et au canon
            transform.Rotate(transform.forward, 20 * deltaAngle, Space.World);
            canon.Rotate(canon.right, 2 * deltaAngle, Space.World);

            lastInteractorPosition = currentInteractorPosition; // Mettre à jour la dernière position pour le prochain calcul
        }
    }
}
