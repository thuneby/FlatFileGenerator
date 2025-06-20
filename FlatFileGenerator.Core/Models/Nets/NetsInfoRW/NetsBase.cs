using FileHelpers;

namespace FlatFileGenerator.Core.Models.Nets.NetsInfoRW
{
    public class NetsBase: TextModelBase
    {
        [FieldFixedLength(2)] public string SYSTEM_KOD;

        [FieldFixedLength(3)] public string TRANS_TYPE;
    }
}