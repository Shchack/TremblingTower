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
                    var heroModel = new HeroModel("Hero", _defaultTraits);
                    SetHeroModel(heroModel);
                }

                return _heroModel;
            }
        }

        private HeroSetupData _defaultTraits;

        public PlayerSession(HeroSetupData defaultTraits)
        {
            _defaultTraits = defaultTraits;
        }

        public void SetHeroModel(HeroModel heroModel)
        {
            _heroModel = heroModel;
        }
    }
}