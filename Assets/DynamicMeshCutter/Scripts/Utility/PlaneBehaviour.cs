
using UnityEngine;

namespace DynamicMeshCutter
{
    public class PlaneBehaviour : CutterBehaviour
    {
        private void LateUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cut();
            }
        }

        public void Cut()
        {
            var roots = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var root in roots)
            {
                if (!root.activeInHierarchy)
                    continue;
                var targets = root.GetComponentsInChildren<MeshTarget>();
                foreach (var target in targets)
                {
                    Cut(target, transform.position, transform.forward, null, OnCreated);
                }
            }
        }

        void OnCreated(Info info, MeshCreationData cData)
        {
            MeshCreation.TranslateCreatedObjects(info, cData.CreatedObjects, cData.CreatedTargets, Separation);
        }

    }
}