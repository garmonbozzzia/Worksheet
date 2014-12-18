namespace HsEmulator
{
    public class Mana
    {
        public static int Max = 10;
        public int Limit { get; set; }
        public int Current { get; set; }
        //+перегрузка
        public Mana NewStep() 
        {
            var val = Limit == Max? Limit : Limit + 1;
            return new Mana { Limit = val, Current = val };
        }

        public Mana Play(Card card)
        {
            return new Mana() {Current = Current - card.Mana, Limit = Limit};
        }
    }
}