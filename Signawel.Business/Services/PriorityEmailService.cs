using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Signawel.Business.Abstractions.Services;
using Signawel.Data;
using Signawel.Domain;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Dto.PriorityEmail;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Signawel.Business.Services
{
    public class PriorityEmailService : IPriorityEmailService
    {
        private readonly SignawelDbContext _context;
        private readonly IMapper _mapper;

        public PriorityEmailService(SignawelDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CheckPriorityEmailAsync(string emailSuffix)
        {
            return await _context.PriorityEmails.AnyAsync(pe => pe.EmailSuffix.Equals(emailSuffix));
        }

        public IQueryable<PriorityEmailResponseDto> GetPriorityEmails()
        {
            return _mapper.ProjectTo<PriorityEmailResponseDto>(_context.PriorityEmails.AsQueryable());
        }

        public async Task<DataResult<PriorityEmailResponseDto>> AddPriorityEmailAsync(PriorityEmailCreationRequestDto dto)
        {
            if(string.IsNullOrEmpty(dto.EmailSuffix) || dto.EmailSuffix.Contains("@") || !dto.EmailSuffix.Contains("."))
                return DataResult<PriorityEmailResponseDto>.WithPublicError(ErrorCodes.PriorityEmailCreationError, "Invalid PriorityEmailCreationRequestDto");

            if(await CheckPriorityEmailAsync(dto.EmailSuffix))
                return DataResult<PriorityEmailResponseDto>.WithPublicError(ErrorCodes.PriorityEmailCreationError, $"{dto.EmailSuffix} is already a priority email.");

            var priorityEmail = _mapper.Map<PriorityEmail>(dto);

            try
            {
                await _context.PriorityEmails.AddAsync(priorityEmail);
                await _context.SaveChangesAsync();

                return DataResult<PriorityEmailResponseDto>.Success(_mapper.Map<PriorityEmailResponseDto>(priorityEmail));
            } catch (Exception)
            {
                return DataResult<PriorityEmailResponseDto>.WithPublicError(ErrorCodes.PriorityEmailCreationError, "Failed to add priority email");
            }
        }

        public async Task<DataResult> RemovePriorityEmailAsync(string id)
        {
            var entity = await _context.PriorityEmails.FirstOrDefaultAsync();

            if (entity == null)
                return DataResult.Success;

            try
            {
                _context.PriorityEmails.Remove(entity);
                await _context.SaveChangesAsync();

                return DataResult.Success;
            } catch (Exception)
            {
                return DataResult.WithPublicError(ErrorCodes.PriorityEmailDeletionError, "Failed to delete priority email.");
            }
        }
    }
}