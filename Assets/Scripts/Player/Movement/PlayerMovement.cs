using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {

        public float moveSpeed;
        private CharacterController _characterController;    
        void Start()
        {
            gameObject.tag = "Player";
            _characterController = GetComponent<CharacterController>();
        }
        
        void Update()
        {
            var x = Input.GetAxisRaw("Horizontal");
            var z = Input.GetAxisRaw("Vertical");
            var direction = new Vector3(x, 0f, z).normalized;
            if (direction.magnitude >= 0.1f)
            {
                _characterController.Move(direction * (moveSpeed * Time.deltaTime));     
            }
       
        }
    
    
    }
}
