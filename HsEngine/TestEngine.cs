using System.Collections.Generic;
using System.Linq;

namespace HsEmulator
{
    public class TestEngine
    {
        //Action->[Effect]->[State]->[Action]

        IEnumerable<IAction> ProcessAction(IAction action, IEnumerable<ICardState> states)
        {
            IEnumerable<ICardState> newstates = action.Do()
                .SelectMany(x => x.Apply())
                .SelectMany(x => x.Result())
                ;
            //todo replace oldstates
            return newstates.SelectMany(x => x.Possibilities());
        }
    }
}