using FileHelpers;
// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfoRW
{
    [FixedLengthRecord(FixedMode.AllowVariableLength)]
    public class InfoRecordFixed1116: InfoRecordFixedBase
    {
                [FieldFixedLength(2)]
        [FieldOptional]
        public string LOENTYPE1;

        [FieldFixedLength(2)]
        [FieldOptional]
        public string LOENTRIN1;

        [FieldFixedLength(10)]
        [FieldOptional]
        public string PENSIONSGIVENDE_LOEN1;

        [FieldFixedLength(2)]
        [FieldOptional]
        public string LOENTYPE2;

        [FieldFixedLength(2)]
        [FieldOptional]
        public string LOENTRIN2;
        
        [FieldFixedLength(10)]
        [FieldOptional]
        public string PENSIONSGIVENDE_LOEN2;
        
        [FieldFixedLength(2)]
        [FieldOptional]
        public string LOENTYPE3;
        
        [FieldFixedLength(2)]
        [FieldOptional]
        public string LOENTRIN3;
        
        [FieldFixedLength(10)]
        [FieldOptional]
        public string PENSIONSGIVENDE_LOEN3;
        
        [FieldFixedLength(2)]
        [FieldOptional]
        public string LOENTYPE4;
        
        [FieldFixedLength(2)]
        [FieldOptional]
        public string LOENTRIN4;
        
        [FieldFixedLength(10)]
        [FieldOptional]
        public string PENSIONSGIVENDE_LOEN4;

        [FieldFixedLength(2)]
        [FieldOptional]
        public string LOENTYPE5;
        
        [FieldFixedLength(2)]
        [FieldOptional]
        public string LOENTRIN5;
        
        [FieldFixedLength(10)]
        [FieldOptional]
        public string PENSIONSGIVENDE_LOEN5;

        [FieldFixedLength(2)]
        [FieldOptional]
        public string LOENTYPE6;
        
        [FieldFixedLength(2)]
        [FieldOptional]
        public string LOENTRIN6;
        
        [FieldFixedLength(10)]
        [FieldOptional]
        public string PENSIONSGIVENDE_LOEN6;
        
        [FieldFixedLength(2)]
        [FieldOptional]
        public string LOENTYPE7;
        
        [FieldFixedLength(2)]
        [FieldOptional]
        public string LOENTRIN7;
        
        [FieldFixedLength(10)]
        [FieldOptional]
        public string PENSIONSGIVENDE_LOEN7;
    }
}
