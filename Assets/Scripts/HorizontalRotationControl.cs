using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HorizontalRotationControl : MonoBehaviour
{
    public Transform rotater; // Assignez votre canon ici via l'inspecteur
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
            float deltaAngle = Vector3.SignedAngle(crankToLastInteractor, crankToCurrentInteractor, transform.forward);
            controlRotation += deltaAngle;

            rotater.Rotate(Vector3.up, 10 * deltaAngle, Space.World);

            lastInteractorPosition = currentInteractorPosition; // Mettre à jour la dernière position pour le prochain calcul
        }
    }
}
