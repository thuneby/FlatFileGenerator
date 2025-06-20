using System.Text;
using FileHelpers;
using FlatFileGenerator.Core.Models;

namespace FlatFileGenerator.FileReader.Business.Helpers
{
    public abstract class FlatParserHelperBase<T> where T : TextModelBase
    {
        public (IEnumerable<T>, IEnumerable<string>) GetRecordsFromPayload(Stream payload, DocumentType documentType)
        {
            var errors = new HashSet<string>();
            var engine = new FileHelperEngine<T>
            {
                ErrorManager =
                {
                    ErrorMode = ErrorMode.SaveAndContinue
                }
            };
            var records = engine.ReadStream(new StreamReader(payload, GetEncoding(documentType)));
            if (engine.ErrorManager.HasErrors)
                foreach (var error in engine.ErrorManager.Errors)
                {
                    errors.Add("Error in line: " + error.LineNumber + " - " + error.ExceptionInfo.Message);
                    //errors.Add(error.ExceptionInfo.Message);
                }
            var errorList = errors.ToList();
            return (records, errorList);
        }

        private static Encoding GetEncoding(DocumentType documentType)
        {
            return documentType switch
            {
                _ => Encoding.UTF8,
            };
        }
    }
}
