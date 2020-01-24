using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisksManager : MonoBehaviour
{

    [SerializeField] private List<Disk> disks = new List<Disk>();
    [SerializeField] private Transform goldenZone;
    private DefinitionOfParameters param;
    private int index = 0;

    private void Start()
    {
        if (disks.Count == 0)
            Debug.LogError("Not added ScriptableObject");
        goldenZone.localScale = new Vector3(disks[0].length, goldenZone.localScale.y, goldenZone.localScale.z);

        param = FindObjectOfType<DefinitionOfParameters>().GetComponent<DefinitionOfParameters>();
        if (param == null)
            Debug.LogError("Not founded a DefinitionOfParameters");

        param.height = disks[0].height;
    }

    public void ReloadGoldenZone()
    {
        index++;
        if(index > disks.Count - 1)
        {
            index = 0;
        }

        goldenZone.localScale = new Vector3(disks[index].length, goldenZone.localScale.y, goldenZone.localScale.z);
        param.height = disks[index].height;
    }

}
