using Xunit;
using Classes;

// THESE TESTS WERE COMPLETED USING THE '.NET CORE TEST EXPLORER' EXTENSION (see screenshots in folder)
namespace OrderClassTests
{
    public class gstTotalTest
    {
        [Theory]
        [InlineData(17, 14)]
        [InlineData(122, 15)]

        public void gstWholeNumberTest(int quantity, float price)
        {
            var dummyOrder = new Order().getGST(quantity, price);
            var value = (quantity * price) / 10;
            Assert.Equal(dummyOrder, value);
        }

        [Theory]
        [InlineData(51, 22)]
        [InlineData(0.5, 4)]
    
        public void totalLogicTest(int quantity, int price)
        {
            var dummyOrder = new Order().findTotal(quantity, price);
            var value = quantity * price;
            Assert.Equal(dummyOrder, value);
        }

        [Theory]
        [InlineData(225.3, 222.15)]
        [InlineData(123.3, 19.73)]

        public void gstFloatTest(int quantity, float price)
        {
            var order = new Order().getGST(quantity, price);
            var result = (quantity * price) / 10;
            Assert.Equal(order, result);
        }
    }
}
