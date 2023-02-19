using UnityEngine;

namespace CodeMonkey.KitchenCaosControl.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;

        public bool IsWalking { get; private set; }

        private void Update()
        {
            var inputVector = new Vector3().normalized;

            var moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

            var t = transform;
            t.position += moveDirection * (moveSpeed * Time.deltaTime);

            IsWalking = moveDirection != Vector3.zero;
            t.forward = Vector3.Slerp(t.forward, moveDirection, Time.deltaTime * rotationSpeed);
        }
    }
}
