using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{
    [SerializeField] Transform leader; // Référence au personnage "Dragon"
    public float followDistance = 2f; // Distance à maintenir entre le "Dwarf" et le "Dragon"
    public float followSpeed = 5f; // Vitesse à laquelle "Dwarf" suit "Dragon"

    private void Update()
    {
        // Calculer la distance entre "Dwarf" et "Dragon"
        float distance = Vector3.Distance(transform.position, leader.position);

        // Si la distance dépasse la valeur définie, "Dwarf" se déplace vers "Dragon"
        if (distance > followDistance)
        {
            // Calcul de la direction à suivre
            Vector3 direction = (leader.position - transform.position).normalized;

            // Déplacement de "Dwarf" vers "Dragon"
            transform.position += direction * followSpeed * Time.deltaTime;
        }
    }
}
