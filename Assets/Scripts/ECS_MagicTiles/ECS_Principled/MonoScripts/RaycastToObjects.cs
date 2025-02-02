using EventChannel;
using UnityEngine;

namespace ECS_MagicTile
{
    public class RaycastToObjects : MonoBehaviour
    {
        [SerializeField]
        private LayerMask targetLayer;

        [SerializeField]
        private IntEventChannel startNoteEntityIdChannel;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                FireRaycast();
            }
        }

        private void FireRaycast()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(
                mousePosition,
                Vector2.zero,
                Mathf.Infinity,
                targetLayer
            );

            if (hit.collider != null)
            {
                if (hit.collider.tag == "StartNote")
                {
                    startNoteEntityIdChannel?.RaiseEvent(
                        hit.collider.GetComponent<IEntityHolder>().EntityId
                    );
                }
            }
            else
            {
                Debug.Log("No object hit.");
            }
        }
    }
}
