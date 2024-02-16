using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowderExhaust : MonoBehaviour
{
    public FireCannon fireCannonScript; // Assignez ce champ via l'inspecteur d'Unity


    private void OnTriggerEnter(Collider other)
    {
        PowderPot powderPot = other.GetComponent<PowderPot>();
        if (powderPot != null)
        {
            // Appelez la méthode AdjustPowderAmount sur fireCannonScript
            fireCannonScript.AdjustPowderAmount(powderPot.GetPowderAmount());
            powderPot.EmptyPot();
        }
    }
}
