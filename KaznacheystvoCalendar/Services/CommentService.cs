using AutoMapper;
using KaznacheystvoCalendar.DTO;
using KaznacheystvoCalendar.Interfaces;
using KaznacheystvoCalendar.Interfaces.ISevices;
using KaznacheystvoCalendar.Models;
using Microsoft.EntityFrameworkCore;

namespace KaznacheystvoCalendar.Services;

public class CommentService:ICommentService
{
    private readonly IGenericRepository<Comment> _commentRepository;
    private readonly IMapper _mapper;
    
    public CommentService(IGenericRepository<Comment> commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }
    
    public async Task<List<CommentDTO>> GetCommentsForEventAsync(int eventId)
    {
        var comments = await _commentRepository.GetQueryable()
            .Include(x => x.User)
            .Where(x => x.EventId == eventId)
            .ToListAsync();
        return _mapper.Map<List<CommentDTO>>(comments);
    }

    public async Task<CommentDTO> GetCommentByIdAsync(int id)
    {
        return _mapper.Map<CommentDTO>(await _commentRepository.GetByIdAsync(id));
    } 

    public async Task<CreatedCommentDTO> CreateCommentAsync(CreateCommentDTO comment)
    {
        var gg =  await _commentRepository.CreateAsync(_mapper.Map<Comment>(comment));
        return _mapper.Map<CreatedCommentDTO>(gg);
    }

    public async Task<bool> DeleteCommentAsync(int commentId)
    {
        var isExist = await _commentRepository.GetByIdAsync(commentId);
        if (isExist == null) return false;
        await _commentRepository.DeleteAsync(isExist);
        return true;
    }
}