using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOfLife
{
    [RequireComponent(typeof(CellManager))]
    public class SwitchStateOnMouseClick : MonoBehaviour
    {
        private void Awake()
        {
            _cellManager = GetComponent<CellManager>();
        }

        private void OnMouseDown()
        {
            _cellManager.SwitchState();
        }

        private CellManager _cellManager;
    }
}

