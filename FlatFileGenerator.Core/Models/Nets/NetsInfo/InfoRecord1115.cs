using System.ComponentModel.DataAnnotations;

// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfo
{
    public class InfoRecord1115: InfoRecordChild
    {
        [StringLength(2)]
        public string LOENTYPE1 {get; set; }

        [StringLength(2)]
        public string LOENTRIN1 {get; set; }

        [StringLength(10)]
        public string PENSIONSGIVENDE_LOEN1 {get; set; }
        
        [StringLength(2)]
        public string LOENTYPE2 {get; set; }

        [StringLength(2)]
        public string LOENTRIN2 {get; set; }

        [StringLength(10)]
        public string PENSIONSGIVENDE_LOEN2 {get; set; }

        [StringLength(2)]
        public string LOENTYPE3 {get; set; }

        [StringLength(2)]
        public string LOENTRIN3 {get; set; }

        [StringLength(10)]
        public string PENSIONSGIVENDE_LOEN3 {get; set; }

        [StringLength(2)]
        public string LOENTYPE4 {get; set; }

        [StringLength(2)]
        public string LOENTRIN4 {get; set; }

        [StringLength(10)]
        public string PENSIONSGIVENDE_LOEN4 {get; set; }

        [StringLength(2)]
        public string LOENTYPE5 {get; set; }

        [StringLength(2)]
        public string LOENTRIN5 {get; set; }

        [StringLength(10)]
        public string PENSIONSGIVENDE_LOEN5 {get; set; }

        [StringLength(2)]
        public string LOENTYPE6 {get; set; }

        [StringLength(2)]
        public string LOENTRIN6 {get; set; }

        [StringLength(10)]
        public string PENSIONSGIVENDE_LOEN6 {get; set; }

        [StringLength(2)]
        public string LOENTYPE7 {get; set; }

        [StringLength(2)]
        public string LOENTRIN7 {get; set; }

        [StringLength(10)]
        public string PENSIONSGIVENDE_LOEN7 {get; set; }
    }
}
