using System.Collections.Generic;

namespace HomeControl.Finances.Domain.SeedWork.Transaction
{
    public interface ITransactionHeaderWithItens<in TItem> : ITransactionHeader
        where TItem : class, ITransaction
    {
        void AddItensList(IEnumerable<TItem> purchaseItens);
        void AddItem(TItem item);
        void RemoveItem(int index);
        void UpdateItem(int index, TItem item);
    }
}
