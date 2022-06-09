using UnityEngine;

namespace Script.Event
{
    public class ParticleActionDetected
    {
        public Vector3 targetPsition;
        public ParticleType particleType;

        public ParticleActionDetected(Vector3 _targetPsition, ParticleType _particle)
        {
            targetPsition = _targetPsition;
            particleType = _particle;
        }
    }
}