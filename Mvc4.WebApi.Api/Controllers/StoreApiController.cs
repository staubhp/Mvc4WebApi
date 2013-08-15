﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Mvc4.WebApi.Service;
using Mvc4.WebApi.Model;
using AutoMapper;
using Mvc4.WebApi.Api.Response;
using Mvc4.WebApi.Api.Request;

namespace Mvc4.WebApi.Api.Controllers
{
    public class StoreApiController : ApiController
    {
        private IStoreService _storeService;

        public StoreApiController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpDelete]
        public void Delete([ModelBinder] Store store)
        {
            if (store.Id.HasValue)
            {
                _storeService.DeleteStore(store.Id.Value);
            }
        }

        public StoreResponse GetStore(int id)
        {
            Store store = (from s in _storeService.GetStores()
                           where s.Id == id
                           select s).First();
            return Mapper.Map<StoreResponse>(store);
        }

        public IEnumerable<KeyValuePair<string, string>> GetDistricts()
        {
            ICollection<KeyValuePair<string, string>> response = new Collection<KeyValuePair<string, string>>();

            response.Add(new KeyValuePair<string, string>("1", "Bay Area"));
            response.Add(new KeyValuePair<string, string>("2", "Nevada"));
            response.Add(new KeyValuePair<string, string>("3", "San Diego"));

            return response;
        }

        public IEnumerable<KeyValuePair<string, string>> GetRetailers()
        {
            ICollection<KeyValuePair<string, string>> response = new Collection<KeyValuePair<string, string>>();
            response.Add(new KeyValuePair<string, string>("1", "Best Buy"));
            response.Add(new KeyValuePair<string, string>("2", "Frys"));
            response.Add(new KeyValuePair<string, string>("3", "Wal Mart"));
            response.Add(new KeyValuePair<string, string>("4", "Target"));
            response.Add(new KeyValuePair<string, string>("5", "Safeway"));
            response.Add(new KeyValuePair<string, string>("6", "Knob Hill"));
            response.Add(new KeyValuePair<string, string>("7", "Luckys"));
            return response;
        }

        public IEnumerable<StoreListResponse> GetStores()
        {
            return Mapper.Map<IEnumerable<StoreListResponse>>(_storeService.GetStores());
        }

        public IEnumerable<KeyValuePair<string, string>> GetTerritorys(int? districtId)
        {
            ICollection<KeyValuePair<string, string>> response = new Collection<KeyValuePair<string, string>>();
            if (!districtId.HasValue)
            {
                return null;
            }

            switch (districtId)
            {
                case 1:
                    response.Add(new KeyValuePair<string, string>("1", "San Fransisco"));
                    response.Add(new KeyValuePair<string, string>("2", "San Jose"));
                    response.Add(new KeyValuePair<string, string>("3", "Oakland"));
                    break;
                case 2:
                    response.Add(new KeyValuePair<string, string>("4", "Las Vegas"));
                    response.Add(new KeyValuePair<string, string>("5", "Reno"));
                    break;
                case 3:
                    response.Add(new KeyValuePair<string, string>("7", "San Diego"));
                    response.Add(new KeyValuePair<string, string>("8", "Oceanside"));
                    response.Add(new KeyValuePair<string, string>("9", "Escondido"));
                    break;
            }

            return response;
        }

        [HttpPost]
        public object Post(StoreEditRequest store)
        {
            Store updateStore = Mapper.Map<Store>(store);
            _storeService.UpdateStore(updateStore);
            return null;
        }
    }
}