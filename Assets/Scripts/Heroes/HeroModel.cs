namespace EG.Tower.Game
{
    public class HeroModel
    {
        public string Name { get; private set; }
        public Trait[] Traits { get; private set; }
        public Trait MainVirtueTrait { get; private set; }
        public Trait MainViceTrait { get; private set; }

        public HeroModel()
        {
            Name = string.Empty;
            Traits = new Trait[0];
            MainVirtueTrait = null;
            MainViceTrait = null;
        }

        public HeroModel(HeroCreateModel createModel)
        {
            Name = createModel.Name;
            Traits = createModel.Traits;
            MainVirtueTrait = createModel.MainVirtueTrait;
            MainViceTrait = createModel.MainViceTrait;
        }
    }
}