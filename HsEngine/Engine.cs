using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace HsEngine
{
    public class Engine
    {
        public static Engine Instance(Player p1, Player p2)
        {
            p1.Opponent = p2;
            p2.Opponent = p1;
            return new Engine() {Player1 = p1, Player2 = p2};
        }

        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public IEffect StartGame()
        {
            var apply = new Func<IEffect, IEnumerable<IEffect>>(effect =>
                //Turn(player1, player2).Cons( Turn(player2, player1).ListWrap())
                //DrawCard(Player1)
                1.To(3).Select(_ => Player1.DrawCard())
                    .Next(1.To(4).Select(_ => Player2.DrawCard()))
                    .Next(1.To().Select(_ => NextRound()))
                    .SelectMany(x => x)
                    //.Next(AddCoin(Player2))
                    .SelectMany(x => x.Apply())
                    .TakeWhileIncluding(x => x.Type != "GameOver"));
            return new Effect(apply) { Type = "StartGame" };
        }

        public IEffect NextRound()
        {
            var apply = new Func<IEffect, IEnumerable<IEffect>>(effect =>
                Turn(Player1)
                    .Next(Turn(Player2))
                    .SelectMany(x => x.Apply())
                );
            return new Effect(apply) { Type = "NextRound" };
        }

        public IEffect Turn(Player player)
        {
            var apply = new Func<IEffect, IEnumerable<IEffect>>(effect =>
                RestoreMana(player)
                    .Next(player.DrawCard())
                    .Concat(player.Actions())
                    .SelectMany(x => x.Apply()));
            return new Effect(apply) { Type = "Turn"};
        }

        public IEffect RestoreMana(Player player)
        {
            return new Effect { Type = "RestoreMana" };
        }
    }
}