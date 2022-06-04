using UnityEngine;

namespace Project.Event
{
    public class ChangeSkinDetected
    {
        public Material currentMaterial;

        public ChangeSkinDetected(Material _currentMaterial)
        {
            currentMaterial = _currentMaterial;
        }
    }
}