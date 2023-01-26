namespace EG.Tower.Game
{
    public class HeroModel
    {
        public string Name { get; private set; }
        public Trait[] Traits { get; private set; }
        public Trait MainVirtueTrait { get; private set; }
        public Trait MainViceTrait { get; private set; }

        public HeroModel(HeroCreateModel createModel)
        {
            Name = createModel.Name;
            Traits = createModel.Traits;
            MainVirtueTrait = createModel.MainVirtueTrait;
            MainViceTrait = createModel.MainViceTrait;
        }
    }
}