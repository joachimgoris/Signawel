using System.Collections.Generic;

namespace Signawel.Dto.Reports
{
    public class ReportGetPaginationDto
    {
        public int Total { get; set; }

        public IList<ReportResponseDto> Reports { get; set; }
    }
}
