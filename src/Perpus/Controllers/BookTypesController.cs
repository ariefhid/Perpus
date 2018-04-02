using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Perpus.Domain.Abstract;
using Perpus.Domain.Entity;
using Perpus.DTO;
using Perpus.Mapper;
using Microsoft.EntityFrameworkCore;
using Perpus.Core;

namespace Perpus.Controllers
{
    [Route("api/[Controller]")]
    public class BookTypesController : Controller
    {
        #region Private Declaration
        IBookTypeManager _bookTypeManager;
        #endregion
        #region Constructor
        public BookTypesController(IBookTypeManager bookTypeManager)
        {
            this._bookTypeManager = bookTypeManager;
        }
        #endregion
        #region BookType CRUD
        [HttpGet]
        public async Task<IActionResult> BookTypes()
        {
            IEnumerable<BookType> BookTypes = await this._bookTypeManager.BookTypes
                .OrderBy(i => i.Code)
                .ToListAsync();
            IEnumerable<DTOBookType> dtoBookTypes = BookTypes.Select(x => x.ToDTO()).ToList();
            return Json(dtoBookTypes);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> BookType(int Id)
        {
            BookType BookType = await this._bookTypeManager.FindByIdAsync(Id);
            if (BookType == null)
            {
                return NotFound();
            }
            else
            {
                return Json(BookType.ToDTO());
            }
        }
        [HttpPost]
        public async Task<IActionResult> BookTypes([FromBody]DTOBookType dtoBookType)
        {
            BookType BookType = dtoBookType.ToEntity();
            PerpusResult result = null;
            if (dtoBookType.ID == 0)
            {
                result = await this._bookTypeManager.CreateAsync(BookType);
                if (result.Succeeded)
                    return new OkObjectResult(BookType.ToDTO());
            }
            else
            {
                result = await this._bookTypeManager.UpdateAsync(BookType);
                if (result.Succeeded)
                    return Ok();
            }
            return BadRequest(result.Errors);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var BookTypeTobeRemove = await this._bookTypeManager.FindByIdAsync(Id);
            var result = await this._bookTypeManager.DeleteAsync(BookTypeTobeRemove);
            if (result.Succeeded)
            {
                return new NoContentResult();
            }
            else
            {
                return NotFound();
            }
        }
        #endregion
    }
}