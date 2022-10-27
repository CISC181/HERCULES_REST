using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using HERCULES.Client.Helper;
using HERCULES.Client.Services;
using SWARM.Shared;
using HERCULES.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Telerik.Blazor.Components;

namespace HERCULES.Client.Pages.Course
{
    public partial class Course : HerculesUI
    {
        [CascadingParameter]
        private Task<AuthenticationState> authState { get; set; }
        private IEnumerable<CourseDto> ieCourses { get; set; }
        private List<Course> lstcourse { get; set; }
        [Inject]
        CourseService _CourseService { get; set; }
        public TelerikGrid<CourseDto> Grid { get; set; }

        public List<int?> PageSizes => true ? new List<int?> { 10, 25, 50, null } : null;
        private int PageSize = 10;
        private int PageIndex { get; set; } = 2;
        private async Task PageChangedHandler(int currPage)
        {
            PageIndex = currPage;
        }

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            await LoadLookupData();
            IsLoading = false;
            await base.OnInitializedAsync();

        }
        private async Task LoadLookupData()
        {
            // lstcourse = await Http.GetFromJsonAsync<List<Course>>("api/Course/GetCourses", options);
        }

        public async Task ReadItems(GridReadEventArgs args)
        {
            IsLoading = true;
            DataEnvelope<CourseDto> result = await _CourseService.GetCoursesService(args.Request);

            if (args.Request.Groups.Count > 0)
            {
                /***
                NO GROUPING FOR THE TIME BEING
                var data = GroupDataHelpers.DeserializeGroups<WeatherForecast>(result.GroupedData);
                GridData = data.Cast<object>().ToList();
                ***/
            }
            else
            {
                ieCourses = result.CurrentPageData.ToList();
            }

            args.Total = result.TotalItemCount;
            args.Data = result.CurrentPageData.ToList();

            IsLoading = false;

            StateHasChanged();
        }

        private void NewCourse(GridCommandEventArgs e)
        {
            String EmptyGuid = Guid.Empty.ToString();
            NavManager.NavigateTo($"/Course/Detail/{EmptyGuid}");
        }
        private void DeleteCourse(GridCommandEventArgs e)
        {
            CourseDto _CourseDTO = e.Item as CourseDto;
            NavManager.NavigateTo($"/Course/DeleteCourse/{_CourseDTO.CourseNo}");
        }
    }



}