using UnityEngine;

public static class ActionMaps
{
    public static class Player
    {
        public const string PlayerActionMap = "Player";
        public const string Move = "Player/Move";
        public const string Jump = "Player/Jump";
        public const string Shoot = "Player/Shoot";
    }

    public static class UI
    {
        public const string UIActionMap = "UI";
        public const string Navigate = "UI/Navigate";
        public const string Select = "UI/Select";
        public const string Back = "UI/Back";
    }
}
