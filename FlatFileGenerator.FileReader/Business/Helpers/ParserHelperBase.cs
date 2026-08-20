using FlatFileGenerator.Core.Models;
using System.Text;

namespace FlatFileGenerator.FileReader.Business.Helpers
{
    public abstract class ParserHelperBase
    {
        public static Encoding GetEncoding(DocumentType documentType)
        {
            return documentType switch
            {
                DocumentType.NetsIs => Encoding.GetEncoding(28591),
                _ => Encoding.UTF8
            };
        }
    }
}
