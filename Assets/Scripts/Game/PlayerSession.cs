using EG.Tower.Heroes;

namespace EG.Tower.Game
{
    public class PlayerSession
    {
        private HeroModel _heroModel;
        public HeroModel HeroModel
        {
            get
            {
                if (_heroModel == null)
                {
                    var heroModel = new HeroModel(_defaultHeroSetup);
                    SetHeroModel(heroModel);
                }

                return _heroModel;
            }
        }

        private HeroSetupData _defaultHeroSetup;

        public PlayerSession(HeroSetupData defaultSetup)
        {
            _defaultHeroSetup = defaultSetup;
        }

        public void SetHeroModel(HeroModel heroModel)
        {
            _heroModel = heroModel;
        }
    }
}