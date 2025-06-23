using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models;

namespace FlatFileGenerator.FileWriter.Business.Helpers
{
    internal class NetsIsModelHelper
    {
        private const string SystemCode = "IN";
        private const string ZeroDate = "00000000";
        private const string ZeroAmount = "000000000000";
        private const string ModtPbsNr = "00015733";
        public static InfoStart GetInfoStart(string bachnumber, string batchDate)
        {
            var infoStart = new InfoStart
            {
                SYSTEM_KOD = SystemCode,
                TRANS_TYPE = "000",
                MODT_PBS_TXT = "PBS",
                LEV_NR = bachnumber,
                LEV_DTO = batchDate,
                LEV_SE_NUM = "24260577",
                SYSTEM_VERS_NR = "110"
            };
            return infoStart;
        }

        public static InfoEnd GetInfoEnd(string bachnumber, string batchDate, int totalRecords, int totalSections,
            int totalLines, decimal totalAmount)
        {
            totalRecords += 2 * totalSections + 2;
            var infoEnd = new InfoEnd
            {
                SYSTEM_KOD = SystemCode,
                TRANS_TYPE = "999",
                MODT_PBS_TXT = "PBS",
                LEV_NR = bachnumber,
                LEV_DTO = batchDate,
                LEV_SE_NUM = "24260577",
                SYSTEM_VERS_NR = "110",
                LEV_TOT_REC_ANT = totalRecords.ToString("D10"),
                LEV_TOT_SECT_ANT = totalSections.ToString("D10"),
                LEV_TOT_LINIE_ANT = totalLines.ToString("D10"),
                LEV_TOT_LINIE_BLB = ((long)Math.Truncate(100 * totalAmount)).ToString("D15")
            };
            return infoEnd;
        }

        public static InfoSectionStart GetInfoSectionStart(string bachnumber, int sectionNumber, DateTime firstDate,
            DateTime bankDate, string bankAccount)
        {
            var sectionStart = new InfoSectionStart
            {
                SYSTEM_KOD = SystemCode,
                TRANS_TYPE = "001",
                LEV_NR = bachnumber,
                SECT_NR = DateTime.Today.ToString("MMdd") + sectionNumber.ToString("D4"),
                MODT_PBS_NR = ModtPbsNr,
                SPEC_NR = "0000000000",
                INDBET_SPEC_DTO = firstDate.ToString("yyyyMMdd"),
                INDBET_DTO = bankDate.ToString("yyyyMMdd"),
                OPGAVE_NR = DateTime.Today.ToString("yyyyMMdd") + "01",
                INFOTYPE_KOD = "150",
                VENDE_KOD = "00",
                OPR_OPGAVE_NR = "0000000000",
                MODT_FORDEL_REG_NR = bankAccount.Substring(0, 4),
                MODT_FORDEL_KTO_NR = bankAccount.Substring(4, bankAccount.Length - 4),
                MODT_FORDEL_PCT = "10000"
            };
            return sectionStart;
        }

        public static InfoSectionEnd GetInfoSectionEnd(string bachnumber, InfoSectionStart sectionStart, int sectionRecords,
            int sectionLines, decimal sectionAmount)
        {
            var sectionEnd = new InfoSectionEnd
            {
                SYSTEM_KOD = SystemCode,
                TRANS_TYPE = "009",
                LEV_NR = bachnumber,
                SECT_NR = sectionStart.SECT_NR,
                SECT_TOT_REC_ANT = sectionRecords.ToString("D10"),
                SECT_TOT_LINIE_ANT = sectionLines.ToString("D10"),
                SECT_TOT_LINIE_BLB = ((long)Math.Truncate(100 * sectionAmount)).ToString("D15")
            };
            return sectionEnd;
        }

