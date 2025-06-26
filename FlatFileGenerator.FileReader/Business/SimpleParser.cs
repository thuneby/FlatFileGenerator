using FlatFileGenerator.Core.Models;
using FlatFileGenerator.FileReader.Business.Helpers;
using FlatFileGenerator.FileReader.Business.Mappers;
using FlatFileGenerator.FileReader.Interfaces;

namespace FlatFileGenerator.FileReader.Business
{
    public class SimpleParser<T1, T2>(FlatParserHelperBase<T1> flatParserHelper, 
            TextMapperBase<T1, T2> textMapper,
            GuidMapperBase<T2, ReceiptDetail> recordMapper) : IAsyncParser
        where T1 : TextModelBase
        where T2 : GuidModelBase
    {

        public async Task<IEnumerable<ReceiptDetail>> ParseAsync(Stream payload, DocumentType documentType)
        {
            var (textRecords, errors) = flatParserHelper.GetRecordsFromPayload(payload, documentType);
            var recordList = textRecords?.ToList() ?? [];
            var errorList = errors.ToList();
            if (errorList.Any())
            {
                var exception = new Exception(errors.FirstOrDefault());
                throw exception;
            }
            if (!recordList.Any())
            {
                var exception = new Exception("Fejl - Formatet er ikke IP Standardformat!");
                throw exception;
            }

            var modelRecords = recordList.Select(textMapper.Map).ToList();
            var receiptDetails = modelRecords.Select(recordMapper.Map).ToList();
            return receiptDetails;
        }
    }
}
