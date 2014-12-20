using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media.Effects;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace HsEmulator
{
    public class Testzone
    {
        [Test]
        public void CreateInputAndOutput()
        {
            Console.WriteLine(Environment.CurrentDirectory);
            File.WriteAllText("Input.txt", "input");
            File.WriteAllText("Output.txt", "output");
        }

        public class Engine
        {
            //Action->[Effect]->[State]->[Action]

            IEnumerable<IAction> ProcessAction(IAction action, IEnumerable<ICardState> states)
            {
                IEnumerable<ICardState> newstates = action.Do()
                    .SelectMany(x => x.Apply() )
                    .SelectMany(x => x.Result());
                //todo replace oldstates
                return newstates.SelectMany(x => x.Possibilities());
            }
        }

        [Test]
        public void MyMethod()
        {
            
        }

        public class Effects : IEffects
        {
            public IEffect StartGame()
            {
                object player1 = null;
                object player2 = null;
                StartTurn(player1, player2);
            }

            public IEffect StartTurn(object player1, object player2)
            {
                throw new NotImplementedException();
            }

            public IEffect RestoreMana()
            {
                throw new NotImplementedException();
            }

            public IEffect DrawCard()
            {
                throw new NotImplementedException();
            }

            public IEffect BattleCry()
            {
                throw new NotImplementedException();
            }

            public IEffect GetDamage()
            {
                throw new NotImplementedException();
            }

            public IEffect Deathrattle()
            {
                throw new NotImplementedException();
            }

            public IEffect Buff()
            {
                throw new NotImplementedException();
            }

            public IEffect Silence()
            {
                throw new NotImplementedException();
            }

            public IEffect Win()
            {
                throw new NotImplementedException();
            }

            public IEffect Lose()
            {
                throw new NotImplementedException();
            }
        }

        public interface IEffects
        {
            IEffect StartGame();
            IEffect StartTurn(object player1, object player2);
            IEffect RestoreMana();
            IEffect DrawCard();
            IEffect BattleCry();
            IEffect GetDamage();
            IEffect Deathrattle();
            IEffect Buff();
            IEffect Silence();
            IEffect Win();
            IEffect Lose();
        }



        //EndTurn :: Action
        //PlayCard :: Action
        //Attack :: Action

        //StartGame :: Effect
        //StartTurn :: Effect
        //RestoreMana :: Effect
        //DrawCard :: Effect
        //BattleCry :: Effect
        //GetDamage :: Effect
        //Deathrattle :: Effect
        //Buff :: Effect
        //Silence :: Effect
        //Win :: Effect
        //Lose :: Effect

        //PlaceCardOnHand :: Effect
        //PlaceCardOnBoard :: Effect
        //BecomeSleep :: Effect
        //BecomeActive :: Effect
        //RemoveFromBoard :: Effect


        public class StartTurn : Testzone.IEffect
        {
            public IEnumerable<Testzone.IEffect> Apply()
            {
                foreach (var effect in StartTurnEffects())
                    yield return effect;
                StartTurnEffects();
                //DrawCard
                //[Action]
            }

            private IEnumerable<IEffect> StartTurnEffects()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Testzone.ICardState> Result()
            {
                throw new NotImplementedException();
            }

            public object Actor { get; set; }
            public object Opponent { get; set; }
        }

        public class EndTurn : IAction
        {
            public IEnumerable<IEffect> Do()
            {
                return EndTurnEffects().Concat(
                    new StartTurn
                    {
                        Actor = Opponent,
                        Opponent = Actor
                    }.ListWrap()
                    );
            }

            public object Actor { get; set; }

            public object Opponent { get; set; }

            private IEnumerable<IEffect> EndTurnEffects()
            {
                yield break;
            }
        }

        public class Start :IEffect
        {
            //Start
            //P1 get 3 card
            //P1 pick cards
            //P1 get last cards
            //P2 get 4 card and coin
            //P1 pick cards
            //P1 get last cards
            //P1 turn
            //P1 increase manalimit
            //P1 get mana
            //P1 receive possibilities
            //P1 play card on board
            //card state changed
            //...
            //P1 EndTurn

            public IEnumerable<IEffect> Apply()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<ICardState> Result()
            {
                throw new NotImplementedException();
            }
        }

        public interface IEffect
        {
            //Effect->[Effect]
            IEnumerable<IEffect> Apply();

            //Effect->[State]
            IEnumerable<ICardState> Result();
        }

        public interface ICardState
        {
            IEnumerable<IAction> Possibilities();
        }

        public interface IAction
        {
            //action->[Effect]
            IEnumerable<IEffect> Do();
        }

        public interface IActor
        {
            Action Pick(IEnumerable<Action> possibilities);
            //get possibilities
            //pick
            //get state changes
            //wait for reaction
            //possibilities: attack(c1,c2) end_turn play_card playcard(target)
            //engine: action->dimhp(c1)->dimhp(c2)->(maybe)dead->send possibilities+states
        }

        public class BattleObserver
        {
            //card type,instance
            //attack observer
            //turn observer
            //life length observer

        }
    }
}