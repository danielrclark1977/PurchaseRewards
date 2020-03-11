using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseRewards
{
    internal class PointsProcessor
    {
        internal int ProcessPoints(decimal transactionAmount) 
        {
            int pointValue = 0;
            if (transactionAmount > 100)
            {
                pointValue = (((int)transactionAmount - 100) * 2) + 50;
            }
            else if (transactionAmount > 50)
            {
                pointValue = (int)transactionAmount - 50;
            }
            return pointValue;
        }
    }
}
