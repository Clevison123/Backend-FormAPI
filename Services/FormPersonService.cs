using FormAPI.Data;
using FormAPI.DTOs;
using FormAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace FormAPI.Services
{
    public class FormPersonService
    {
        private readonly AppDbContext _context;

        public FormPersonService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseFormPersonDto> CreateAsync(CreateFormPersonDto dto)
        {
            var existingUser = await _context.FormPeople
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (existingUser != null)
            {
                throw new Exception("Email já está em uso.");
            }

            var user = new FormPerson
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };

            _context.FormPeople.Add(user);

            await _context.SaveChangesAsync();

            return new ResponseFormPersonDto
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address
            };
        }
        public async Task<List<ResponseFormPersonDto>> GetAllAsync()
        {

            var users = await _context.FormPeople.ToListAsync();

            return users.Select(user => new ResponseFormPersonDto
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address
            }).ToList();
        }

        public async Task<ResponseFormPersonDto> GetByIdAsync(int id)
        {
            var user = await _context.FormPeople.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            return new ResponseFormPersonDto
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address
            };
        }

        public async Task<ResponseFormPersonDto> UpdateAsync(int id, UpdateFormPersonDto dto)
        {
            var userToUpdate = await _context.FormPeople
                .FirstOrDefaultAsync(x => x.Id == id);

            if (userToUpdate == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            var userWithSameEmail = await _context.FormPeople
                .FirstOrDefaultAsync(x =>
                    x.Email == dto.Email &&
                    x.Id != id);

            if (userWithSameEmail != null)
            {
                throw new Exception("Já existe outro usuário com este email.");
            }

            userToUpdate.Name = dto.Name;
            userToUpdate.Email = dto.Email;
            userToUpdate.Phone = dto.Phone;
            userToUpdate.Address = dto.Address;

            await _context.SaveChangesAsync();

            return new ResponseFormPersonDto
            {
                Name = userToUpdate.Name,
                Email = userToUpdate.Email,
                Phone = userToUpdate.Phone,
                Address = userToUpdate.Address
            };
        }

        public async Task<ResponseFormPersonDto> DeleteAsync(int id)
        {
            var user = await _context.FormPeople
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                throw new Exception("Usuário não encontrado");

            _context.FormPeople.Remove(user);
            await _context.SaveChangesAsync();

            return new ResponseFormPersonDto
            {
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address
            };
        }
    }
}
