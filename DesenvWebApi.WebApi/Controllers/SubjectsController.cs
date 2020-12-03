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
    public class SubjectsController : ApiControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public SubjectsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public Task<ActionResult<IEnumerable<SubjectViewModel>>> GetAsync()
            => ExecuteAsync<IEnumerable<SubjectViewModel>>(async () =>
            {
                var repo = _unitOfWork.GetRepository<Subject>();
                var subjects = await repo.GetQueryable()
                    .OrderBy(u => u.CreatedAt)
                    .Select(u => (SubjectViewModel) u)
                    .ToListAsync();

                if (!subjects.Any())
                    return NoContent();

                return Ok(subjects);
            });

        [HttpGet("{id}")]
        public Task<ActionResult<IEnumerable<SubjectViewModel>>> GetAsync(Guid id)
            => ExecuteAsync<IEnumerable<SubjectViewModel>>(async () =>
            {
                var repo = _unitOfWork.GetRepository<Subject>();
                var subject = await repo.GetQueryable()
                    .Include(c => c.Curriculums)
                        .ThenInclude(s => s.Curriculum)
                    .SingleOrDefaultAsync(u => u.Id == id);

                if (subject is null)
                    return NotFound();

                return Ok((SubjectViewModel) subject);
            });

        [HttpPost]
        public Task<ActionResult<SubjectViewModel>> PostAsync([FromBody] SubjectInputModel im)
            => ExecuteAsync<SubjectViewModel>(async () =>
            {
                var repo = _unitOfWork.GetRepository<Subject>();

                var subject = new Subject(im.Code, im.Name, im.Description);
                repo.Add(subject);
                await _unitOfWork.SaveChangesAsync();

                return Ok((SubjectViewModel) subject);
            });

        [HttpPut("{id}")]
        public Task<ActionResult<SubjectViewModel>> PutAsync(Guid id, [FromBody] SubjectInputModel im)
            => ExecuteAsync<SubjectViewModel>(async () =>
            {
                var repo = _unitOfWork.GetRepository<Subject>();
                var subject = await repo.GetByIdAsync(id);

                if (subject is null)
                    return NotFound();

                if (!string.IsNullOrWhiteSpace(im.Name))
                {
                    subject.SetName(im.Name);
                }

                if (!string.IsNullOrWhiteSpace(im.Description))
                {
                    subject.SetDescription(im.Description);
                }

                repo.Update(subject);
                await _unitOfWork.SaveChangesAsync();

                return Ok((SubjectViewModel) subject);
            });

        [HttpDelete("{id}")]
        public Task<ActionResult> DeleteAsync(Guid id)
            => ExecuteAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Subject>();
                var subject = await repo.GetByIdAsync(id);

                if (subject is null)
                    return NotFound();

                repo.Delete(subject);
                await _unitOfWork.SaveChangesAsync();

                return Ok();
            });

        [HttpPost("{id}/curriculum/{curriculumId}")]
        public Task<ActionResult> AddSubject(Guid id, Guid curriculumId)
            => ExecuteAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Subject>();
                var subject = await repo.GetQueryable()
                    .Include(s => s.Curriculums)
                    .SingleOrDefaultAsync(e => e.Id == id);

                if (subject is null)
                    return NotFound("Subject not found");

                var curriculumRepo = _unitOfWork.GetRepository<Curriculum>();
                var curriculum = await curriculumRepo.GetByIdAsync(curriculumId);

                if (curriculum is null)
                    return NotFound("Curriculum not found");

                if (subject.Curriculums.Any(s => s.CurriculumId == curriculumId))
                    return BadRequest("Subject already contains provided curriculum");

                var nn = new SubjectCurriculum(subject, curriculum);
                var screpo = _unitOfWork.GetRepository<SubjectCurriculum>();
                screpo.Add(nn);
                await _unitOfWork.SaveChangesAsync();

                return Ok();
            });

        [HttpDelete("{id}/curriculum/{curriculumId}")]
        public Task<ActionResult> DelSubject(Guid id, Guid curriculumId)
            => ExecuteAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<Subject>();
                var subject = await repo.GetQueryable()
                    .Include(s => s.Curriculums)
                    .SingleOrDefaultAsync(e => e.Id == id);

                if (subject is null)
                    return NotFound("Subject not found");

                var curriculumRepo = _unitOfWork.GetRepository<Curriculum>();
                var curriculum = await curriculumRepo.GetByIdAsync(curriculumId);

                if (curriculum is null)
                    return NotFound("Curriculum not found");

                var screpo = _unitOfWork.GetRepository<SubjectCurriculum>();
                var nn = await screpo.GetQueryable()
                    .SingleOrDefaultAsync(n =>
                        n.CurriculumId == curriculumId
                        && n.SubjectId == id);

                if (nn is null)
                    return BadRequest("Relationship does not exist");

                screpo.Delete(nn);
                await _unitOfWork.SaveChangesAsync();

                return Ok();
            });
    }
}