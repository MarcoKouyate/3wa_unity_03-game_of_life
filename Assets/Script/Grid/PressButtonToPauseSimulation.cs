using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOfLife
{
    [RequireComponent(typeof(GridManager))]
    public class PressButtonToPauseSimulation : MonoBehaviour
    {
        private void Awake()
        {
            _gridManager = GetComponent<GridManager>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Pause"))
            {
                Debug.Log("Pause");
                _gridManager.Pause = !_gridManager.Pause;
            }
        }

        private GridManager _gridManager;
    }

}
