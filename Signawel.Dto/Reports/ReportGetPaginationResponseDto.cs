using System.Collections.Generic;

namespace Signawel.Dto.Reports
{
    public class ReportGetPaginationResponseDto
    {
        public int Total { get; set; }

        public IList<ReportResponseDto> Reports { get; set; }
    }
}
