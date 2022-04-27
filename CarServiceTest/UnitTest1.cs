using CarService.Models;
using CarService.Services;
using System.Linq;
using Xunit;

namespace CarServiceTest
{
    public class UnitTest1
    {
        private targyiEszkozContext context;
        private CoworkerService service;

        public UnitTest1() {
            this.context = new targyiEszkozContext();
            this.service = new CoworkerService(context);
        }

        // it just simply tests coworkers number
        [Fact]
        public void Test1()
        {
            Assert.Equal(2, service.GetCoworkersNumber());
            
        }

        // Test get coworker method, it checks coworker's name by email
        [Fact]
        public void Test2()
        {
            Coworker result = service.GetCoworkerByEmail("lindi.imre@gmail.com");
            Assert.Equal("Lindi Imre", result.Name);

        }

        // Test Phone service, test phone addition method
        [Fact]
        public void Test3()
        {
            Phone phone = new Phone() { 
                CoworkerId = 1,
                Brand = "Nokia",
                Type = "6310i"
            };
            service.AddPhoneToCoworker(1, phone);
            Coworker result = service.GetCoworkerByEmail("lindi.imre@gmail.com");
            Assert.Equal("Lindi Imre", result.Name);
            Assert.NotNull(result.Phones.Where(p => p.Type == "6310i").FirstOrDefault());

        }
    }
}
