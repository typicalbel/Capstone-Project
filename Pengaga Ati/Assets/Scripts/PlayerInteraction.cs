using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Examples
{
    public class PlayerInteraction : MonoBehaviour
    {
        Player player;
        FarmingLand selectedLand = null;

        // Start is called before the first frame update
        void Start()
        {
            // Get access to our Player script component
            player = transform.GetComponent<Player>();
        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit, 1))
            {
                OnInteractableHit(hit);
            }
        }

        // Handles the interaction when player interact with farming land
        void OnInteractableHit(RaycastHit hit)
        {
            Collider other = hit.collider;
            
            //Check if player is going to interact with land
            if(other.tag == "Land")
            {
                // Get the farming land script component
                FarmingLand farmingLand = other.GetComponent<FarmingLand>();
                SelectLand(farmingLand);
                return;
            }

            //Deselect the land if the player is not standing on any land at the moment
            if(selectedLand != null)
            {
                selectedLand.Select(false);
                selectedLand = null;
            }
        }

        //Handles the selection process
        void SelectLand(FarmingLand farmingLand)
        {
            //Set the previously selected land to false (If any)
            if(selectedLand != null)
            {
                selectedLand.Select(false);
            }

            //Set the new selected land to the land we're selecting now
            selectedLand = farmingLand;
            farmingLand.Select(true);
        }

    }
}

