using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlatFileGenerator.Core.Models;
using FlatFileGenerator.DataAccess.Interfaces;
using FlatFileGenerator.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.DataAccess.Repositories
{
    public class ReceiptDetailRepository(FlatFileContext context, ILogger<GuidRepositoryBase<ReceiptDetail>> logger) : GuidRepositoryBase<ReceiptDetail>(context, logger), IGuidRepository<ReceiptDetail>
    {
    }
}
