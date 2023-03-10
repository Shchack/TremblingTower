using EG.Tower.Heroes;
using System.Linq;

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

        public HeroModel[] Team { get; private set; }

        private HeroSetupData _defaultHeroSetup;

        public PlayerSession(HeroSetupData defaultSetup, HeroSetupData[] teamHeroes)
        {
            _defaultHeroSetup = defaultSetup;

            Team = new HeroModel[teamHeroes.Length + 1];
            Team[0] = HeroModel;

            for (int i = 0; i < teamHeroes.Length; i++)
            {
                Team[i + 1] = new HeroModel(teamHeroes[i]);
            }
        }

        public void SetHeroModel(HeroModel heroModel)
        {
            _heroModel = heroModel;
            Team[0] = heroModel;
        }
    }
}