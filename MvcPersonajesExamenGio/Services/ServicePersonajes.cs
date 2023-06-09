﻿using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using MvcPersonajesExamenGio.Models;

namespace MvcPersonajesExamenGio.Services {
    public class ServicePersonajes {

        private MediaTypeWithQualityHeaderValue Header;
        private string UrlApi;

        public ServicePersonajes(IConfiguration configuration) {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiPersonajesAzure");
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        #region GENERAL
        private async Task<T> CallApiAsync<T>(string request) {
            using (HttpClient client = new HttpClient()) {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                HttpResponseMessage response = await client.GetAsync(request);
                return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<T>() : default(T);
            }
        }

        private async Task<HttpStatusCode> InsertApiAsync<T>(string request, T objeto) {
            using (HttpClient client = new HttpClient()) {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                string json = JsonConvert.SerializeObject(objeto);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
                return response.StatusCode;
            }
        }

        private async Task<HttpStatusCode> UpdateApiAsync<T>(string request, T objeto) {
            using (HttpClient client = new HttpClient()) {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                string json = JsonConvert.SerializeObject(objeto);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
                return response.StatusCode;
            }
        }

        // Se supone que en el request ya va el id. Ejemplo: http:/localhost/api/deletealgo/17
        private async Task<HttpStatusCode> DeleteApiAsync(string request) {
            using (HttpClient client = new HttpClient()) {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();

                HttpResponseMessage response = await client.DeleteAsync(request);
                return response.StatusCode;
            }
        }
        #endregion

        #region PERSONAJES
        public async Task<List<Personaje>?> GetPersonajesAsync() {
            string request = "/api/personaje";
            List<Personaje>? personajes = await this.CallApiAsync<List<Personaje>?>(request);
            return personajes;
        }

        public async Task<Personaje?> FindPersonajeAsync(int idPersonaje) {
            string request = "/api/personaje/" + idPersonaje;
            Personaje? personaje = await this.CallApiAsync<Personaje?>(request);
            return personaje;
        }

        public async Task<HttpStatusCode> InsertPersonajeAsync(Personaje personaje) {
            string request = "/api/personaje/insertpersonaje";
            return await this.InsertApiAsync<Personaje>(request, personaje);
        }

        public async Task<HttpStatusCode> UpdatePersonajeAsync(Personaje personaje) {
            string request = "/api/personaje/updatepersonaje";
            return await this.UpdateApiAsync<Personaje>(request, personaje);
        }

        public async Task<HttpStatusCode> DeletePersonajeAsync(int idPersonaje) {
            string request = "/api/personaje/deletepersonaje/" + idPersonaje;
            return await this.DeleteApiAsync(request);
        }
        #endregion

    }
}
