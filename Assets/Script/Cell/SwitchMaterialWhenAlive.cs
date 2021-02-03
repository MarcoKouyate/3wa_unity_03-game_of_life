using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOfLife
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(CellManager))]
    public class SwitchMaterialWhenAlive : MonoBehaviour
    {
        [SerializeField] private Material _aliveMaterial;
        [SerializeField] private Material _deadMaterial;


        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _cellManager = GetComponent<CellManager>();
            SwitchMaterial();
        }

        private void SwitchMaterial()
        {
            _meshRenderer.material = (_cellManager.IsAlive) ? _aliveMaterial : _deadMaterial;
        }

        private void Update()
        {
            SwitchMaterial();
        }

        private MeshRenderer _meshRenderer;
        private CellManager _cellManager;
    }
}

