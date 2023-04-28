using Microsoft.AspNetCore.Mvc;
using MvcPersonajesExamenGio.Models;
using MvcPersonajesExamenGio.Services;

namespace MvcPersonajesExamenGio.Controllers {
    public class PersonajesController : Controller {

        private ServicePersonajes service;

        public PersonajesController(ServicePersonajes service) {
            this.service = service;
        }

        public async Task<IActionResult> GetPersonajes() {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            return View(personajes);
        }

        public async Task<IActionResult> DetailsPersonaje(int idPersonaje) {
            Personaje? personaje = await this.service.FindPersonajeAsync(idPersonaje);
            return View(personaje);
        }

        public IActionResult CreatePersonaje() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonaje(Personaje personaje) {
            await this.service.InsertPersonajeAsync(personaje);
            return RedirectToAction("GetPersonajes");
        }

        public async Task<IActionResult> UpdatePersonaje(int idPersonaje) {
            Personaje? personaje = await this.service.FindPersonajeAsync(idPersonaje);
            return View(personaje);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePersonaje(Personaje personaje) {
            await this.service.UpdatePersonajeAsync(personaje);
            return RedirectToAction("GetPersonajes");
        }

        public async Task<IActionResult> DeletePersonaje(int idPersonaje) {
            await this.service.DeletePersonajeAsync(idPersonaje);
            return RedirectToAction("GetPersonajes");
        }

    }
}
