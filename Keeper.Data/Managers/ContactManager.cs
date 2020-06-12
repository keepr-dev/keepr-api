﻿using Keeper.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keeper.Data.Managers
{
    public class ContactManager : IContactManager
    {
        private KeeperDbContext _dbContext;

        public ContactManager(KeeperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Contact> GetByIdAsync(int id, int userId)
        {
            return await _dbContext.Contacts
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id && x.Client.UserId == userId);
        }

        public async Task<IEnumerable<Contact>> GetByClientIdAsync(int clientId, int userId)
        {
            return await _dbContext.Contacts
                .AsNoTracking()
                .Where(x => x.ClientId == clientId && x.Client.UserId == userId)
                .ToListAsync();
        }

        public async Task<Contact> CreateAsync(int clientId, string firstName, string lastName, string email, string phone, int userId)
        {
            var client = await _dbContext.Clients
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == clientId && x.UserId == userId);

            if (client != null)
            {
                var newContact = new Contact()
                {
                    ClientId = clientId,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = phone,
                    Created = DateTime.UtcNow,
                };

                _dbContext.Contacts.Add(newContact);
                await _dbContext.SaveChangesAsync();

                return newContact;
            }

            return null;
        }

        public async Task<Contact> UpdateAsync(int id, string firstName, string lastName, string email, string phone, int userId)
        {
            var contact = await _dbContext.Contacts
                .SingleOrDefaultAsync(x => x.Id == id && x.Client.UserId == userId);

            if (contact != null)
            {
                if (!string.IsNullOrEmpty(firstName))
                {
                    contact.FirstName = firstName;
                }

                if (!string.IsNullOrEmpty(lastName))
                {
                    contact.LastName = lastName;
                }

                if (!string.IsNullOrEmpty(email))
                {
                    contact.Email = email;
                }

                if (!string.IsNullOrEmpty(phone))
                {
                    contact.Phone = phone;
                }

                contact.Modified = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();

                return contact;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var contact = await _dbContext.Contacts
                .SingleOrDefaultAsync(x => x.Id == id && x.Client.UserId == userId);

            if (contact != null)
            {
                _dbContext.Contacts.Remove(contact);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
