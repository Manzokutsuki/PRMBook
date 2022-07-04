using BookCore.Dtos.Receiver;

namespace BookCore.Dtos.Order
{
    public class OrderDetailDto
    {
        public int OrderDetailId { get; set; }
        public string? OrderId { get; set; }
        public string? OrderDate { get; set; }

        public ReceiverDetailDto receiverDetail { get; set; }

        public List<OrderBookDetailDto> bookDetails { get; set; }

        public OrderDetailDto()
        {}

        public OrderDetailDto(int orderDetailId, string? orderId, 
            string? orderDate, ReceiverDetailDto 
            receiverDetail, List<OrderBookDetailDto> bookDetails)
        {
            OrderDetailId = orderDetailId;
            OrderId = orderId;
            OrderDate = orderDate;
            this.receiverDetail = receiverDetail;
            this.bookDetails = bookDetails;
        }
    }
}
