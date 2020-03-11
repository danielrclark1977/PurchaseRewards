using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PurchaseRewards
{
    internal class TransactionProcessor
    {
        public void processTransaction(string transactionLine, List<Transaction> transactionList)
        {
            var transactionStringArray = transactionLine.Split(',');
            if (transactionStringArray != null && transactionStringArray.Length == 3)
            {
                var transaction = new Transaction();
                int parsedCustomerId = 0;
                transaction.CustomerId = int.TryParse(transactionStringArray[0], out parsedCustomerId) ? parsedCustomerId : parsedCustomerId;
                var transactionDate = DateTime.MinValue;
                transaction.TransactionDate = DateTime.TryParse(transactionStringArray[1], out transactionDate) ? transactionDate : transactionDate;
                decimal parsedTransactionAmount = 0;
                string transactionAmountStringNoDollarSign = transactionStringArray[2].Contains('$') ? transactionStringArray[2].Replace('$', ' ') : transactionStringArray[2];
                transaction.TransactionAmount = decimal.TryParse(transactionAmountStringNoDollarSign, out parsedTransactionAmount) ? parsedTransactionAmount : parsedTransactionAmount;
                PointsProcessor pointsProcessor = new PointsProcessor();
                transaction.TransactionPoints = pointsProcessor.ProcessPoints(transaction.TransactionAmount);
                transactionList.Add(transaction);
            }
        }

        public List<Transaction> processThreeMonthsOfTransactions(List<Transaction> transactionList,DateTime startDate) 
        {
            List<Transaction> threeMonthsOfTransactions = new List<Transaction>();
            int startMonth = startDate.Month;
            //what happens on year change
            if (startMonth > 10)
            {
                int endMonth = startDate.Month + 3 - 12;
                threeMonthsOfTransactions = transactionList.OrderBy(x => x.TransactionDate).Where(x => x.TransactionDate.Month >= startDate.Month && x.TransactionDate.Month <= 12).ToList();
                threeMonthsOfTransactions.AddRange(transactionList.OrderBy(x => x.TransactionDate).Where(x => (x.TransactionDate.Year == startDate.Year + 1) && (x.TransactionDate.Month < endMonth)).ToList());
            }
            else
            {
                threeMonthsOfTransactions = transactionList.Where(x => x.TransactionDate.Month >= startMonth && x.TransactionDate.Month < startMonth + 3).ToList();
            }
            return threeMonthsOfTransactions;
            
        }
    }
}
