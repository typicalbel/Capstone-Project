using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Examples
{
    public class PlayerInteractionPickUp : MonoBehaviour
    {
        Player player;

        public bool pickedItem = true;

        // Start is called before the first frame update
        void Start()
        {
            // Get access to our Player script component
            player = GetComponent<Player>();
        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
            {
                OnInteractableHit(hit);
            }
        }

        // Handles the interaction when player interact with plants
        void OnInteractableHit(RaycastHit hit)
        {
            Collider other = hit.collider;

            //Check if player is going to interact with plant
            if (other.tag == "PickUp")
            {
                Debug.Log("pick up");
                pickedItem = false;
            }
        }
    }
}
