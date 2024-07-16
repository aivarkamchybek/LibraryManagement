using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<AuthorDto> GetAllAuthors();
        void AddAuthor(AuthorDto author);
        void UpdateAuthor(AuthorDto author);
        void DeleteAuthor(int id);
    }
}
