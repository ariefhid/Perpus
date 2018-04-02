using Perpus.Domain.Entity;
using Perpus.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perpus.Mapper
{
    public static class BookTypeMapper
    {
        public static BookType ToEntity(this DTOBookType dTOBookType)
        {
            if (dTOBookType == null)
            {
                return null;
            }
            else
            {
                return new BookType()
                {
                    Id = dTOBookType.ID,
                    Code = dTOBookType.Code,
                    BookTypeName = dTOBookType.BookTypeName,
                    IsActive = dTOBookType.IsActive
                };
            }
        }
        public static DTOBookType ToDTO(this BookType bookType)
        {
            if (bookType == null)
            {
                return null;
            }
            else
            {
                return new DTOBookType()
                {
                    ID = bookType.Id,
                    Code = bookType.Code,
                    BookTypeName = bookType.BookTypeName,
                    IsActive = bookType.IsActive
                };
            }
        }
    }
}
