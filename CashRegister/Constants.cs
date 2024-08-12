using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterNS
{
    public static class Constants
    {
        public const string NoChangeDue = "No change due";
    }


    public static class ParserErrors
    {
        public const string InvalidLine = "Invalid line";
        public const string InvalidAmountOwed = "Invalid amount owed";
        public const string InvalidAmountPaid = "Invalid amount paid";
    }

    public static class  CashRegisterErrors
    {
        public const string NegativeAmount = "Amounts must be positive"; 
        public const string AmountPaidLessThanOwed = "Amount paid is less than amount owed";
    }
}
