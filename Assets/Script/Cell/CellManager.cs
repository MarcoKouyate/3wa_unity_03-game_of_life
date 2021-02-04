using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOfLife
{
    [RequireComponent(typeof(MeshRenderer))]
    public class CellManager : MonoBehaviour
    {
        [SerializeField] private bool _isAlive;

        public bool IsAlive
        {
            get => _isAlive;
        }

        public bool ShouldBeAlive
        {
            set => _shouldBeAlive = value;
        }

        public void UpdateState()
        {
            _isAlive = _shouldBeAlive;
        }

        public void SwitchState()
        {
            _isAlive = !_isAlive;
        }

        private bool _shouldBeAlive;
    }
}

