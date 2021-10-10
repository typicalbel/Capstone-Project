using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Examples
{
    public class FarmingLand : MonoBehaviour
    {
        public GameObject select;

        public void Select(bool toggle)
        {
            select.SetActive(toggle);
        }
    }
}

