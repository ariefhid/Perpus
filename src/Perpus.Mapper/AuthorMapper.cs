using Perpus.Domain.Entity;
using Perpus.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perpus.Mapper
{
    public static class AuthorMapper
    {
        public static Author ToEntity(this DTOAuthor dTOAuthor)
        {
            if (dTOAuthor == null)
            {
                return null;
            }
            else
            {
                return new Author()
                {
                    Id = dTOAuthor.ID,
                    Code = dTOAuthor.Code,
                    AuthorName = dTOAuthor.AuthorName,
                    IsActive = dTOAuthor.IsActive
                };
            }
        }
        public static DTOAuthor ToDTO(this Author author)
        {
            if (author == null)
            {
                return null;
            }
            else
            {
                return new DTOAuthor()
                {
                    ID = author.Id,
                    Code = author.Code,
                    AuthorName = author.AuthorName,
                    IsActive = author.IsActive
                };
            }
        }
    }
}
