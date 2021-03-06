﻿using HCMSSMI.Entities.Models;

using HCMSSMI.Entities.Models.Login;
using MoreLinq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using HCMSSMI.Utils;
using HCMSSMI.Entities.Models.Profile;
using RestSharp.Serialization.Json;
using HCMSSMI.Entities.Models.Candidate;
using HCMSSMI.Entities.Models.Profile.Address;
using HCMSSMI.Entities.Models.Profile.Sector;
using HCMSSMI.Entities.Models.Employee;

namespace HCMSSMI.Reader
{
    public class ReaderService : IReaderService
    {

        #region Private Members

        /// <summary>
        /// A single instance member ReaderConfiguration
        /// </summary>
        private readonly ReaderConfiguration configuration;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration">A single instance parameter from <see cref="ReaderConfiguration"/></param>
        public ReaderService(ReaderConfiguration configuration)
        {
            // TODO: ADD LOGGER SERVICE
            this.configuration = configuration;

        }

        #endregion

        #region Users Authentication

        public async Task<Entities.Models.RestRequest.RestResponse<Users>> GetUserAccountDetail(int id, string clientKey = null, string apiKey = null)
        {

            Entities.Models.RestRequest.RestResponse<Users> user = new Entities.Models.RestRequest.RestResponse<Users>();

            try
            {
                configuration.ClientURL = @ServerApi.URL_AuthGateway;


                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = $"api/v1/Users/Get?id={id}";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.GET, DataFormat.Json);

                var response = await configuration.Client.ExecuteAsync<Entities.Models.RestRequest.RestResponse<Users>>(configuration.Request);

                return response.Data;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
            }


            return user;
        }

        public async Task<Entities.Models.RestRequest.RestResponse<Users>> GetUserAccountDetail(string username, string clientKey = null, string apiKey = null)
        {

            Entities.Models.RestRequest.RestResponse<Users> user = new Entities.Models.RestRequest.RestResponse<Users>();

            try
            {
                configuration.ClientURL = @ServerApi.URL_AuthGateway;


                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = $"api/v1/Users/GetUsers/{username}";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.GET, DataFormat.Json);

                var response = configuration.Client.Execute<Entities.Models.RestRequest.RestResponse<Users>>(configuration.Request);

                return await Task.FromResult(response.Data);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
            }


            return user;
        }

        public async Task<IEnumerable<Roles>> GetUserRoles(string clientKey = null, string apiKey = null)
        {


            try
            {
                configuration.ClientURL = @ServerApi.URL_AuthGateway;

                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = $"api/v1/Users/Roles/Get";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.GET, DataFormat.Json);

                var response = configuration.Client.Execute<List<Roles>>(configuration.Request);

                return await Task.FromResult(response.Data);

            }
            catch (Exception ex)
            {
                throw new ArgumentException($"An error occurred: {ex.Message}");
            }


        }

        #endregion

        #region Profile

        public async Task<Entities.Models.RestRequest.RestResponse<Profile>> SearchProfileIndex(string userName, string clientKey = null, string apiKey = null)
        {

            Entities.Models.RestRequest.RestResponse<Profile> user = new Entities.Models.RestRequest.RestResponse<Profile>();

            try
            {
                configuration.ClientURL = @ServerApi.URL_AuthGateway;


                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = $"GetProfile/{userName}";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.GET, DataFormat.Json);

                var response = configuration.Client.Execute<Entities.Models.RestRequest.RestResponse<Profile>>(configuration.Request);

                return await Task.FromResult(response.Data);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
            }


            return user;
        }

        public async Task<IEnumerable<Address>> SearchAddress(SearchAddress Searchaddress, string clientKey = null, string apiKey = null)
        {
            List<Address> AddressList = new List<Address>();

            try
            {
                configuration.ClientURL = @ServerApi.URL_AuthGateway;


                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = $"SearchAddress";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.POST, DataFormat.Json);
                configuration.Request.AddJsonBody(Searchaddress);

                var response = configuration.Client.Execute<List<Address>>(configuration.Request);

                return response.Data;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
            }

            return await Task.FromResult(AddressList);
        }

        public async Task<IEnumerable<Sector>> SearchSector(SearchSector searchSector, string clientKey = null, string apiKey = null)
        {
            List<Sector> AddressList = new List<Sector>();

            try
            {
                configuration.ClientURL = @ServerApi.URL_AuthGateway;


                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = $"SearchSector";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.POST, DataFormat.Json);
                configuration.Request.AddJsonBody(searchSector);

                var response = configuration.Client.Execute<List<Sector>>(configuration.Request);

                return response.Data;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
            }

            return await Task.FromResult(AddressList);
        }

        #endregion

        #region Candidate
        public async Task<IEnumerable<Profile>> SearchCandidatePublic(SearchCandidate candidate, string clientKey = null, string apiKey = null)
        {
            List<Profile> candidateList = new List<Profile>();

            try
            {
                configuration.ClientURL = @ServerApi.URL_AuthGateway;


                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = $"SearchCandidatePublic";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.POST, DataFormat.Json);
                configuration.Request.AddJsonBody(candidate);

                var response = configuration.Client.Execute<List<Profile>>(configuration.Request);

                return response.Data;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
            }

            return await Task.FromResult(candidateList);
        }

        public async Task<IEnumerable<Profile>> SearchCandidate(SearchCandidate candidate, string clientKey = null, string apiKey = null)
        {
            List<Profile> candidateList = new List<Profile>();

            try
            {
                configuration.ClientURL = @ServerApi.URL_AuthGateway;


                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = $"SearchCandidate";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.POST, DataFormat.Json);
                configuration.Request.AddJsonBody(candidate);

                var response = configuration.Client.Execute<List<Profile>>(configuration.Request);

                return response.Data;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
            }

            return await Task.FromResult(candidateList);
        }

        #endregion

        #region Employee
        public async Task<IEnumerable<Profile>> SearchEmployeePublic(SearchEmployee employee, string clientKey = null, string apiKey = null)
        {
            List<Profile> EmployeeList = new List<Profile>();

            try
            {
                configuration.ClientURL = @ServerApi.URL_AuthGateway;


                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = $"SearchEmployeePublic";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.POST, DataFormat.Json);
                configuration.Request.AddJsonBody(employee);

                var response = configuration.Client.Execute<List<Profile>>(configuration.Request);

                return response.Data;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
            }

            return await Task.FromResult(EmployeeList);
        }

        public async Task<IEnumerable<Profile>> SearchEmployee(SearchEmployee employee, string clientKey = null, string apiKey = null)
        {
            List<Profile> candidateList = new List<Profile>();

            try
            {
                configuration.ClientURL = @ServerApi.URL_AuthGateway;


                if ((!string.IsNullOrEmpty(clientKey) && !string.IsNullOrEmpty(apiKey)))
                    configuration.Client.Authenticator = new HttpBasicAuthenticator(clientKey, apiKey);

                configuration.RequestURL = $"SearchEmployee";
                configuration.Client = new RestClient($"{configuration.ClientURL}");
                configuration.Request = new RestRequest($"{configuration.RequestURL}", Method.POST, DataFormat.Json);
                configuration.Request.AddJsonBody(employee);

                var response = configuration.Client.Execute<List<Profile>>(configuration.Request);

                return response.Data;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occured when request to API: {ex.Message}");
            }

            return await Task.FromResult(candidateList);
        }

        #endregion
    }
}
