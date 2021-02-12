using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sprout.Controllers
{
    public abstract class BaseController : Controller
    {
        [NonAction]
        protected Result<T> Execute<T>(Func<T> func)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Result<T>.Succeed(func());
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            return Result<T>.Fail(default).AddErrors(ModelState) as Result<T>;
        }

        [NonAction]
        protected async Task<Result<T>> Execute<T>(Func<Task<T>> func)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Result<T>.Succeed(await func());
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            return Result<T>.Fail(default).AddErrors(ModelState) as Result<T>;
        }

        [NonAction]
        protected async Task<Result> Execute(Func<Task> func)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await func();

                    return Result.Succeed();
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            return Result.Fail().AddErrors(ModelState);
        }
    }

    public class Result
    {
        public bool Failed { get; set; }

        public string Message { get; set; }

        protected Result(bool failed) => Failed = failed;

        public static Result Fail() => new Result(true);

        public static Result Succeed() => new Result(false);

        public Result AddErrors(ModelStateDictionary modelState)
        {
            Message = modelState.First().Value.Errors.FirstOrDefault()?.ErrorMessage;

            return this;
        }

        public JsonResult ToJsonResult()
            => Failed
                ? new JsonResult(new { success = false, message = Message })
                : new JsonResult(new { success = true });
    }

    public class Result<T> : Result
    {
        public T Output { get; set; }

        private Result(T output, bool failed) : base(failed) => Output = output;

        public static Result<T> Fail(T output) => new Result<T>(output, true);

        public static Result<T> Succeed(T output) => new Result<T>(output, false);
    }
}
