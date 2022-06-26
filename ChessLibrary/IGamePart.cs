using System.Collections.Generic;

namespace ChessLibrary
{
    public abstract class GamePart
    {
        public abstract void Update();
        public abstract void ReadInput();
        public abstract List<ISceneItem> GetScene();
    }
}