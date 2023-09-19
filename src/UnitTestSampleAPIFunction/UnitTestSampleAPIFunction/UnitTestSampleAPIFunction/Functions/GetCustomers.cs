using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using SampleDAL;
using SampleRepository.Repositories;

namespace UnitTestSampleAPIFunction.Functions
{
    public class GetCustomers
    {
        private ILogger _logger;
        private readonly IConfiguration _config;
        private readonly ICustomerRepository _customerRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="config">The Configuration object injected by the host</param>
        /// <param name="customerRepository"></param>
        public GetCustomers(IConfiguration config, ICustomerRepository customerRepository)
        {
            // _logger = log;
            _config = config;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Get Customers API. This is a sample API to demonstrate how to write unit tests for Azure Functions. It simulates some of the features of OData, but is not using the OData SDK.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        [FunctionName("GetCustomers")]
        // Add these OpenAPI attributes
        [OpenApiOperation(operationId: "GetCustomers", tags: new[] { "customers" }, Summary = "This returns a collection of Customers")]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "x-functions-key",
            In = OpenApiSecurityLocationType.Header, Description = "This is the Azure Function API Key. In production this should be hidden behind API Manager")]
        [OpenApiParameter(name: "top", In = ParameterLocation.Query, Required = false, Type = typeof(int),
            Description = "The number of records it wants to retrieve in each page")]
        [OpenApiParameter(name: "skip", In = ParameterLocation.Query, Required = false, Type = typeof(int),
            Description = "The offset from the beginning of the results")]
        [OpenApiParameter(name: "filter", In = ParameterLocation.Query, Required = false, Type = typeof(string),
            Description =
                "used to filter the results of a data query, so that only a subset of the data that satisfies the specified condition is returned.")]
        [OpenApiParameter(name: "count", In = ParameterLocation.Query, Required = false, Type = typeof(bool),
            Description = "Enable the count to be returned. Default = true")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(CustomerListDto),
            Description = "The list of Customers, Total Count and Link to the next page.")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Description = "The request was successful, but no customers matching the parameters could be found.")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "No customers matching the parameters could be found.")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "The request was not understood. Possibly a parameter is missing or incorrect.")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Unauthorized, Description = "Unauthorized. The API Key is missing or incorrect.")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Internal Server Error.")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            HttpRequest req, ILogger logger)
        {
            _logger = logger;

            #region Get the parameters from the query string

            // Each parameter has a default value in the configuration file.
            // If the parameter is not passed in the query string, the default value is used.
            // If the parameter passed in the query string is not valid, the default value is used.

            int top;
            if (!int.TryParse(req.Query["top"], out top))
            {
                top = _config.GetValue<int>("DefaultTop");
            }

            int skip;
            if (!int.TryParse(req.Query["skip"], out skip))
            {
                skip = _config.GetValue<int>("DefaultSkip");
            }

            string filter;
            if (string.IsNullOrEmpty(req.Query["filter"]))
            {
                filter = _config.GetValue<string>("DefaultFilter");
            }
            else
            {
                filter = req.Query["filter"];
            }

            string orderBy;
            if (string.IsNullOrEmpty(req.Query["orderby"]))
            {
                orderBy = _config.GetValue<string>("DefaultOrderBy");
            }
            else
            {
                orderBy = req.Query["orderby"];
            }

            bool count;
            if (!bool.TryParse(req.Query["count"], out count))
            {
                count = _config.GetValue<bool>("DefaultCount");
            }

            #endregion


            #region Get the parameters from the request body if using a POST

            //get the parameters from the request body and overwrite the defaults if they are passed in
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            if (data != null)
            {
                top = data.top;
                skip = data.skip;
                filter = data.filter;
                orderBy = data.orderby;
                count = data.count;
            }

            #endregion


            #region Application Insights Logging and setting up for timing and metrics

            // Write some meta data, properties and metrics for Application Insights logging
            var logProps = new Dictionary<string, string>
            {
                { "FunctionName", "GetCustomers" },
                { "RecordsRequested", top.ToString() }
            };
            // Create an Event Id
            var eventId = new EventId(1, "GetCustomers");
            _logger.Log(LogLevel.Information, eventId, "GetCustomer Function Called", logProps);
            // or use the explicit log level
            _logger.LogInformation(eventId, "GetCustomer Function Called", logProps);

            //Log some Metrics
            _logger.LogMetric("RecordsRequests", Convert.ToDouble(top));

            #endregion

            #region Validate the parameters

            // This is an example of how to validate the parameters and return a BadRequest if they are not valid
            if (top > 500)
            {
                 return new BadRequestErrorMessageResult("The maximum number of records that can be returned is 500");
            }

            #endregion

            #region Get the data from the database and return it

            // Get the data from the database using the top, skip, filter and orderBy parameters
            // Because handling a full featured filter is beyond the scope of this sample, we are only handling a simple filter on the Customer Surname

            try
            {
                var response = new
                {
                    TotalCount = await _customerRepository.CountCustomers(filter).CountAsync(),
                    Customers = await _customerRepository.GetCustomers(top, skip, filter, orderBy).ToListAsync(),
                    NextLink = "coming soon"
                };
                return new OkObjectResult(response);
            }
            catch (Exception e)
            {
                _logger.LogError(eventId, e, "Error getting customers", logProps);
                return new InternalServerErrorResult();
            }
            
            #endregion
        }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for Customer List Response
    /// </summary>
    public class CustomerListDto
    {
        /// <summary>
        /// Total count of records in the result set
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// List of Customers in this page
        /// </summary>
        public List<SalesLT_Customer> Customers { get; set; }

        /// <summary>
        /// Link to the next page
        /// </summary>
        public string NextLink { get; set; }
    }
}