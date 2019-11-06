using AutoMapper;
using Microsoft.Extensions.Logging;
using Signawel.Business.Abstractions.Services;
using Signawel.Data;
using Signawel.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Signawel.Business.Services
{
    public class ReportService : IReportService
    {
        private readonly IMapper _mapper;
        private readonly SignawelDbContext _context;
        private readonly ILogger<ReportService> _logger;

        public ReportService(SignawelDbContext context, ILogger<ReportService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Report> AddReportAsync(Report report)
        {
            if (report != null)
                await _context.Reports.AddAsync(report);

            await _context.SaveChangesAsync();

            return report;
        }

        public async Task DeleteReportAsync(string reportId)
        {
            Report report = null;
            if (!string.IsNullOrEmpty(reportId))
                report = await _context.Reports.FindAsync(reportId);

            if (report != null)
                _context.Remove(report);

            await _context.SaveChangesAsync();
        }

        public IQueryable<Report> GetAllReports()
        {
            return _context.Reports;
        }

        public async Task<Report> GetReportAsync(string reportId)
        {
            Report report = null;
            if (!string.IsNullOrEmpty(reportId))
                report = await _context.Reports.FindAsync(reportId);

            return report;
        }

        public async Task<Report> ModifyReportAsync(Report report)
        {
            Report oldReport = await _context.Reports.FindAsync(report.Id);
            oldReport = _mapper.Map(report, oldReport);

            await _context.SaveChangesAsync();

            return oldReport;
        }
    }
}
