using ApplicationCore;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SocialGames.TechnicalTest.Api.Validators
{
    public class GameIdValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

                if (descriptor != null)
                {
                    var parameters = descriptor.MethodInfo.GetParameters();

                    if (parameters.Length == 1)
                    {
                        
                        if (Convert.ToString(context.ActionArguments[parameters[0].Name]) != Constants.GAME_ID)
                            context.ModelState.AddModelError(nameof(GameIdValidatorAttribute), $"Game ID is not {Constants.GAME_ID}");
                    }
                    else
                    {
                        context.ModelState.AddModelError(nameof(GameIdValidatorAttribute), $"Too many parameters");
                    }
                }
            }
            catch (Exception ex)
            {
                context.ModelState.AddModelError(nameof(GameIdValidatorAttribute), ex.Message);
            }
            finally
            {
                base.OnActionExecuting(context);
            }
        }

    }
}
