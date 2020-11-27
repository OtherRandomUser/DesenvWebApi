using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DesenvWebApi.WebApi.Controllers
{
    public abstract class ApiControllerBase: ControllerBase
    {
        protected async Task<ActionResult<T>> ExecuteAsync<T>(Func<Task<ActionResult<T>>> function)
        {
            try
            {
                return await function();
            }
            catch (ArgumentException e)
            {
                // TODO log
                return BadRequest(e.Message);
            }
        }

        protected async Task<ActionResult> ExecuteAsync(Func<Task<ActionResult>> function)
        {
            try
            {
                return await function();
            }
            catch (ArgumentException e)
            {
                // TODO log
                return BadRequest(e.Message);
            }
        }
    }
}