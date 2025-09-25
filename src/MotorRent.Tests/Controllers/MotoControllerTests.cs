using MotorRent.ApiService.Common;
using MotorRent.Application.DTO.Moto;
using MotorRent.Application.ViewModels;
using MotorRent.Domain.Constants;
using System.Net.Http.Json;
using Xunit;

namespace MotorRent.Tests.Controllers
{
    public class MotoControllerTests(WebApplicationFixture fixture) : IClassFixture<WebApplicationFixture>
    {
        [Fact]
        public async Task Post_CreateValidMoto_StatusCreated()
        {
            // Arrange
            var moto = new CreateMotoDTO(identificador: "moto6789", modelo: "CG", ano: 2025, placa: "MOT-6789");
            await fixture.HttpClient.DeleteAsync($"/api/motos/{moto.identificador}");

            // Act
            var response = await fixture.HttpClient.PostAsJsonAsync("/api/motos", moto);
            var content = await response.Content.ReadAsStringAsync();
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(content, string.Empty);
        }

        [Fact]
        public async Task Post_TryCreateInvalidMoto_StatusBadRequest()
        {
            // Arrange
            var moto = new CreateMotoDTO(identificador: Guid.NewGuid().ToString(), modelo: "CG", ano: 2025, placa: "1234567");
            // Act
            var response = await fixture.HttpClient.PostAsJsonAsync("/api/motos", moto);
            var content = await response.Content.ReadFromJsonAsync<ApiResponse>();
            // Assert

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(Messages.InvalidData, content!.Mensagem);
        }

        [Fact]
        public async Task Get_GetMotosByPlate_StatusOk()
        {
            // Arrange
            var moto = new CreateMotoDTO(identificador: Guid.NewGuid().ToString(), modelo: "CG", ano: 2025, placa: "ABC-1234");
            await fixture.HttpClient.PostAsJsonAsync("/api/motos", moto);
            await Task.Delay(5000); // O ideal seria uma abordagem mais robusta para garantir que a mensagem foi processada, mas para fins de teste, um delay simples vai funcionar.

            // Act
            var response = await fixture.HttpClient.GetAsync($"/api/motos?placa={moto.placa}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<List<MotoViewModel>>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<List<MotoViewModel>>(content, exactMatch: false);
            Assert.NotEmpty(content);
            Assert.Equal(content.First().placa, moto.placa);
        }

        [Fact]
        public async Task Get_GetMotoById_StatusOk()
        {
            var moto = new CreateMotoDTO(identificador: "motoById", modelo: "CG", ano: 2025, placa: "MOT-1234");
            await fixture.HttpClient.PostAsJsonAsync("/api/motos", moto);
            await Task.Delay(5000); // O ideal seria uma abordagem mais robusta para garantir que a mensagem foi processada, mas para fins de teste, um delay simples vai funcionar.

            // Act

            var response = await fixture.HttpClient.GetAsync($"/api/motos/{moto.identificador}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<MotoViewModel>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<MotoViewModel>(content, exactMatch: false);
            Assert.Equal(content.placa, moto.placa);
        }

        [Fact]
        public async Task Delete_DeleteMotoById_StatusOk()
        {
            // Arrange
            var moto = new CreateMotoDTO(identificador: "motoDelete", modelo: "CG", ano: 2025, placa: "MOT-9999");
            await fixture.HttpClient.PostAsJsonAsync("/api/motos", moto);
            await Task.Delay(5000); // O ideal seria uma abordagem mais robusta para garantir que a mensagem foi processada, mas para fins de teste, um delay simples vai funcionar.
            // Act
            var response = await fixture.HttpClient.DeleteAsync($"/api/motos/{moto.identificador}");
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
