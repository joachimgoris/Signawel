using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Signawel.Business.Abstractions.Services;
using Signawel.Data;
using Signawel.Domain;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Dto.BlacklistEmail;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Signawel.Business.Services
{
    public class BlacklistEmailService : IBlacklistEmailService
    {
        private readonly SignawelDbContext _context;
        private readonly IMapper _mapper;

        public BlacklistEmailService(SignawelDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <inheritdoc cref="IBlacklistEmailService.AddBlacklistEmailAsync(BlacklistEmailCreationRequestDto)"/>
        public async Task<DataResult<BlacklistEmailResponseDto>> AddBlacklistEmailAsync(BlacklistEmailCreationRequestDto dto)
        {
            if (string.IsNullOrEmpty(dto.Email))
            {
                return DataResult<BlacklistEmailResponseDto>.WithPublicError(ErrorCodes.BlacklistEmailCreationError, "Invalid BlacklistEmailCreationRequestDto");
            }

            if ((await CheckBlacklistEmailAsync(dto.Email)).Succeeded)
            {
                return DataResult<BlacklistEmailResponseDto>.WithPublicError(ErrorCodes.BlacklistEmailCreationError, $"{dto.Email} is already a blacklisted email");
            }

            var blacklistEmail = _mapper.Map<BlacklistEmail>(dto);

            try
            {
                await _context.BlacklistEmails.AddAsync(blacklistEmail);
                await _context.SaveChangesAsync();

                return DataResult<BlacklistEmailResponseDto>.Success(_mapper.Map<BlacklistEmailResponseDto>(blacklistEmail));
            }
            catch (Exception)
            {
                return DataResult<BlacklistEmailResponseDto>.WithPublicError(ErrorCodes.BlacklistEmailCreationError, "Failed to add blacklist email.");
            }
        }

        /// <inheritdoc cref="IBlacklistEmailService.CheckBlacklistEmailAsync(string)"/>
        public async Task<DataResult> CheckBlacklistEmailAsync(string email)
        {
            if (await _context.BlacklistEmails.AnyAsync(be => be.Email.Equals(email)))
            {
                return DataResult.Success;
            }
            
            return DataResult.WithPublicError(ErrorCodes.NotFoundError, $"{email} is not a blacklisted email.");
        }

        /// <inheritdoc cref="IBlacklistEmailService.GetBlacklistEmails"/>
        public DataResult<IQueryable<BlacklistEmailResponseDto>> GetBlacklistEmails()
        {
            return DataResult<IQueryable<BlacklistEmailResponseDto>>.Success(_mapper.ProjectTo<BlacklistEmailResponseDto>(_context.BlacklistEmails.AsQueryable()));

        }

        /// <inheritdoc cref="IBlacklistEmailService.RemoveBlacklistEmailAsync(string)"/>
        public async Task<DataResult> RemoveBlacklistEmailAsync(string id)
        {
            var entity = await _context.BlacklistEmails.FirstOrDefaultAsync();

            if (entity == null)
            {
                return DataResult.Success;
            }

            try
            {
                _context.BlacklistEmails.Remove(entity);
                await _context.SaveChangesAsync();

                return DataResult.Success;
            }
            catch(Exception)
            {
                return DataResult.WithPublicError(ErrorCodes.BlacklistEmailDeletionError, "Failed to delete blackmail email.");
            }
        }
    }
}
