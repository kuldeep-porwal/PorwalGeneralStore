using System;
using System.Collections.Generic;

namespace PorwalGeneralStore.EdmxModel
{
    public partial class OrderPaymentDetail
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }
        public string PaymentMode { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TransactionStatus { get; set; }
        public string TransactionNote { get; set; }
    }
}
