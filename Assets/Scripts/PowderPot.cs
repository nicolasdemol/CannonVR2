using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowderPot : MonoBehaviour
{
    [SerializeField]
    private float powderAmount = 10f; // Quantité initiale de poudre

    public float GetPowderAmount()
    {
        return powderAmount;
    }

    public void EmptyPot()
    {
        powderAmount = 0f;
    }
}
