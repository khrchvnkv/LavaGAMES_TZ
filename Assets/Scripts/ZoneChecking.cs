using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Скрипт для определения нахождения игрока в зоне Диска
public class ZoneChecking : MonoBehaviour
{
    public bool isInZone
    {
        get;
        private set;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isInZone = false;
        }
    }
}
