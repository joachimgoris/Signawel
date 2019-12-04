﻿using Plugin.Media;
using Plugin.Media.Abstractions;
using Signawel.Mobile.Bootstrap.Abstract;
using Signawel.Mobile.Constants;
using Signawel.Mobile.Models;
using Signawel.Mobile.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Signawel.Mobile.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IReportService _reportService;
        private readonly IIssueService _issueService;

        public RoadWork Roadwork { get; set; }
        public IList<string> IssueTypes { get; set; }
        public string IssueType { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public ObservableCollection<Image> Images { get; set; }

        public ICommand AddImageCommand => new Command(async () => await OnAddImage());
        public ICommand SendReportCommand => new Command(async () => await OnSendReport());
        public ICommand SearchRoadworkCommand => new Command(OnRoadworkSearch);

        public ReportViewModel(INavigationService navigationService, 
            IMessageBoxService messageBoxService,
            IReportService reportService,
            IIssueService issueService)
        {
            _navigationService = navigationService;
            _messageBoxService = messageBoxService;
            _reportService = reportService;
            _issueService = issueService;
            Images = new ObservableCollection<Image>();
        }

        public override async Task InitializeAsync(object data)
        {
            if (data != null)
            {
                Roadwork = data as RoadWork;
            }

            var issues = await _issueService.GetAllDefaultIssues();
            if (issues != null && issues.Count > 0)
            {
                IssueTypes = issues.Select(issue => issue.Name).ToList();
            }
            else
            {
                IssueTypes = new List<string>
                {
                    "Vuil",
                    "Ontbrekend",
                    "Kapot",
                    "Andere"
                };
            }
        }

        #region AddImages
        // Command used by the view to open a action sheet
        // giving the user the option to add from local storage or take a picture
        // the resulting action is then used to determine what to do
        private async Task OnAddImage()
        {
            var action = await _messageBoxService
                    .ShowActionSheet(TextConstants.AddPictureTitle,
                        TextConstants.CancelButton, null, new string[]
                        {
                            TextConstants.LocalStorage,
                            TextConstants.Camera
                        });
                switch (action)
                {
                    case TextConstants.Camera:
                        await TakePictureWithCamera();
                        break;
                    case TextConstants.LocalStorage:
                        await AddImageFromLocalStorage();
                        break;
                }
        }

        // Opens the Local storage to allow the user to select an image
        // Converts the resulting stream to an image source and adds it
        // to an observable list used to display it in the app
        private async Task AddImageFromLocalStorage()
        {
            var stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();

            if (stream != null)
            {
                var imageSource = ImageSource.FromStream(() => stream);
                Image image = new Image
                {
                    Source = imageSource
                };
                Images.Add(image);
            }
        }

        // Opens the camera to allow the user to take a picture
        // Converts the resulting stream to an image source and adds it
        // to an observable list used to display it in the app
        private async Task TakePictureWithCamera()
        {

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                _messageBoxService.ShowAlert(TextConstants.NoCameraAvailableTitle,
                    TextConstants.NoCameraAvailableMessage);
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Signawel",
                Name = $"{Guid.NewGuid().ToString()}.jpg",
                SaveToAlbum = true,
                PhotoSize = PhotoSize.Small
            });

            if (file == null)
                return;

            var imagesource = ImageSource.FromStream(() => file.GetStream());
            Image image = new Image
            {
                Source = imagesource
            };
            Images.Add(image);
        }
        #endregion

        private void OnRoadworkSearch()
        {
            _navigationService.NavigateToAsync<MapPageViewModel>();
        }

        private async Task OnSendReport()
        {
            if (Roadwork != null)
            {
                var report = ConvertValuesToReport();
                var result = await _reportService.AddReport(report);

                if (result != null)
                {
                    if (result.Any(error => error.Code == "Failed to create a report."))
                    {
                        _messageBoxService.ShowAlert(TextConstants.CreateReportFailedTitle, "Failed to create report");
                    }
                    else
                    {
                        if (result.Any())
                        {
                            _messageBoxService.ShowAlert(TextConstants.ReportAddedWithErrorsTitle,
                                TextConstants.ReportAddedWithErrorsMessage);
                        }
                        _messageBoxService.ShowAlert(TextConstants.Success,
                            TextConstants.ReportAddedSuccessfullyMessage);
                        ClearReportForm();
                    }
                }
                else
                {
                    _messageBoxService.ShowAlert(TextConstants.Alert, "No response from server");
                }
            }
            else
            {
                _messageBoxService.ShowAlert(TextConstants.Alert, 
                    TextConstants.NoRoadworkSelectedMessage);
            }
        }

        private ReportData ConvertValuesToReport()
        {
            return new ReportData
                {
                    Images = Images,
                    Report = new Report
                    {
                        UserEmail = Email,
                        Description = Description,
                        RoadworkId = Roadwork.GipodId,
                        CreationTime = DateTime.Now
                    }
            };
        }

        public void ClearReportForm()
        {
            Roadwork = null;
            Email = null;
            Description = null;
            Images = new ObservableCollection<Image>();
        }
    }
}