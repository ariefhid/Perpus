using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Perpus.Domain.Abstract;
using Perpus.Domain.Entity;
using Perpus.DTO;
using Microsoft.EntityFrameworkCore;
using Perpus.Mapper;
using Perpus.Core;

namespace Perpus.Controllers
{
    [Route("api/[Controller]")]
    public class AuthorController : Controller
    {
        #region Private Declaration
        IAuthorManager _authorManager;
        #endregion
        
        public AuthorController(IAuthorManager authorManager)
        {
            this._authorManager = authorManager;
        }

        [HttpGet]
        public async Task<IActionResult> Authors()
        {
            IEnumerable<Author> Authors = await this._authorManager.Authors
                .OrderBy(i => i.Code)
                .ToListAsync();
            IEnumerable<DTOAuthor> dtoAuthors = Authors.Select(x => x.ToDTO()).ToList();
            return Json(dtoAuthors);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Author(int Id)
        {
            Author Author = await this._authorManager.FindByIdAsync(Id);
            if (Author == null)
            {
                return NotFound();
            }
            else
            {
                return Json(Author.ToDTO());
            }
        }
        [HttpPost]
        public async Task<IActionResult> Authors([FromBody]DTOAuthor dtoAuthor)
        {
            Author Author = dtoAuthor.ToEntity();
            PerpusResult result = null;
            if (dtoAuthor.ID == 0)
            {
                result = await this._authorManager.CreateAsync(Author);
                if (result.Succeeded)
                    return new OkObjectResult(Author.ToDTO());
            }
            else
            {
                result = await this._authorManager.UpdateAsync(Author);
                if (result.Succeeded)
                    return Ok();
            }
            return BadRequest(result.Errors);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var AuthorTobeRemove = await this._authorManager.FindByIdAsync(Id);
            var result = await this._authorManager.DeleteAsync(AuthorTobeRemove);
            if (result.Succeeded)
            {
                return new NoContentResult();
            }
            else
            {
                return NotFound();
            }
        }
    }
}