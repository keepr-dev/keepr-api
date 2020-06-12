﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keeper.Data.Models;

namespace Keeper.API.Models
{
    public class ContactModel
    {
        public ContactModel() { }

        public ContactModel(Contact contact)
        {
            this.Id = contact.Id;
            this.FirstName = contact.FirstName;
            this.LastName = contact.LastName;
            this.Email = contact.Email;
            this.Phone = contact.Phone;
            this.Created = contact.Created;
            this.Modified = contact.Modified;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
