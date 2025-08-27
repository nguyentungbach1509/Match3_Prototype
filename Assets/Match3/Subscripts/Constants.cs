using UnityEngine;

namespace Match3.Subscripts
{
    public static class Constants
    {
        #region Enemies
        public static readonly string Normal_Enemy = "Normal_Enemy";
        #endregion
    }

    public enum ECellType
    {
        Sword, Shield, Skull, Health, Fire, Ice, Cloak
    }


    public enum EStatusDmgType
    {
        None, Tick, Once
    }

    public enum EDirection
    {
        Up, Down, Left, Right
    }
}

