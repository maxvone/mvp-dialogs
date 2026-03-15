using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "PuzzlesStaticData", menuName = "StaticData/PuzzlesStaticData")]
    public class PuzzlesStaticData : ScriptableObject
    {
        public PuzzleData[] Puzzles;
    }
}