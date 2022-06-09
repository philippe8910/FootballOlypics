using System.Collections;
using System.Collections.Generic;
using Project;
using Script.Event;
using UnityEngine;

public class ParticlCtr : MonoBehaviour
{
    void Start()
    {
        EventBus.Subscribe<ParticleActionDetected>(OnParticleActionDetected);
    }

    private void OnParticleActionDetected(ParticleActionDetected obj)
    {
        var particleType = obj.particleType;
        var targetPsition = obj.targetPsition;

        var particlePrefab = TypeComplie(particleType);

        var g = Instantiate(particlePrefab, targetPsition, Quaternion.identity);
        Destroy(g,2);
        
        
    }

    private GameObject TypeComplie(ParticleType particleType)
    {
        var name = "";
        
        switch (particleType)
        {
            case ParticleType.KickEffect_Adger:
                name = "Kick_Effect_v01";
                break;
            
            case ParticleType.KickEffect_Azer:
                name = "Kick_Effect_v02";
                break;
        }
        
        var obj = Resources.Load<GameObject>("Particle/" + name);

        return obj;
    }
    
    

    void Update()
    {
        
    }
}
