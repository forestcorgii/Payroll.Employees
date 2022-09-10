﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pms.Employees.ServiceLayer.HRMS.Exceptions;

namespace Pms.Employees.ServiceLayer.HRMS.Adapter
{
    public class HRMSAdapter
    {
        private HttpClient Client = new HttpClient();
        private HRMSParameter Parameter;

        public HRMSAdapter(HRMSParameter hrmsParameter)
        {
            Client = new HttpClient();
            Client.Timeout = TimeSpan.FromSeconds(30d);

            Parameter = hrmsParameter;

        }

        public async Task<T> GetEmployeeFromHRMS<T>(string eeId, string site)
        {
            try
            {
                Parameter.BodyArgs["idno"] = eeId;
                var content = new FormUrlEncodedContent(Parameter.BodyArgs);

                var response = await Client.PostAsync(Parameter.Urls[site], content);

                string responseString = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var jsonSettings = new JsonSerializerSettings();
                    jsonSettings.NullValueHandling = NullValueHandling.Ignore;

                    HRMSResponse<T> employee = JsonConvert.DeserializeObject<HRMSResponse<T>>(responseString, jsonSettings);
                    if (employee is not null)
                        return employee.message[0];
                }
                else
                {
                    switch (response.StatusCode)
                    {
                        case (System.Net.HttpStatusCode)400:
                            throw new InvalidRequestException();
                        case (System.Net.HttpStatusCode)404:
                            throw new EmployeeNotFoundException(eeId);
                    }
                }
            }
            catch (InvalidRequestException) { }
            catch (EmployeeNotFoundException) { }
            return default;
        }

        public class HRMSResponse<T>
        {
            public List<T> message = new();
            public string code = "";
        }
    }
}
