﻿using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories
{
    public class GeneralRepository<Entity, TId> : IRepository<Entity, TId>
        where Entity : class
    {
        private readonly string request;
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor contextAccessor;

        public GeneralRepository(string request)
        {
            this.request = request;
           /* contextAccessor = new HttpContextAccessor();*/
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7023/api/")
            };
            // Ini yg bawah skip dulu
            /*httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", contextAccessor.HttpContext?.Session.GetString("JWToken"));*/
        }

        public async Task<ResponseMessageVM> Deletes(TId Guid)
        {
            ResponseMessageVM entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(Guid), Encoding.UTF8, "application/json");
            using (var response = httpClient.DeleteAsync(request + Guid).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseViewModel<Entity>> Get(TId id)
        {
            ResponseViewModel<Entity> entity = null;

            using (var response = await httpClient.GetAsync(request + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<ResponseViewModel<Entity>>(apiResponse);
            }
            return entity;
        }

        public async Task<ResponseMessageVM> Put(Entity entity)
        {
            ResponseMessageVM entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PutAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseMessageVM> Post(Entity entity)
        {
            ResponseMessageVM entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
            }
            return entityVM;
        }
        public async Task<ResponseListVM<Entity>> Get()
        {
            ResponseListVM<Entity> entityVM = null;
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseListVM<Entity>>(apiResponse);
            }
            return entityVM;
        }
    }
}