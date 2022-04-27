using CarService.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CarService.Services
{
    public class CoworkerService
    {
        private readonly targyiEszkozContext context;

        public CoworkerService(targyiEszkozContext context) {
            this.context = context;
        }

        public int GetCoworkersNumber() {
            return context.Coworkers.Count();
        }

        public Coworker GetCoworkerByEmail(string email) {
            return context.Coworkers.Include(cw => cw.Notebooks).Include(cw => cw.Phones).FirstOrDefault(cw => cw.Email == email);
        }

        public void AddPhoneToCoworker(int coworkerId, Phone phone) {
            phone.CoworkerId = coworkerId;
            context.Phones.Add(phone);
            context.SaveChanges();
        }
    }
}
