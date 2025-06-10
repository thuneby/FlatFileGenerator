using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileGenerator.Core.Models.Nets.NetsInfo
{
    public class InfoRecordChild : InfoRecordBase
    {
        public Guid? InfoRecord00Id { get; set; }
        public InfoRecord00 InfoRecord00 { get; set; }
    }
}