        public static InfoRecord00 GetInfoRecord00(string bachnumber, InfoSectionStart sectionStart, int record00Count,
            string batchDate, ReceiptDetail transfer)
        {
            var record00 = new InfoRecord00
            {
                SYSTEM_KOD = SystemCode,
                TRANS_TYPE = "005",
                LEV_NR = bachnumber,
                SECT_NR = sectionStart.SECT_NR,
                SEKV_NR = record00Count.ToString("D10"),
                REC_TYP_ID = "00",
                //REC_ANT = "03",
                LINIE_TRANSID = "FI" + batchDate + sectionStart.SECT_NR + "00" + record00Count.ToString("D10"),
                AFS_SE_NR = transfer.Cvr,
                DL_SE_NR = "24260577",
                KUND_NR_HOS_AFS = transfer.CustomerNumber,
                MODT_AFD_NR = string.Empty,
                AFS_AFT_NR_HOS_MODT = string.Empty,
                KUND_CPR_NR = transfer.Cpr.Trim('-'),
                KUND_NR_HOS_MODT = transfer.CustomerNumber,
                INDBET_BLB = ((long)Math.Truncate(Math.Abs(transfer.Amount * 100))).ToString("D12"),
                INDBET_BLB_FRTFLT = transfer.Amount < 0 ? "-" : "+",
                PERIODE_FRA = transfer.FromDate.ToString("yyyyMMdd"),
                PERIODE_TIL = transfer.ToDate.ToString("yyyyMMdd"),
                OVERENSKOMSTNR = transfer.LaborAgreementNumber,
                SPEC_BLB = ZeroAmount,
                BLANKE = string.Empty
            };
            return record00;
        }

        public static InfoRecord01 GetInfoRecord01(string bachnumber, InfoSectionStart sectionStart, ReceiptDetail transfer,
            InfoRecord00 record00)
        {
            var record01 = new InfoRecord01
            {
                SYSTEM_KOD = SystemCode,
                TRANS_TYPE = "005",
                LEV_NR = bachnumber,
                SECT_NR = sectionStart.SECT_NR,
                SEKV_NR = record00.SEKV_NR,
                REC_TYP_ID = "01",
                KUND_START_DATO = transfer.EmployeeSalaryStartDate?.ToString("yyyyMMdd") ?? ZeroDate,
                KUNDE_NAVN = transfer.PersonFullName,
                OPL_PLIGT_KOD = "0",
                PENS_ALDER_KOD = "00",
                PENS_ALDER = "00",
                AFLOEN_FORM_START_DTO = ZeroDate,
                AFLOEN_FORM = "00",
                ANC_FRA_DATO = ZeroDate,
                FRATR_DATO = transfer.EmploymentTerminationDate?.ToString("yyyyMMdd") ?? ZeroDate,
                RESERVE1_TXT = string.Empty
            };
            return record01;
        }

        public static InfoRecord02 GetInfoRecord02(string bachnumber, InfoSectionStart sectionStart, ReceiptDetail transfer,
            InfoRecord00 record00)
        {
            var employeeSalary100 = transfer.EmployeeSalary != null ? (int)Math.Truncate(transfer.EmployeeSalary.Value * 100) : 0;
            var record02 = new InfoRecord02
            {
                SYSTEM_KOD = SystemCode,
                TRANS_TYPE = "005",
                LEV_NR = bachnumber,
                SECT_NR = sectionStart.SECT_NR,
                SEKV_NR = record00.SEKV_NR,
                REC_TYP_ID = "02",
                PENS_GIV_LOEN_START_DTO = ZeroDate,
                PENS_GIV_LOEN_BLB = employeeSalary100.ToString("D12"),
                PENS_TYPE_START_DTO = ZeroDate,
                PENS_TYPE = ((int)transfer.ReceiptType).ToString("D2"), // ToDo
                NORM_BIDRAG_START_DTO = ZeroDate,
                NORM_BIDRAG_BLB = record00.INDBET_BLB,
                ARB_ANDEL_BIDRAG_BLB = "0000",
                PENS_BIDRAG_PCT_START_DTO = ZeroDate,
                PENS_BIDRAG_PCT = transfer.TotalContributionRate == null? "0000" : ((int)Math.Truncate(transfer.TotalContributionRate.Value * 100)).ToString("D4"),
                ARB_ANDEL_PENS_BIDRAG_PCT = "0000",
                LOEN_TRIN_START_DTO = ZeroDate,
                LOEN_TRIN_NR = string.Empty,
                GRP_ANDEL_START_DTO = ZeroDate,
                GRP_ANDEL_BLB = ZeroAmount
            };
            return record02;
        }
    }
}
