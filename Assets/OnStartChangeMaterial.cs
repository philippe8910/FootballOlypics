using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartChangeMaterial : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer currentMaterial;
    // Start is called before the first frame update
    void Start()
    {
        currentMaterial = GetComponent<SkinnedMeshRenderer>();
        
        var switchMaterial = currentMaterial.materials;

        var bodyIndex = PlayerPrefs.GetString("BodyIndex");

        var targetMaterial = Resources.Load<Material>("BodyMaterial/" + bodyIndex);

        switchMaterial[0] = targetMaterial;

        currentMaterial.materials = switchMaterial;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
