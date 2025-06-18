using FlatFileGenerator.Core.Models;
using FlatFileGenerator.DataAccess.Repositories;
using FlatFileGenerator.Test.Common;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FlatFileGenerator.Test.RepositoryTests
{
    public class ReceiptDetailRepositoryTests : TestBase
    {
        private readonly ReceiptDetailRepository _repository;

        private readonly ReceiptDetail _receiptDetail = new ReceiptDetail
        {
            Amount = 123.45M,
            Cpr = "1234567890",
            FromDate = new DateTime(2025, 1, 1),
            ToDate = new DateTime(2025, 1, 31),
            ReceivedDate = new DateTime(2025, 2, 1),
            LaborAgreementNumber = "123456",
            Cvr = "12345678",
            PersonFullName = "Test Person",
            PaymentReference = "PAY123456",
            PaymentDate = new DateTime(2025, 2, 2),
            ReceiptType = ReceiptType.Payment,
            PolicyNumber = 123456

        };

        public ReceiptDetailRepositoryTests()
        {
            // Initialize the test database and other setup if needed
            _repository = new ReceiptDetailRepository(Context, LoggerFactory.CreateLogger<ReceiptDetailRepository>());
        }

        [Fact]
        public void AddReceiptDetail_ShouldAddReceiptDetail()
        {
            // Arrange

            // Act
            _repository.Add(_receiptDetail);

            // Assert
            var addedReceiptDetail = _repository.Get(_receiptDetail.Id);
            Assert.NotNull(addedReceiptDetail);
            Assert.Equal(_receiptDetail.Amount, addedReceiptDetail.Amount);
            Assert.Equal(_receiptDetail.Cpr, addedReceiptDetail.Cpr);
            Assert.Equal(_receiptDetail.FromDate, addedReceiptDetail.FromDate);
            Assert.Equal(_receiptDetail.ToDate, addedReceiptDetail.ToDate);

        }

        [Fact]
        public async Task GetReceiptDetailList()
        {
            // Arrange
            _repository.Add(_receiptDetail);

            // Act
            var receiptDetails = await _repository.GetList(10);

            // Assert
            Assert.NotEmpty(receiptDetails);
            Assert.Single(receiptDetails);
            Assert.Contains(receiptDetails, rd => rd.Id == _receiptDetail.Id);
            Assert.Equal(_receiptDetail.Amount, receiptDetails.First(rd => rd.Id == _receiptDetail.Id).Amount);
        }

        [Fact]
        public async Task AddReceiptDetailList()
        {
            // Arrange
            const string testData = "receiptdetails_small.json";
            var blob = await TestUtil.ReadFileAsync(testData);
            using MemoryStream ms = new MemoryStream(blob);
            var receiptDetails = await JsonSerializer.DeserializeAsync<ReceiptDetail[]>(ms);

            // Act
            if (receiptDetails != null)
            {
                await _repository.AddRange(receiptDetails);
            }

            // Assert
            var allReceiptDetails = (await _repository.GetList()).ToList();
            Assert.NotEmpty(allReceiptDetails);
            Assert.Equal(receiptDetails?.Length, allReceiptDetails.Count);
        }
    }
}
