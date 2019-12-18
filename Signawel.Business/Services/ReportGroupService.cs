using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Signawel.Business.Abstractions.Services;
using Signawel.Data;
using Signawel.Domain.DataResults;
using Signawel.Domain.ReportGroups;
using Signawel.Dto.ReportGroup;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Signawel.Domain.Constants;

namespace Signawel.Business.Services
{
    public class ReportGroupService : IReportGroupService
    {
        private readonly SignawelDbContext _context;
        private readonly IMapper _mapper;

        public ReportGroupService(SignawelDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Delete

        /// <inheritdoc cref="IReportGroupService.DeleteReportGroupAsync(string)"/>
        /// 
        public async Task<DataResult> DeleteReportGroupAsync(string id)
        {
            var reportGroup = await _context.ReportGroups.FindAsync(id);
            if (reportGroup == null)
            {
                return DataResult<ReportGroupResponseDto>.WithPublicError(ErrorCodes.NotFoundError,
                    "Report group not found.");
            }

            _context.ReportGroups.Remove(reportGroup);
            await _context.SaveChangesAsync();

            return DataResult.Success;
        }

        #endregion

        #region Set

        /// <inheritdoc cref="IReportGroupService.SetReportGroupAsync"/>
        public async Task<DataResult<ReportGroupResponseDto>> SetReportGroupAsync(
            ReportGroupCreationRequestDto reportGroupCreationRequest)
        {
            var reportGroup = new ReportGroup();

            foreach (CityCreationRequestDto cityDto in reportGroupCreationRequest.CityReportGroups)
            {
                var city = _mapper.Map<City>(cityDto);
                var link = new CityReportGroup
                {
                    ReportGroupId = reportGroup.Id,
                    CityId = _context.Cities.FirstOrDefault(c => c.Name == city.Name)?.Id
                };
                _context.CityReportGroups.Add(link);
            }

            foreach (UserCreationRequestDto userDto in reportGroupCreationRequest.UserReportGroups)
            {
                var link = new UserReportGroup
                {
                    ReportGroupId = reportGroup.Id,
                    UserId = userDto.Id
                };
               _context.UserReportGroups.Add(link);
            }

            foreach (EmailCreationRequestDto emailDto in reportGroupCreationRequest.EmailReportGroups)
            {
                var email = _mapper.Map<Email>(emailDto);
                var id = email.Id;
                var emailInDatabase = _context.Emails.FirstOrDefault(e => e.EmailAddress == email.EmailAddress);
                if (emailInDatabase == null)
                {
                    _context.Emails.Add(email);
                }

                if (emailInDatabase != null)
                {
                    id = emailInDatabase.Id;
                }

                var link = new EmailReportGroup
                {
                    ReportGroupId = reportGroup.Id,
                    EmailId = id
                };
                _context.EmailReportGroups.Add(link);
            }

            _context.ReportGroups.Add(reportGroup);

            var allReportGroups = await _context.ReportGroups.ToListAsync();

            if (allReportGroups.Any(repo => CheckEqual(reportGroup, repo)))
            {
                return DataResult<ReportGroupResponseDto>.WithPublicError(ErrorCodes.InvalidOperationError, "Report group already existing.");
            }

            await _context.SaveChangesAsync();

            var response = _mapper.Map<ReportGroupResponseDto>(reportGroup);

            return DataResult<ReportGroupResponseDto>.Success(response);
        }

        #endregion

        #region GetById

        /// <inheritdoc cref="IReportGroupService.GetReportGroupByIdAsync"/>
        public async Task<DataResult<ReportGroupResponseDto>> GetReportGroupByIdAsync(string id)
        {
            var reportGroup = await _context.ReportGroups.FindAsync(id);
            if (reportGroup == null)
            {
                return DataResult<ReportGroupResponseDto>.WithPublicError(ErrorCodes.NotFoundError, "Report group not found.");
            }

            var response = _mapper.Map<ReportGroupResponseDto>(reportGroup);
            return DataResult<ReportGroupResponseDto>.Success(response);
        }

        #endregion

        #region GetByParams

        /// <inheritdoc cref="IReportGroupService.GetReportGroupsAsync"/>
        public async Task<DataResult<List<ReportGroupResponseDto>>> GetReportGroupsAsync(string city, string email,string user)
        {
            if (city.Equals("null") && email.Equals("null") && user.Equals("null"))
            {
                var responseNull =
                    _mapper.Map<List<ReportGroup>, List<ReportGroupResponseDto>>(
                        await _context.ReportGroups.ToListAsync());
                return DataResult<List<ReportGroupResponseDto>>.Success(responseNull);
            }
            
            List<ReportGroup> reportGroups = await _context.ReportGroups.ToListAsync();
            var response = _mapper.Map<List<ReportGroupResponseDto>>(reportGroups);

            if (city != "null")
            {
                response = (from report in response
                                let cities =
                                    report.CityReportGroups.Where(c =>
                                        c.Name.ToLower().Contains(city.ToLower()))
                                where cities.Any()
                                select report).ToList();
            }

            if (email != "null")
            {

                response = (from report in response
                                let emails =
                                    report.EmailReportGroups.Where(e =>
                                        e.EmailAddress.ToLower().Contains(email.ToLower()))
                                where emails.Any()
                                select report).ToList();
            }

            if (user != "null")
            {

                response = (from report in response
                            let users =
                                report.UserReportGroups.Where(e =>
                                    e.UserName.ToLower().Contains(user.ToLower()))
                            where users.Any()
                            select report).ToList();
            }


            return DataResult<List<ReportGroupResponseDto>>.Success(response);
        }

        #endregion

        #region GetCities

        /// <inheritdoc cref="IReportGroupService.GetAllCitiesAsync"/>
        public async Task<DataResult<List<CityResponseDto>>> GetAllCitiesAsync()
        {
            List<City> cities = await _context.Cities.ToListAsync();
            var result = _mapper.Map<List<CityResponseDto>>(cities);

            return DataResult<List<CityResponseDto>>.Success(result);
        }

        #endregion

        #region ModifyReportGroup

        /// <inheritdoc cref="IReportGroupService.ModifyReportGroupAsync(ReportGroupResponseDto)"/>
        public async Task<DataResult<ReportGroupResponseDto>> ModifyReportGroupAsync(string id, ReportGroupCreationRequestDto reportGroup)
        {
            if (reportGroup == null)
                return DataResult<ReportGroupResponseDto>.WithPublicError(ErrorCodes.ParameterEmptyError, "The given Dto is empty.");

            ReportGroup oldReportGroup = await _context.ReportGroups.FindAsync(id);

            _context.ReportGroups.Remove(oldReportGroup);
            await _context.SaveChangesAsync();

            var newReportGroup = await this.SetReportGroupAsync(reportGroup);

            return DataResult<ReportGroupResponseDto>.WithEntityOrError(newReportGroup.Entity, ErrorCodes.ReportModificationError, "Something went wrong when updating the reportGroup.");
        }

        #endregion

        private bool CheckEqual(ReportGroup reportGroup1, ReportGroup reportGroup2)
        {
            if (reportGroup1.CityReportGroups.Count != reportGroup2.CityReportGroups.Count)
            {
                return false;
            }

            if (reportGroup1.EmailReportGroups.Count != reportGroup2.EmailReportGroups.Count)
            {
                return false;
            }

            for (int i = 0; i < reportGroup1.CityReportGroups.Count; i++)
            {
                if (reportGroup1.CityReportGroups[i].City.Name != reportGroup2.CityReportGroups[i].City.Name)
                {
                    return false;
                }
            }

            for (int i = 0; i < reportGroup1.EmailReportGroups.Count; i++)
            {
                if (reportGroup1.EmailReportGroups[i].Email.EmailAddress !=
                    reportGroup2.EmailReportGroups[i].Email.EmailAddress)
                {
                    return false;
                }
            }

            return true;
        }
    }
}