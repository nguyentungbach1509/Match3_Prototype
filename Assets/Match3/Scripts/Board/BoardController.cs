using Match3.Scripts.Character;
using Match3.Scripts.Level;
using Match3.Subscripts;
using UnityEngine;

namespace Match3.Scripts
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField] Board board;
        [SerializeField] AnimateLayer animLayer;

        public Board Board => board;

        public void Init(LevelController level, EnemyBase enemy)
        {
            Match3Sub.LoadCellTileData();
            board.Init(level, animLayer, enemy);
        }

        public void UpdateBoard()
        {
            board.UpdateBoard();
        }
    }
}

