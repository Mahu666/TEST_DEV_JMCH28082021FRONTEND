using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MVC_TEST_DEV.Models;

namespace MVC_TEST_DEV.Controllers
{
    public class PersonaFisicaController : Controller
    {
        // GET: PersonaFisica
        public ActionResult Index()
        {
            IEnumerable<Tb_PersonasFisicas> Tb_PersonasFisicas1 = null;
            using (var client = new HttpClient())
            {
                //API: https://localhost:44337/api/Tb_PersonasFisicas1
                client.BaseAddress = new Uri("https://localhost:44337/api/");
                var responseTask = client.GetAsync("Tb_PersonasFisicas1");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var persona = result.Content.ReadAsAsync<IList<Tb_PersonasFisicas>>();
                    persona.Wait();
                    Tb_PersonasFisicas1 = persona.Result;

                }
                else
                {
                    //Retorna error aquí.
                    Tb_PersonasFisicas1 = Enumerable.Empty<Tb_PersonasFisicas>();
                    ModelState.AddModelError(string.Empty, "Ocurrió un error en el servidor, Contacte con el administrador.");
                }
            }
            return View(Tb_PersonasFisicas1);
        }

        //POST PersonaFisica
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Tb_PersonasFisicas personasFisicas)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/Tb_PersonasFisicas1");
                var persona = client.PostAsJsonAsync<Tb_PersonasFisicas>("Tb_PersonasFisicas1", personasFisicas);
                persona.Wait();

                var postResult = persona.Result;

                if (postResult.IsSuccessStatusCode)                
                    return RedirectToAction("Index");                
            }
            ModelState.AddModelError(string.Empty, "Ocurrió un error en el servidor, Contacte con el administrador.");
            return View(personasFisicas);
        }

        //PUT Persona Fisica
        public ActionResult Edit(int IdPersonaFisica)
        {
            Tb_PersonasFisicas personasFisicas = null;

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/");
                var responseTask = client.GetAsync("Tb_PersonasFisicas1/" + IdPersonaFisica.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Tb_PersonasFisicas>();
                    readTask.Wait();

                    personasFisicas = readTask.Result;
                }
            }
            return View(personasFisicas);
        }

        //POST para ACTUALIZAR los datos.
        [HttpPost]
        public ActionResult Edit(Tb_PersonasFisicas personasFisicas)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44337/api/Tb_PersonasFisicas1");
                //En esta versión es necesario identificar el id despues de la request uri
                var putTask = client.PutAsJsonAsync("https://localhost:44337/api/Tb_PersonasFisicas1/" + personasFisicas.IdPersonaFisica, personasFisicas);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                return View(personasFisicas);
                
            }
        }

        //DELETE personas fisicas
        public ActionResult Delete(int IdPersonaFisica)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/");
                var deleteTask = client.DeleteAsync("Tb_PersonasFisicas1/" + IdPersonaFisica.ToString());

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

       

    }
}