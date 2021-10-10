using UnityEngine;
using TouchControlsKit;

namespace Examples
{
    public class Player : MonoBehaviour
    {
        bool binded;
        Transform myTransform, cameraTransform;
        CharacterController controller;
        float rotation;
        bool jump, prevGrounded;
        float weapReadyTime;
        bool weapReady = true;
        public bool speed;

        public Transform bulletDest;
        public float range;
        //public GameObject crosshair;

        public Camera secondCamera;

        public Transform pickUpDest;
        public Rigidbody pickItem;

        public bool pickedItem = true;

        // Awake
        void Awake()
        {
            myTransform = transform;
            cameraTransform = Camera.main.transform;
            controller = GetComponent<CharacterController>();

            secondCamera.transform.position = new Vector3(0.5f, 3.4f, transform.position.z);
        }

        // Update
        void Update()
        {
            // Setting the crosshair to false for default
            //crosshair.gameObject.SetActive(false);

            // Setting up the shooting mechanics
            if ( weapReady == false )
            {
                weapReadyTime += Time.deltaTime;
                if( weapReadyTime > 1f )
                {
                    weapReady = true;
                    weapReadyTime = 0f;
                }
            }

            // Assigning the pick up mechanics to the pick up button
            if( TCKInput.GetAction( "pickBtn", EActionEvent.Press))
            {
                if (pickedItem == false)
                {
                    PickUp();
                }  
            }

            // Assigning the item to drop when the button is not pressed
            if (TCKInput.GetAction("pickBtn", EActionEvent.Up))
            {
                PickDown();
            }

            // Assigning the shooting mechanics to the shooting button
            if ( TCKInput.GetAction( "fireBtn", EActionEvent.Press ) )
            {
                PlayerFiring();
            }

            // Navigating the camera angles according to the player's touch on the touchpad area of the screen
            Vector2 look = TCKInput.GetAxis( "Touchpad" );
            PlayerRotation( look.x, look.y );
        }

        // FixedUpdate
        void FixedUpdate()
        {
            /*float moveX = TCKInput.GetAxis( "Joystick", EAxisType.Horizontal );
            float moveY = TCKInput.GetAxis( "Joystick", EAxisType.Vertical );*/
            
            // Assign the movement of the character to a joystick
            Vector2 move = TCKInput.GetAxis( "Joystick" ); // NEW func since ver 1.5.5
            PlayerMovement( move.x, move.y );
        }


        // Jumping
        private void Jumping()
        {
            if( controller.isGrounded )
                jump = true;
        }

                        
        // PlayerMovement
        private void PlayerMovement( float horizontal, float vertical )
        {
            secondCamera.gameObject.SetActive(false);

            bool grounded = controller.isGrounded;

            Vector3 moveDirection = myTransform.forward * vertical;
            moveDirection += myTransform.right * horizontal;

            moveDirection.y = -10f;

            if( jump )
            {
                jump = false;
                moveDirection.y = 25f;
                /*isPorjectileCube = !isPorjectileCube;*/
            }

            if( grounded )            
                moveDirection *= 5f;
            
            controller.Move( moveDirection * Time.fixedDeltaTime);

            if( !prevGrounded && grounded )
                moveDirection.y = 0f;

            prevGrounded = grounded;
        }

        // PlayerRotation
        public void PlayerRotation( float horizontal, float vertical )
        {
            myTransform.Rotate( 0f, horizontal * 12f, 0f );
            rotation += vertical * 12f;
            rotation = Mathf.Clamp( rotation, -60f, 60f );
            cameraTransform.localEulerAngles = new Vector3( -rotation, cameraTransform.localEulerAngles.y, 0f );
        }

        // PlayerFiring
        public void PlayerFiring()
        {
            // Camera zooms in on the screen when player shoots
            secondCamera.gameObject.SetActive(true);

            // Crosshair enabled when shooting
            //crosshair.gameObject.SetActive(true);

            if ( !weapReady )
                return;

            weapReady = false;

            // Setting up the bullets 
            GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            primitive.transform.position = bulletDest.position;
            primitive.transform.localScale = Vector3.one * .2f;
            Rigidbody rBody = primitive.AddComponent<Rigidbody>();
            Transform camTransform = secondCamera.transform;
            rBody.AddForce( camTransform.forward * range, ForceMode.Impulse );
            Destroy( primitive, 0.5f );
        }

        // PlayerPickUp
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            // When players get close to items with tag "PickUp"
            if (hit.gameObject.tag.Equals("PickUp"))
            {
                pickedItem = false;
            }
            else
            {
                pickedItem = true;
            }
        }

        private void PickUp()
        {
            // Coding the pickable items to be carried
            pickItem.useGravity = false;
            pickItem.transform.position = pickUpDest.position;
            pickItem.transform.parent = GameObject.Find("PickUpDestination").transform;
            pickItem.constraints = RigidbodyConstraints.FreezeAll;
        }

        private void PickDown()
        {
            //Debug.Log("Player drop item");
            pickItem.constraints = RigidbodyConstraints.None;
            pickItem.transform.parent = null;
            pickItem.useGravity = true;
            pickedItem = true;
        }


        // PlayerClicked
        public void PlayerClicked()
        {
            //Debug.Log( "PlayerClicked" );
        }
    };
}