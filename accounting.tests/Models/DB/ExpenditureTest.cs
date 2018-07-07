using System;
using System.Collections.Generic;
using Accounting.WS.Models.DB;
using Xunit;

namespace Accounting.WS.Test.Models.DB
{
    public class ExpenditureTest
    {
        [Fact]
        public void ExpenditureAllTest()
        {
            var expenditure = new Expenditure();
            expenditure.Amount = 500;
            Assert.Equal(500, expenditure.GetTotal());
            //add refund
            expenditure.ReFunds = new List<ExpenditureReFund>();
            var refund1 = GetExpenditureReFund1();
            expenditure.ReFunds.Add(refund1);
            Assert.Equal(400, expenditure.GetTotal());
            //change amount
            expenditure.Amount = 1000;
            Assert.Equal(900, expenditure.GetTotal());
            //add more refund
            var refund2 = GetExpenditureReFund2();
            expenditure.ReFunds.Add(refund2);
            Assert.Equal(850, expenditure.GetTotal());
        }

        private ExpenditureReFund GetExpenditureReFund1()
        {
            return  new ExpenditureReFund(){
                        Amount = 100
                    };
        }

        private ExpenditureReFund GetExpenditureReFund2()
        {
            return  new ExpenditureReFund(){
                        Amount = 50
                    };
        }
    }
}