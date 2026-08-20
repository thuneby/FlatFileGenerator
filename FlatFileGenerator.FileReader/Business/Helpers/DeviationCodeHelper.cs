using FlatFileGenerator.Core.Models;


namespace FlatFileGenerator.FileReader.Business.Helpers
{
    public static class DeviationCodeHelper
    {
        public static DeviationCode GetNetsIsDeviationcode(string codeString)
        {
            var success = int.TryParse(codeString, out var netsCode);
            if (!success)
                return DeviationCode.None;
            return netsCode switch
            {
                0 => DeviationCode.NewPolicy,
                1 => DeviationCode.EmploymentTermination,
                2 => DeviationCode.OtherLeaveNoPension,
                3 => DeviationCode.MaternityNoPension,
                4 => DeviationCode.MilitaryService,
                5 => DeviationCode.MissingEducation,
                6 => DeviationCode.Suspension,
                7 => DeviationCode.MissingEducationWithPlacementBonus,
                8 => DeviationCode.Reduction,
                9 => DeviationCode.Other,
                _ => DeviationCode.None
            };
        }
    }
}
