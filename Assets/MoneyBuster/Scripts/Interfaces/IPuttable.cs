using DG.Tweening;
using MoneyBuster.Gameplay;

namespace MoneyBuster.Interfaces
{
    public interface IPuttable
    {
        Tweener Put(Money money);
    }
}