using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlatFileGenerator.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.DataAccess.Repositories
{
    public class ReceiptDetailRepository(DbContext context, ILogger<GuidRepositoryBase<ReceiptDetail>> logger) : GuidRepositoryBase<ReceiptDetail>(context, logger)
    {
    }
}
