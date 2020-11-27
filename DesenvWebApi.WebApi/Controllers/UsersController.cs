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
    public class UsersController : ApiControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public Task<ActionResult<IEnumerable<UserViewModel>>> GetAsync()
            => ExecuteAsync<IEnumerable<UserViewModel>>(async () =>
            {
                var repo = _unitOfWork.GetRepository<User>();
                var users = await repo.GetQueryable()
                    .Include(u => u.Curriculum)
                    .OrderBy(u => u.CreatedAt)
                    .Select(u => (UserViewModel) u)
                    .ToListAsync();

                if (!users.Any())
                    return NoContent();

                return Ok(users);
            });

        [HttpGet("{id}")]
        public Task<ActionResult<IEnumerable<UserViewModel>>> GetAsync(Guid id)
            => ExecuteAsync<IEnumerable<UserViewModel>>(async () =>
            {
                var repo = _unitOfWork.GetRepository<User>();
                var user = await repo.GetQueryable()
                    .Include(u => u.Curriculum)
                    .Select(u => (UserViewModel) u)
                    .SingleOrDefaultAsync(u => u.Id == id);

                if (user is null)
                    return NotFound();

                return Ok(user);
            });

        [HttpPost]
        public Task<ActionResult<UserViewModel>> PostAsync([FromBody] UserInputModel im)
            => ExecuteAsync<UserViewModel>(async () =>
            {
                var repo = _unitOfWork.GetRepository<User>();
                var sameEmail = await repo.GetQueryable()
                    .SingleOrDefaultAsync(u => u.Email == im.Email);

                if (sameEmail != null)
                    return BadRequest("Another user with the same email already exists");

                var user = new User(im.Email, im.Name);
                repo.Add(user);
                await _unitOfWork.SaveChangesAsync();

                return Ok((UserViewModel) user);
            });

        [HttpPut("{id}")]
        public Task<ActionResult<UserViewModel>> PutAsync(Guid id, [FromBody] UserInputModel im)
            => ExecuteAsync<UserViewModel>(async () =>
            {
                var repo = _unitOfWork.GetRepository<User>();
                var user = await repo.GetByIdAsync(id);

                if (user is null)
                    return NotFound();

                if (!string.IsNullOrWhiteSpace(im.Name))
                {
                    user.SetName(im.Name);
                }

                repo.Update(user);
                await _unitOfWork.SaveChangesAsync();

                return Ok((UserViewModel) user);
            });

        [HttpDelete("{id}")]
        public Task<ActionResult> DeleteAsync(Guid id)
            => ExecuteAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<User>();
                var user = await repo.GetByIdAsync(id);

                if (user is null)
                    return NotFound();

                repo.Delete(user);
                await _unitOfWork.SaveChangesAsync();

                return Ok();
            });

        [HttpPost("{id}/curriculum/{curriculumId}")]
        public Task<ActionResult> SetCurriculum(Guid id, Guid curriculumId)
            => ExecuteAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<User>();
                var user = await repo.GetByIdAsync(id);

                if (user is null)
                    return NotFound("User not found");

                var curriculumRepo = _unitOfWork.GetRepository<Curriculum>();
                var curriculum = await curriculumRepo.GetByIdAsync(curriculumId);

                if (curriculum is null)
                    return NotFound("Curriculum not found");

                user.SetCurriculum(curriculum);
                await _unitOfWork.SaveChangesAsync();

                return Ok();
            });

        [HttpDelete("{id}/curriculum")]
        public Task<ActionResult> UnsetCurriculum(Guid id)
            => ExecuteAsync(async () =>
            {
                var repo = _unitOfWork.GetRepository<User>();
                var user = await repo.GetByIdAsync(id);

                if (user is null)
                    return NotFound("User not found");

                user.SetCurriculum(null);
                await _unitOfWork.SaveChangesAsync();

                return Ok();
            });
    }
}