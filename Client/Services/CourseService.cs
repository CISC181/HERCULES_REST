using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Telerik.DataSource;
using HERCULES.Shared.DTO;
using HERCULES.EF.Models;
using SWARM.Shared;


namespace HERCULES.Client.Services
{
    public class CourseService
    {
        protected HttpClient Http { get; set; }

        public CourseService(HttpClient client)
        {
            Http = client;
        }

        public async Task<DataEnvelope<CourseDto>> GetCoursesService(DataSourceRequest gridRequest)
        {
            //options very important here !!!!!!!!!!
            var options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true
            };
            HttpResponseMessage response = await Http.PostAsJsonAsync("api/Course/GetCourses", gridRequest);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var JsonData = await response.Content.ReadFromJsonAsync<DataEnvelope<CourseDto>>(options);
                return JsonData;
            }

            throw new Exception($"The service returned with status {response.StatusCode}");
        }
    }
}