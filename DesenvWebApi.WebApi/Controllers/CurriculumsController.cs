using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesenvWebApi.Domain;
using DesenvWebApi.Domain.Interfaces;
using DesenvWebApi.WebApi.InputModels;
using DesenvWebApi.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesenvWebApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurriculumsController : ApiControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public CurriculumsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public Task<ActionResult<IEnumerable<CurriculumViewModel>>> GetAsync()
            => ExecuteAsync<IEnumerable<CurriculumViewModel>>(async () =>
            {
                var repo = _unitOfWork.GetRepository<Curriculum>();
                var curriculums = await repo.GetQueryable()
                    .Include(c => c.Subjects)
                        .ThenInclude(s => s.Subject)
                    .OrderBy(u => u.CreatedAt)
                    .Select(u => (CurriculumViewModel) u)
                    .ToListAsync();

                if (!curriculums.Any())
                    return NoContent();

                return Ok(curriculums);
            });

        [HttpGet("{id}")]
        public Task<ActionResult<IEnumerable<CurriculumViewModel>>> GetAsync(Guid id)
            => ExecuteAsync<IEnumerable<CurriculumViewModel>>(async () =>
            {
                var repo = _unitOfWork.GetRepository<Curriculum>();
                var curriculum = await repo.GetQueryable()
                    .Include(c => c.Subjects)
                        .ThenInclude(s => s.Subject)
                    .SingleOrDefaultAsync(u => u.Id == id);

                if (curriculum is null)
                    return NotFound();

                return Ok((CurriculumViewModel) curriculum);
            });

        [HttpPost]
        public Task<ActionResult<CurriculumViewModel>> PostAsync([FromBody] CurriculumInputModel im)
            => ExecuteAsync<CurriculumViewModel>(async () =>
            {
                var repo = _unitOfWork.GetRepository<Curriculum>();

                var curriculum = new Curriculum(im.Code, im.Name, im.Description);
                repo.Add(curriculum);
                await _unitOfWork.SaveChangesAsync();

                return Ok((CurriculumViewModel) curriculum);
            });

        [HttpPut("{id}")]
        public Task<ActionResult<CurriculumViewModel>> PutAsync(Guid id, [FromBody] CurriculumInputModel im)
            => ExecuteAsync<CurriculumViewModel>(async () =>
            {
                var repo = _unitOfWork.GetRepository<Curriculum>();
                var curriculum = await repo.GetByIdAsync(id);

                if (curriculum is null)
                    return NotFound();

                if (!string.IsNullOrWhiteSpace(im.Name))
                {
                    curriculum.SetName(im.Name);
                }

                if (!string.IsNullOrWhiteSpace(im.Description))
                {
                    curriculum.SetDescription(im.Description);
                }

                repo.Update(curriculum);
                await _unitOfWork.SaveChangesAsync();

                return Ok((CurriculumViewModel) curriculum);
            });

        [HttpDelete("{id}")]
        public Task<ActionResult> DeleteAsync(Guid id)
            => ExecuteAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Curriculum>();
                var curriculum = await repo.GetByIdAsync(id);

                if (curriculum is null)
                    return NotFound();

                repo.Delete(curriculum);
                await _unitOfWork.SaveChangesAsync();

                return Ok();
            });

        [HttpPost("{id}/subject/{subjectId}")]
        public Task<ActionResult> AddSubject(Guid id, Guid subjectId)
            => ExecuteAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Curriculum>();
                var curriculum = await repo.GetQueryable()
                    .Include(c => c.Subjects)
                    .SingleOrDefaultAsync(e => e.Id == id);

                if (curriculum is null)
                    return NotFound("Curriculum not found");

                var subjectRepo = _unitOfWork.GetRepository<Subject>();
                var subject = await subjectRepo.GetByIdAsync(subjectId);

                if (subject is null)
                    return NotFound("Subject not found");

                if (curriculum.Subjects.Any(s => s.SubjectId == subjectId))
                    return BadRequest("Curriculum already contains provided subject");

                var nn = new SubjectCurriculum(subject, curriculum);
                var screpo = _unitOfWork.GetRepository<SubjectCurriculum>();
                screpo.Add(nn);
                await _unitOfWork.SaveChangesAsync();

                return Ok();
            });

        [HttpDelete("{id}/subject/{subjectId}")]
        public Task<ActionResult> DelSubject(Guid id, Guid subjectId)
            => ExecuteAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Curriculum>();
                var curriculum = await repo.GetQueryable()
                    .Include(c => c.Subjects)
                    .SingleOrDefaultAsync(e => e.Id == id);

                if (curriculum is null)
                    return NotFound("Curriculum not found");

                var subjectRepo = _unitOfWork.GetRepository<Subject>();
                var subject = await subjectRepo.GetByIdAsync(subjectId);

                if (subject is null)
                    return NotFound("Subject not found");

                var screpo = _unitOfWork.GetRepository<SubjectCurriculum>();
                var nn = await screpo.GetQueryable()
                    .SingleOrDefaultAsync(n =>
                        n.CurriculumId == id
                        && n.SubjectId == subjectId);

                if (nn is null)
                    return BadRequest("Relationship does not exist");

                screpo.Delete(nn);
                await _unitOfWork.SaveChangesAsync();

                return Ok();
            });
    }
}