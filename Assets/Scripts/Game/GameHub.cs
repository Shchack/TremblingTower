using EG.Tower.Utils;

namespace EG.Tower.Game
{
    public class GameHub : Singleton<GameHub>
    {
        private PlayerSession _session;
        public PlayerSession Session
        {
            get
            {
                if (_session == null)
                {
                    _session = new PlayerSession();
                }

                return _session;
            }
        }
    }
}
