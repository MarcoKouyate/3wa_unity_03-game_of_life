using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOfLife {
    public class GridExample : MonoBehaviour
    {
        private void Awake()
        {
            // (i , j)


            // Neighboors!
            //(i-1, j-1)
            //(i-1, j)
            //(i-1, j+1)

            //(i, j-1)
            //(i, j+1)

            //(i+1, j-1)
            //(i+1, j)
            //(i+1, j+1)
        }

        private void Update()
        {

        }
    }
}

