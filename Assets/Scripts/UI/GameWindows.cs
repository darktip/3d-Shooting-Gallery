using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "Game Windows", menuName = "Game Windows", order = 50)]
    public class GameWindows : ScriptableObject
    {
        public WindowBase mainWindow;
        public WindowBase highScoreWindow;
        public WindowBase winScreen;
        public WindowBase looseScreen;
    }
}
