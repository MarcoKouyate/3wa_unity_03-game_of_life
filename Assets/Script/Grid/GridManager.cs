using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOfLife
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private int _rows;
        [SerializeField] private int _columns;
        [SerializeField] private GameObject _cellPrefab;
        [SerializeField] private float _stepInterval;
        [SerializeField] private bool _pause;

        public bool Pause
        {
            get => _pause;
            set => _pause = value;
        }

        private void Awake()
        {
            Init();
            CreateCells();
        }


        private void Init()
        {
            _nextStepTime = Time.time;

            _cells = new CellManager[_columns, _rows];
            _transform = transform;

            _neighboursIndices = new int[8, 2] {
                {-1,-1},
                {-1, 0},
                {-1, 1},

                { 0,-1},
                { 0, 1},

                { 1,-1},
                { 1, 0},
                { 1, 1}
            };
        }

        private void CreateCells()
        {
            Vector2 cellScale = new Vector2(_transform.localScale.x / _columns, _transform.localScale.y / _rows);
            Vector2 offset = new Vector2(_transform.position.x - _transform.localScale.x / 2 + cellScale.x / 2, _transform.position.y + _transform.localScale.y /2 - cellScale.y / 2);


            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    GameObject cell = Instantiate(_cellPrefab, _transform) ;
                    cell.transform.localScale = cellScale / (Vector2)_transform.localScale;
                    cell.transform.position = _transform.position + (Vector3) offset + Vector3.right * (cellScale.x * i) + Vector3.down * (cellScale.y * j);
                    _cells[i, j] = cell.GetComponent<CellManager>();
                }
            }
        }

        private void Update()
        {
            if(!_pause && Time.time > _nextStepTime)
            {
                _nextStepTime = Time.time + _stepInterval;
                SimulateCells();
                UpdateCellsState();
            }
        }

        private void UpdateCellsState()
        {
            foreach(CellManager cellManager in _cells)
            {
                cellManager.UpdateState();
            }
        }

        private void SimulateCells()
        {
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    SimulateCell(i, j);
                }
            }
        }

        private void SimulateCell(int x, int y) {
            int neighboursCount = CountAliveNeighbours(x, y);
            CellManager cellManager = _cells[x,y].GetComponent<CellManager>();
            cellManager.ShouldBeAlive = ((neighboursCount == 3) || (cellManager.IsAlive && neighboursCount == 2));

            // Any live cell with two or three live neighbours survives.
            // Any dead cell with three live neighbours becomes a live cell.
            // All other live cells die in the next generation.Similarly, all other dead cells stay dead.
        }

        void OnDrawGizmos()
        {
            // Draw a yellow cube at the transform position
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }

        private int CountAliveNeighbours(int x, int y)
        {
            int aliveCount = 0;
            foreach(CellManager neighbour in GetNeighbours(x, y))
            {
                if (neighbour.IsAlive)
                {
                    aliveCount++;
                }
            }
            return aliveCount;
        }

        private List<CellManager> GetNeighbours(int x, int y)
        {
            List<CellManager> neighbours = new List<CellManager>();
            
            for (int i = 0; i < _neighboursIndices.GetLength(0); i++)
            {
                AddNeighbourToList(neighbours, x + _neighboursIndices[i,0], y + _neighboursIndices[i, 1]);
            }

            return neighbours;
        }

        private void AddNeighbourToList(List<CellManager> neighbours, int x, int y)
        {
            if(IsNeighbourExist(x, y))
            {
                neighbours.Add(_cells[x,y]);
            }
        }

        private bool IsNeighbourExist(int x, int y)
        {
            return !(x < 0 || x >= _columns || y < 0 || y >= _rows);
        }

        private float _nextStepTime;
        private int[,] _neighboursIndices;
        private Transform _transform;
        private CellManager[,] _cells;
    }
}

